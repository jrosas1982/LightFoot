using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Aplicacion.FIlters;
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

        public async Task CrearSolicitud(Solicitud solicitud, IEnumerable<SolicitudDetalle> solicitudDetalles, string comentario = "")
        {
            if (!_db.Sucursales.Any(x => x.Id == solicitud.IdSucursal))
                throw new Exception("No es posible procesar la solicitud. La sucursal especificada no existe");

            var idArticulos = _db.Articulos.Select(x => x.Id);
            if (solicitudDetalles.Any(x => !idArticulos.Contains(x.IdArticulo)))
                throw new Exception("No es posible procesar la solicitud. Al menos un detalle posee un articulo no existente");

            var cantidadDeIdArticulos = solicitudDetalles.GroupBy(x => x.IdArticulo);
            if (cantidadDeIdArticulos.Any(x => x.Count() > 1))
                throw new Exception("El detalle de una solicitud no pueden poseeer articulos repetidos");

            _db.Solicitudes.Add(solicitud);

            foreach (var detalle in solicitudDetalles)
            {
                solicitud.SolicitudDetalles.Add(detalle);
            }

            solicitud.SolicitudEventos.Add(new SolicitudEvento()
            {
                Comentario = solicitud.Comentario,
                EstadoSolicitud = solicitud.EstadoSolicitud
            });

            await _db.SaveChangesAsync();
            _logger.LogInformation($"Solicitud creada: {solicitud.Id}");

        }       


        public async Task AprobarSolicitud(int idSolicitud)
        {
            var solicitudDB = _db.Solicitudes
                .Include(x => x.SolicitudDetalles)
                    .ThenInclude(x => x.Articulo)
                .SingleOrDefault(x => x.Id == idSolicitud);

            if (solicitudDB == null)
                throw new Exception($"La solicitud {idSolicitud} no existe");

            //TODO agregar validacion receta; UPDATE creo que era esto
            var articulosConReceta = await _db.Recetas.Where(x => x.Activo).Select(x => x.IdArticulo).ToListAsync();
            if (solicitudDB.SolicitudDetalles.Any(x => !articulosConReceta.Contains(x.Id)))
                throw new Exception($"Al menos un articulo perteneciente a la solicitud {idSolicitud} no posee una receta de fabricacion activa asociada");

            if (solicitudDB.SolicitudDetalles.Any(x => !x.Articulo.Recetas.Any(x => x.Activo)))
                throw new Exception($"Al menos un articulo perteneciente a la solicitud {idSolicitud} no posee una receta de fabricacion activa asociada");

            var insumosNecesarios = await _fabricacion.ContabilizarInsumosRequeridos(idSolicitud);

            var insumosVerificados = await _fabricacion.VerificarStockInsumos(insumosNecesarios);

            if (insumosVerificados.Any(x => x.CantidadDisponible < x.CantidadNecesaria))
                throw new Exception("No hay suficiente stock disponible para aprobar la solicitud");

            await _fabricacion.ReservarStockInsumos(insumosNecesarios);

            solicitudDB.EstadoSolicitud = EstadoSolicitud.Aprobada;

            solicitudDB.SolicitudEventos.Add(new SolicitudEvento()
            {
                Comentario = solicitudDB.Comentario,
                EstadoSolicitud = solicitudDB.EstadoSolicitud
            });

            _db.Update(solicitudDB);            

            var PrimerEtapaOrdenProduccion = _db.EtapasOrdenProduccion.OrderBy(x => x.Orden).FirstOrDefault();
            if (PrimerEtapaOrdenProduccion == null)
                throw new Exception("No existe una etapa inicial configurada en el sistema");

            var ordenesProduccion = new List<OrdenProduccion>();

            foreach (var detalle in solicitudDB.SolicitudDetalles)
            {
                var ordenProduccion = new OrdenProduccion()
                {
                    IdArticulo = detalle.IdArticulo,
                    IdSolicitudDetalle = detalle.Id,
                    IdEtapaOrdenProduccion = PrimerEtapaOrdenProduccion.Id,
                    EstadoOrdenProduccion = EstadoOrdenProduccion.EnProceso,
                    EstadoEtapaOrdenProduccion = EstadoEtapaOrdenProduccion.Pendiente,
                    CantidadTotal = detalle.CantidadSolicitada,
                    CantidadFabricada = 0,
                };

                ordenesProduccion.Add(ordenProduccion);

                ordenProduccion.OrdenProduccionEventos.Add(new OrdenProduccionEvento()
                {
                    IdEtapaOrdenProduccion = ordenProduccion.IdEtapaOrdenProduccion,
                    EstadoOrdenProduccion = ordenProduccion.EstadoOrdenProduccion,
                    EstadoEtapaOrdenProduccion = ordenProduccion.EstadoEtapaOrdenProduccion,
                    CantidadFabricada = ordenProduccion.CantidadFabricada,
                    Comentario = "Solicitud Aprobada",
                });
            }

            _db.AddRange(ordenesProduccion);

            await _db.SaveChangesAsync();
        }

        public async Task RechazarSolicitud(int idSolicitud, string comentario)
        {
            var solicitudDB = await _db.Solicitudes.FindAsync(idSolicitud);

            solicitudDB.EstadoSolicitud = EstadoSolicitud.Rechazada;

            solicitudDB.SolicitudEventos.Add(new SolicitudEvento()
            {
                Comentario = solicitudDB.Comentario,
                EstadoSolicitud = solicitudDB.EstadoSolicitud
            });

            _db.Update(solicitudDB);
            await _db.SaveChangesAsync();
        }

        public async Task<Solicitud> BuscarPorId(int idSolicitud)
        {
            var solicitud = await _db.Solicitudes
                .AsNoTracking()
                .Include(x => x.Sucursal)
                .Include(x => x.SolicitudDetalles)
                    .ThenInclude(x => x.OrdenesProduccion)
                .Include(x => x.SolicitudDetalles)
                    .ThenInclude(x => x.Articulo)
                        .ThenInclude(x => x.Recetas.Single(x => x.Activo))
                            .ThenInclude(x => x.RecetaDetalles)
                                .ThenInclude(x => x.Insumo)
                .SingleOrDefaultAsync(x => x.Id == idSolicitud);
            if (solicitud == null)
                throw new Exception("La solicitud solicitada no existe");
            return solicitud;
        }

        public async Task<IEnumerable<Solicitud>> GetSolicitudes()
        {
            var solicitudesList = await _db.Solicitudes
                .AsNoTracking()
                .Include(x => x.Sucursal)
                .Include(x => x.SolicitudDetalles)
                    .ThenInclude(x => x.Articulo)
                .Include(x => x.SolicitudDetalles)
                    .ThenInclude(x => x.OrdenesProduccion).ToListAsync();

            _logger.LogInformation("Se buscaron las solicitudes");
            return solicitudesList;
        }

        public async Task<IEnumerable<Solicitud>> GetSolicitudes(SolicitudFilter filter)
        {
            var solicitudesList = await _db.Solicitudes
                .AsNoTracking()
                .Include(x => x.Sucursal)
                .Include(x => x.SolicitudDetalles)
                    .ThenInclude(x => x.Articulo)
                .Include(x => x.SolicitudDetalles)
                    .ThenInclude(x => x.OrdenesProduccion)
                .Where(x => filter.IdSucursalList.Contains(x.IdSucursal)
                            && filter.EstadoSolicitudList.Contains(x.EstadoSolicitud)
                            && x.FechaCreacion > filter.FechaDesde
                            && x.FechaCreacion < filter.FechaHasta)
                .ToListAsync();

            _logger.LogInformation("Se buscaron las solicitudes");
            return solicitudesList;
        }

        public async Task<IEnumerable<SolicitudDetalle>> GetSolicitudDetalles(int idSolicitud)
        {
            var detallesSolicitud = _db.SolicitudDetalles.Where(x => x.IdSolicitud == idSolicitud);

            return await detallesSolicitud.ToListAsync();
        }

        public async Task<IEnumerable<SolicitudEvento>> GetSolicitudEventos(int idSolicitud)
        {
            var eventosSolicitud = _db.SolicitudEventos.Where(x => x.IdSolicitud == idSolicitud);

            return await eventosSolicitud.ToListAsync();
        }

        public async Task<IEnumerable<EstadoSolicitud>> GetEstadosSolicitud()
        {
            return await Task.FromResult(EnumExtensions.GetValues<EstadoSolicitud>());
        }

        public async Task<bool> HayStockSuficiente(int idSolicitud) 
        {
            var insumosNecesarios = await _fabricacion.ContabilizarInsumosRequeridos(idSolicitud);

            var insumosVerificados = await _fabricacion.VerificarStockInsumos(insumosNecesarios);

            var hayStock = !insumosVerificados.Any(x => x.CantidadDisponible < x.CantidadNecesaria);

            return await Task.FromResult(hayStock);
        }

    }
}
