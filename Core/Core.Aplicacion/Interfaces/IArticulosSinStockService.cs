using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Dominio.AggregatesModel;
using Core.Dominio.CoreModelHelpers;

namespace Core.Aplicacion.Interfaces
{
    public interface IArticulosSinStockService
    {
        public Task<IEnumerable<ArticuloStock>> GetArticulosSinStock();

    }
}