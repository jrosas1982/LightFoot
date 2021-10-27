using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Aplicacion.Interfaces;
using Core.Dominio.AggregatesModel;
using Core.Infraestructura;
using Microsoft.Extensions.Logging;

namespace Core.Aplicacion.Services
{
    public class ControlStockArticuloService : IControlStockArticuloService
    {
        private readonly AppDbContext _db;
        private readonly ILogger<ControlStockArticuloService> _logger;
        public ControlStockArticuloService(ExtendedAppDbContext extendedAppDbContext, ILogger<ControlStockArticuloService> logger)
        {
            _logger = logger;
            _db = extendedAppDbContext.context;
        }
        public async Task<ArticuloStock> BuscarPorId(int IdArticuloStock)
        {
            var articuloStock = await _db.ArticulosStock.FindAsync(IdArticuloStock);
            return articuloStock;
        }

        public async Task CrearArticuloStock(ArticuloStock articuloStock)
        {
            _db.Add(articuloStock);
            await _db.SaveChangesAsync();
        }

        public async Task EditarArticuloStock(ArticuloStock articuloStock)
        {
            var articuloStockDb = await _db.ArticulosStock.FindAsync(articuloStock.Id);

            articuloStockDb.IdProveedorPreferido = articuloStock.IdProveedorPreferido;
            articuloStockDb.StockTotal = articuloStock.StockTotal;
            articuloStockDb.StockMinimo = articuloStock.StockMinimo;
            articuloStockDb.EsReposicionPorLote = articuloStock.EsReposicionPorLote;
            articuloStockDb.TamañoLote = articuloStock.TamañoLote;

            _db.Update(articuloStockDb);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> EliminarArticuloStock(ArticuloStock articuloStock)
        {
            try
            {
                var articuloStockDb = await _db.ArticulosStock.FindAsync(articuloStock.Id);
                _db.Remove(articuloStockDb);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<ArticuloStock>> GetArticuloStock()
        {
            var articuloStockList = _db.ArticulosStock;
            _logger.LogInformation("Se buscaron el stock de articulos");
            return await Task.FromResult(articuloStockList);
        }
    }
}
