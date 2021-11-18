using Core.Aplicacion.Interfaces;
using Core.Dominio.AggregatesModel;
using Core.Infraestructura;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MoreLinq;
using MoreLinq.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Aplicacion.Services
{
    public class RecetaService : IRecetaService
    {
        private readonly AppDbContext _db;
        private readonly IRecetaDetalleService _recetaDetalleService;
        private readonly ILogger<SolicitudService> _logger;

        public RecetaService(AppDbContext db, ILogger<SolicitudService> logger ,
            IRecetaDetalleService recetaDetalleService)
        {
            _db = db;
            _logger = logger;
            _recetaDetalleService = recetaDetalleService;
        }

        public async Task<Receta> BuscarPorId(int IdReceta)
        {
            try
            {
                var receta = await _db.Recetas
                    .AsNoTracking()
                    .Include(x => x.Articulo)
                    .Include(x => x.RecetaDetalles)
                        .ThenInclude(x => x.Insumo)
                    .Include(x => x.RecetaDetalles)
                        .ThenInclude(x => x.EtapaOrdenProduccion)
                    .Where(x => x.Id == IdReceta).SingleOrDefaultAsync();

                return receta;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($" Error al BuscarPorId { ex.Message }");
                throw;
            }
        }

        public async Task<IEnumerable<Receta>> GetRecetas()
        {
            try
            {
                var recetas = await _db.Recetas
                    .AsNoTracking()
                    .Include(x => x.Articulo)
                        .ThenInclude(x => x.ArticuloCategoria)
                    .Include(x => x.RecetaDetalles.OrderBy(x => x.EtapaOrdenProduccion.Orden).ThenBy(x => x.Insumo.Nombre))                    
                    .OrderByDescending(x => x.Activo)
                    .ToListAsync();
                    //   .ThenInclude(x => x.Insumo)
                    //.Include(x => x.RecetaDetalles)
                    //   .ThenInclude(x => x.EtapaOrdenProduccion);

                return recetas;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($" Error al GetRecetas { ex.Message }");

                throw;
            }

        }

        public async Task<IEnumerable<RecetaDetalle>> GetRecetaDetalles(int Idreceta)
        {
            try
            {
                var detallesSolicitud = await _db.RecetaDetalles.Where(x => x.IdReceta == Idreceta).OrderByDescending(x => x.Insumo.Nombre).ToListAsync();
                return detallesSolicitud;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($" Error al GetRecetaDetalles { ex.Message }");
                throw;
            }

        }

        public async Task<bool> EliminarReceta(int IdReceta)
        {
            try
            {
                var receta = _db.Recetas.Include(x => x.RecetaDetalles).Where(x => x.Id == IdReceta).Single();

                foreach (var item in receta.RecetaDetalles)
                {
                    _db.RecetaDetalles.Remove(item);
                }

                _db.Recetas.Remove(receta);

                await _db.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($" Error al EliminarReceta { ex.Message }");
                return false;
            }
        }

        public async Task<bool> ActivarDesactivarReceta(int IdReceta)
        {
            try
            {
                var item = await _db.Recetas.FindAsync(IdReceta); //returns a single item.
                item.Activo = !item.Activo;

                if (_db.Recetas.Any(x => x.IdArticulo == item.IdArticulo && x.Activo && x.Id != item.Id))
                    throw new Exception("Solo puede haber una receta actva por articulo a la vez");

                _db.Recetas.Update(item);

                await _db.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($" Error al ActivarDesactivarReceta { ex.Message }");
                return false;
            }    
        }

        public async Task<bool> CrearReceta(Receta receta)
        {
           try
           {
                _db.Recetas.Add(receta);
                await _db.SaveChangesAsync();
                return true;

                //var articulo = await _db.Articulos.FindAsync(receta.IdArticulo);
                ////if (articulo.Receta != null)
                ////    throw new Exception("el articulo ya posee una receta asociada");
                //articulo.Receta = receta;
                //_db.Articulos.Update(articulo);
           }
           catch (Exception ex)
           {
               _logger.LogInformation($"  { ex.Message }");
               return false;
           }
        }
    }
}
