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
    public class ControlStockArticuloService : IControlStockArticuloService
    {
        private readonly AppDbContext _db;
        private readonly ILogger<ControlStockArticuloService> _logger;
        private readonly int IdSucursal;
        public ControlStockArticuloService(ExtendedAppDbContext extendedAppDbContext, ILogger<ControlStockArticuloService> logger)
        {
            _logger = logger;
            _db = extendedAppDbContext.context;
            IdSucursal = int.Parse(_db.GetSucursalId());
        }
        public async Task<ArticuloStock> BuscarPorId(int IdArticuloStock)
        {
            var articuloStock = await _db.ArticulosStock.Include(x => x.Articulo).SingleOrDefaultAsync(s => s.Id == IdArticuloStock);
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
            articuloStockDb.EsReposicionAutomatica = articuloStock.EsReposicionAutomatica;
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
           // var articuloStockList = await _db.ArticulosStock.ToListAsync();
            var articuloStockList = await _db.ArticulosStock
                .Where(x => x.IdSucursal == IdSucursal)
                .Include(p => p.Articulo)
                    .ThenInclude(p => p.ArticuloCategoria)
                .OrderBy(x => x.Articulo.ArticuloCategoria)
                    .ThenByDescending(x => x.Articulo.CodigoArticulo)
                .ToListAsync();
            _logger.LogInformation("Se buscaron el stock de articulos");
            return articuloStockList;
        }
    }
}
