using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Dominio.AggregatesModel;

namespace Core.Aplicacion.Interfaces
{
    public interface IProveedorInsumoService
    {
        public Task<int> AgregarInsumoAProveedor(ProveedorInsumo insumoProveedor);
        public Task<ProveedorInsumo> BuscarProveedorInsumoPorId(int idProveedorInsumo);
        public Task<bool> EliminarInsumoDeProveedor(int lineaInsumo);
        public Task<ProveedorInsumo> BuscarProveedorInsumo(int idInsumo, int idProveedor);
        public Task<decimal> GetPrecioInsumo(int idInsumo, int idProveedor);
    }
}
