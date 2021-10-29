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
        public Task<ProveedorInsumo> BuscarProveedorInsumoPorId(int idInsumo);
        public Task<bool> EliminarInsumoDeProveedor(int lineaInsumo);
    }
}
