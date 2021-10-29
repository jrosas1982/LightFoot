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
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Core.Aplicacion.Services
{
    public class RecetaService : IRecetaService
    {
        private readonly AppDbContext _db;
        private readonly IRecetaDetalleService _recetaDetalleService;
        private readonly ILogger<SolicitudService> _logger;

        public RecetaService(ExtendedAppDbContext extendedAppDbContext, ILogger<SolicitudService> logger , IRecetaDetalleService recetaDetalleService)
        {
            _db = extendedAppDbContext.context;
            _logger = logger;
            _recetaDetalleService = recetaDetalleService;
        }

        public async Task<Receta> BuscarPorId(int IdReceta)
        {
            try
            {
            var receta = await _db.Recetas
                .Include(x => x.Articulo)
                .Include(x => x.RecetaDetalles)
                .ThenInclude(x => x.Insumo)
                .Include(x => x.RecetaDetalles)
                .ThenInclude(x => x.EtapaOrdenProduccion).Where(x => x.Id == IdReceta).SingleOrDefaultAsync();

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
             var recetasList = _db.Recetas
                 .Include(x => x.Articulo)
                 .Include(x => x.RecetaDetalles)
                 .ThenInclude(x => x.Insumo)
                 .Include(x => x.RecetaDetalles)
                 .ThenInclude(x => x.EtapaOrdenProduccion);

                return await recetasList.ToListAsync().ConfigureAwait(false);
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
                var detallesSolicitud = _db.RecetaDetalles.Where(x => x.IdReceta == Idreceta);
                return await detallesSolicitud.ToListAsync();
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
                var receta = _db.Recetas.Where(x => x.Id == IdReceta).SingleOrDefault();
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
                var item = _db.Recetas.SingleOrDefault(x => x.Id == IdReceta); //returns a single item.

                if (item == null)
                {
                    return false;
                }
                item.Activo = item.Activo ? false : true;
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
           }
           catch (Exception ex)
           {
               _logger.LogInformation($"  { ex.Message }");
               return false;
           }
        }
    }
}
