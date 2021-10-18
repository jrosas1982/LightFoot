using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Dominio.AggregatesModel;

namespace Core.Aplicacion.Interfaces
{
    public interface ISucursalService
    {
        public Task<IEnumerable<Sucursal>> GetSucursales();
        public Task<Sucursal> BuscarPorId(int IdSucursal);
        public Task CrearSucursal(Sucursal sucursal);
        public Task EditarSucursal(Sucursal sucursal);
        public Task<bool> EliminarSucursal(Sucursal sucursal);
    }
}
