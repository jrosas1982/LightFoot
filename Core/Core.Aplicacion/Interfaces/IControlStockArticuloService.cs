using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Dominio.AggregatesModel;

namespace Core.Aplicacion.Interfaces
{
    public interface IControlStockArticuloService
    {
        public Task<IEnumerable<ArticuloStock>> GetArticuloStock();
        public Task<ArticuloStock> BuscarPorId(int IdArticuloStock);
        public Task CrearArticuloStock(ArticuloStock articuloStock);
        public Task EditarArticuloStock(ArticuloStock articuloStock);
        public Task<bool> EliminarArticuloStock(ArticuloStock articuloStock);

    }
}
