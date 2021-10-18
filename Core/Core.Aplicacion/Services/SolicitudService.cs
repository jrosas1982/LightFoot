using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Aplicacion.Helpers;
using Core.Aplicacion.Interfaces;
using Core.Dominio.AggregatesModel;
using Core.Infraestructura;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Core.Aplicacion.Services
{
    public class SolicitudService : ISolicitudService
    {
        private readonly AppDbContext _db;
        private readonly ILogger<SolicitudService> _logger;
        private readonly IFabricacionService _fabricacion;

        public SolicitudService(ExtendedAppDbContext extendedAppDbContext, ILogger<SolicitudService> logger, IFabricacionService fabricacion)
        {
            _db = extendedAppDbContext.context;
            _logger = logger;
            _fabricacion = fabricacion;
        }


        public async Task<IEnumerable<Solicitud>> GetSolicitudes()
        {
            var solicitudesList = _db.Solicitudes
                .AsNoTracking()
                .Include(x => x.Sucursal)
                .Include(x => x.SolicitudDetalles)
                    .ThenInclude(x => x.Articulo)
                .Include(x => x.SolicitudDetalles)
                    .ThenInclude(x => x.OrdenesProduccion);

            var queryString = solicitudesList.ToQueryString();

            _logger.LogInformation("Se buscaron las solicitudes");
            return await solicitudesList.ToListAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<SolicitudDetalle>> GetSolicitudDetalles(int idSolicitud)
        {
            var detallesSolicitud = _db.SolicitudDetalles.Where(x => x.IdSolicitud == idSolicitud);

            return await detallesSolicitud.ToListAsync();
        }

        public async Task<IEnumerable<SolicitudEvento>> GetSolicitudEventos(int idSolicitud)
        {
            var eventosSolicitud = _db.SolicitudEventos.Where(x => x.IdSolicitud == idSolicitud);

            return await Task.FromResult(eventosSolicitud);
        }

        public async Task<Solicitud> BuscarPorId(int idSolicitud)
        {
            var solicitud = await _db.Solicitudes.FindAsync(idSolicitud);
            return solicitud;
        }

        public async Task CrearSolicitud(Solicitud solicitud)
        {
            _db.Add(solicitud);
            await _db.SaveChangesAsync();
            _logger.LogInformation($"Solicitud creada: {solicitud.Comentario}");
        }

        public async Task EditarSolicitud(Solicitud solicitud)
        {
            var solicitudDB = await _db.Solicitudes.FindAsync(solicitud.Id);

            solicitudDB.IdSucursal = solicitud.IdSucursal;
            solicitudDB.EstadoSolicitud = solicitud.EstadoSolicitud;
            solicitudDB.Comentario = solicitud.Comentario;

            _db.Update(solicitudDB);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> EliminarSolicitud(Solicitud solicitud)
        {
            try
            {
                var solicitudDB = await _db.Solicitudes.FindAsync(solicitud.Id);
                _db.Remove(solicitudDB);
                await _db.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<EstadoSolicitud>> GetEstadosSolicitud()
        {
            return await Task.FromResult(EnumExtensions.GetValues<EstadoSolicitud>());
        }

        public async Task<IList<CantidadInsumoNecesarioStock>> GetVerificacionInsumosStock(int Idsolicitud)
        {
            var insumosNecesarios = await _fabricacion.ContabilizarInsumosRequeridos(Idsolicitud);

            var insumosVerificados = await _fabricacion.VerificarStockInsumos(insumosNecesarios);

            return insumosVerificados;
        }


        public async Task AprobarSolicitud(int idSolicitud, string comentario = "")
        {
            var solicitudDB = _db.Solicitudes
                .Include(x => x.SolicitudDetalles)
                .SingleOrDefault(x => x.Id == idSolicitud);

            if (solicitudDB == null)
                throw new Exception($"La solicitud {idSolicitud} no existe");

            var insumosVerificados = await GetVerificacionInsumosStock(solicitudDB.Id);

            if (insumosVerificados.Any(x => x.CantidadDisponible < x.CantidadNecesaria))
                throw new Exception("No hay suficiente stock disponible para aprobar la solicitud");

            solicitudDB.EstadoSolicitud = EstadoSolicitud.Aprobada;
            solicitudDB.Comentario = comentario;

            _db.Update(solicitudDB);

            var PrimerEtapaOrdenProduccion = _db.EtapasOrdenProduccion.OrderBy(x => x.Orden).FirstOrDefault();
            if (PrimerEtapaOrdenProduccion == null)
                throw new Exception("No existe una etapa inicial configurada en el sistema");

            var ordenesProduccion = new List<OrdenProduccion>();

            foreach (var detalle in solicitudDB.SolicitudDetalles)
            {
                ordenesProduccion.Add(new OrdenProduccion()
                {
                    IdArticulo = detalle.IdArticulo,
                    IdSolicitudDetalle = detalle.Id,
                    IdEtapaOrdenProduccion = PrimerEtapaOrdenProduccion.Id,
                    EstadoOrdenProduccion = EstadoOrdenProduccion.EnProceso,
                    EstadoEtapaOrdenProduccion = EstadoEtapaOrdenProduccion.Pendiente,
                    CantidadTotal = detalle.CantidadSolicitada,
                    CantidadTotalFabricada = 0,
                });
            }

            _db.AddRange(ordenesProduccion);

            await _db.SaveChangesAsync();
        }

        public async Task RechazarSolicitud(int idSolicitud, string comentario)
        {
            var solicitudDB = await _db.Solicitudes.FindAsync(idSolicitud);

            solicitudDB.EstadoSolicitud = EstadoSolicitud.Rechazada;
            solicitudDB.Comentario = comentario;

            _db.Update(solicitudDB);

            await _db.SaveChangesAsync();
        }

        public async Task AgregarSolicitudDetalles(int idSolicitud, IEnumerable<SolicitudDetalle> solicitudDetalle) => throw new NotImplementedException();

        public async Task EliminarSolicitudDetalle(int idSolicitudDetalle) => throw new NotImplementedException();

        public async Task EliminarSolicitudEvento(int idSolicitudDetalle) => throw new NotImplementedException();

        

    }
}
