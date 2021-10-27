using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Dominio.AggregatesModel;


namespace Core.Aplicacion.Interfaces
{
    public interface IProveedorService
    {
        public Task<IEnumerable<Proveedor>> GetProveedores();
        public Task<Proveedor> BuscarPorId(int IdProveedor);
        public Task CrearProveedor(Proveedor proveedor);
        public Task EditarProveedor(Proveedor proveedor);
        public Task<bool> EliminarProveedor(Proveedor proveedor);
    }
}
