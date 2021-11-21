using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Aplicacion.FIlters;
using Core.Aplicacion.Helpers;
using Core.Aplicacion.Interfaces;
using Core.Dominio;
using Core.Dominio.AggregatesModel;
using Core.Infraestructura;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Z.EntityFramework.Plus;

namespace Core.Aplicacion.Services
{
    public class SolicitudService : ISolicitudService
    {
        private readonly AppDbContext _db;
        private readonly ILogger<SolicitudService> _logger;
        private readonly IFabricacionService _fabricacion;

        public SolicitudService(AppDbContext db, ILogger<SolicitudService> logger, IFabricacionService fabricacion)
        {
            _db = db;
            _logger = logger;
            _fabricacion = fabricacion;
        }

        public async Task CrearSolicitud(Solicitud solicitud, IEnumerable<SolicitudDetalle> solicitudDetalles)
        {
            if (!_db.Sucursales.Any(x => x.Id == solicitud.IdSucursal))
                throw new ExcepcionControlada("No es posible procesar la solicitud. La sucursal especificada no existe");

            var idArticulos = _db.Articulos.Select(x => x.Id);
            if (solicitudDetalles.Any(x => !idArticulos.Contains(x.IdArticulo)))
                throw new ExcepcionControlada("No es posible procesar la solicitud. Al menos un detalle posee un articulo no existente");

            var cantidadDeIdArticulos = solicitudDetalles.GroupBy(x => x.IdArticulo);
            if (cantidadDeIdArticulos.Any(x => x.Count() > 1))
                throw new ExcepcionControlada("El detalle de una solicitud no pueden poseeer articulos repetidos");

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
                throw new ExcepcionControlada($"La solicitud {idSolicitud} no existe");

            if(solicitudDB.SolicitudDetalles.Any(x => x.Articulo.Eliminado))
                throw new ExcepcionControlada($"Al menos un articulo perteneciente a la solicitud {idSolicitud} no existe o fue eliminado");

            //TODO agregar validacion receta; UPDATE creo que era esto
            var articulosConReceta = await _db.Recetas.Where(x => x.Activo).Select(x => x.IdArticulo).ToListAsync();
            if (solicitudDB.SolicitudDetalles.Any(x => !articulosConReceta.Contains(x.IdArticulo)))
                throw new ExcepcionControlada($"Al menos un articulo perteneciente a la solicitud {idSolicitud} no posee una receta de fabricacion activa asociada");

            //if (solicitudDB.SolicitudDetalles.Any(x => !x.Articulo.Recetas.Any(x => x.Activo)))
            //    throw new Exception($"Al menos un articulo perteneciente a la solicitud {idSolicitud} no posee una receta de fabricacion activa asociada");

            var insumosNecesarios = await _fabricacion.ContabilizarInsumosRequeridos(idSolicitud);

            var insumosVerificados = await _fabricacion.VerificarStockInsumos(insumosNecesarios);

            if (insumosVerificados.Any(x => x.CantidadDisponible < x.CantidadNecesaria))
                throw new ExcepcionControlada("No hay suficiente stock disponible para aprobar la solicitud");

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
                throw new ExcepcionControlada("No existe una etapa inicial configurada en el sistema");

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


        public async Task<bool> RechazarSolicitud(int idSolicitud)
        {
            try
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
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en RechazarSolicitud Mensaje: {ex.Message}");
                return false;

            }

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
                        .ThenInclude(x => x.Recetas)
                            .ThenInclude(x => x.RecetaDetalles)
                                .ThenInclude(x => x.Insumo)
                .Include(x => x.SolicitudDetalles)
                    .ThenInclude(x => x.Articulo)
                        .ThenInclude(x => x.Recetas)
                            .ThenInclude(x => x.RecetaDetalles)
                                .ThenInclude(x => x.EtapaOrdenProduccion)
                .Include(x => x.SolicitudDetalles)
                    .ThenInclude(x => x.Articulo)
                        .ThenInclude(x => x.ArticuloCategoria)
                .SingleOrDefaultAsync(x => x.Id == idSolicitud);
            if (solicitud == null)
                throw new ExcepcionControlada("La solicitud solicitada no existe");
            return solicitud;
        }

