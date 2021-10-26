using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Aplicacion.Interfaces;
using Core.Dominio.AggregatesModel;
using Core.Infraestructura;
using Microsoft.Extensions.Logging;

namespace Core.Aplicacion.Services
{
    public class ControlStockInsumoService : IControlStockInsumoService
    {
        private readonly AppDbContext _db;
        private readonly ILogger<ControlStockInsumoService> _logger;
        public ControlStockInsumoService(ExtendedAppDbContext extendedAppDbContext, ILogger<ControlStockInsumoService> logger)
        {
            _logger = logger;
            _db = extendedAppDbContext.context;
        }

        public async Task<InsumoStock> BuscarPorId(int IdInsumoStock)
        {
            var insumo = await _db.InsumosStock.FindAsync(IdInsumoStock);
            return insumo;
        }

        public async Task CrearInsumoStock(InsumoStock insumoStock)
        {
            _db.Add(insumoStock);
            await _db.SaveChangesAsync();
        }

        public async Task EditarInsumoStock(InsumoStock insumoStock)
        {
            var insumoStockDb = await _db.InsumosStock.FindAsync(insumoStock.Id);

            insumoStock.IdProveedorPreferido = insumoStock.IdProveedorPreferido;
            insumoStock.StockTotal = insumoStock.StockTotal;
            insumoStock.StockReservado = insumoStock.StockReservado;

            _db.Update(insumoStockDb);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> EliminarInsumoStock(InsumoStock insumoStock)
        {
            try
            {
                var insumoStockDb = await _db.InsumosStock.FindAsync(insumoStock.Id);
                _db.Remove(insumoStockDb);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<InsumoStock>> GetInsumoStock()
        {
            var insumoStockList = _db.InsumosStock;
            _logger.LogInformation("Se buscaron el stock de insumos");
            return await Task.FromResult(insumoStockList);
        }
    }
}
