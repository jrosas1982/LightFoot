using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Dominio.AggregatesModel;

namespace Core.Aplicacion.Interfaces
{
    public interface IControlStockInsumoService
    {
        public Task<IEnumerable<InsumoStock>> GetInsumoStock();
        public Task<InsumoStock> BuscarPorId(int IdInsumoStock);
        public Task CrearInsumoStock(InsumoStock insumoStock);
        public Task EditarInsumoStock(InsumoStock insumoStock);
        public Task<bool> EliminarInsumoStock(InsumoStock insumoStock);
    }
}
