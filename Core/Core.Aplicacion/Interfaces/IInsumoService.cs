using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Dominio.AggregatesModel;

namespace Core.Aplicacion.Interfaces
{
    public interface IInsumoService
    {
        public Task<IEnumerable<Insumo>> GetInsumos();
        public Task<IEnumerable<Proveedor>> GetProveedoresInsumo(int idInsumo);
        public Task<Insumo> BuscarPorId(int IdInsumo);
        public Task CrearInsumo(Insumo insumo);
        public Task EditarInsumo(Insumo insumo);
        public Task<bool> EliminarInsumo(Insumo insumo);
    }
}