        public async Task<IEnumerable<Solicitud>> GetSolicitudes()
        {
            var options = new MemoryCacheEntryOptions() { SlidingExpiration = TimeSpan.FromSeconds(10) };

            var solicitudesList = _db.Solicitudes
                .Where(x => !x.Eliminado)
                .OrderByDescending(x => x.EstadoSolicitud == EstadoSolicitud.PendienteAprobacion)
                .ThenByDescending(x => x.FechaModificacion.HasValue)
                .ThenByDescending(x => x.FechaCreacion)
                .IncludeOptimized(x => x.Sucursal)
                .IncludeOptimized(x => x.Sucursal.Nombre)                
                .IncludeOptimized(x => x.SolicitudDetalles)
                .IncludeOptimized(x => x.SolicitudDetalles.Select(d => d.Articulo))
                .IncludeOptimized(x => x.SolicitudDetalles.Select(d => d.Articulo.ArticuloCategoria))
                .IncludeOptimized(x => x.SolicitudDetalles.Select(d => d.Articulo.ArticuloCategoria.Descripcion))
                .IncludeOptimized(x => x.SolicitudDetalles.Select(d => d.OrdenesProduccion.Select(o => o.Id)))
                .ToList();

            //.ToListAsync()
            //.FromCacheAsync(options);

            //var solicitudesList = await _db.Solicitudes
            //    .AsNoTracking()
            //    .Include(x => x.Sucursal)
            //    .Include(x => x.SolicitudDetalles)
            //        .ThenInclude(x => x.Articulo)
            //            .ThenInclude(x => x.ArticuloCategoria)
            //    .Include(x => x.SolicitudDetalles)
            //        .ThenInclude(x => x.OrdenesProduccion)
            //    .OrderByDescending(x => x.EstadoSolicitud == EstadoSolicitud.PendienteAprobacion)
            //    .ThenByDescending(x => x.FechaModificacion.HasValue)
            //    .ThenByDescending(x => x.FechaCreacion)
            //    .ToListAsync();

            _logger.LogInformation("Se buscaron las solicitudes");
            return await Task.FromResult(solicitudesList);
        }

        public async Task<IEnumerable<Solicitud>> GetSolicitudes(SolicitudFilter filter)
        {
            try
            {
                var solicitudesList = _db.Solicitudes
                    .Where(x => !x.Eliminado)
                    .OrderByDescending(x => x.EstadoSolicitud == EstadoSolicitud.PendienteAprobacion)
                    .ThenByDescending(x => x.FechaModificacion.HasValue)
                    .ThenByDescending(x => x.FechaCreacion)
                    .IncludeOptimized(x => x.Sucursal)
                    .IncludeOptimized(x => x.Sucursal.Nombre)
                    .IncludeOptimized(x => x.SolicitudDetalles)
                    .IncludeOptimized(x => x.SolicitudDetalles.Select(d => d.Articulo))
                    .IncludeOptimized(x => x.SolicitudDetalles.Select(d => d.Articulo.ArticuloCategoria))
                    .IncludeOptimized(x => x.SolicitudDetalles.Select(d => d.Articulo.ArticuloCategoria.Descripcion))
                    .IncludeOptimized(x => x.SolicitudDetalles.Select(d => d.OrdenesProduccion.Select(o => o.Id)));

                var solicitudesFiltered = solicitudesList.Where(x => x.FechaCreacion > filter.FechaDesde
                                                                  && x.FechaCreacion < filter.FechaHasta.AddDays(1));

                if (filter.IdSucursalList != null && filter.IdSucursalList.Any())
                    solicitudesFiltered = solicitudesFiltered.Where(x => filter.IdSucursalList.Contains(x.IdSucursal));
                if (filter.EstadoSolicitudList != null && filter.EstadoSolicitudList.Any())
                    solicitudesFiltered = solicitudesFiltered.Where(x => filter.EstadoSolicitudList.Contains(x.EstadoSolicitud));      

                _logger.LogInformation("Se buscaron las solicitudes");
                return await solicitudesFiltered.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en GetSolicitudes Mensaje: {ex.Message}");
                throw ex;
            }

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
