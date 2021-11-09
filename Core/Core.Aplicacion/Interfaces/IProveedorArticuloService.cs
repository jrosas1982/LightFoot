using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Dominio.AggregatesModel;

namespace Core.Aplicacion.Interfaces
{
    public interface IProveedorArticuloService
    {
        public Task<int> AgregarArticuloAProveedor(ProveedorArticulo articuloProveedor);
        public Task<ProveedorArticulo> BuscarProveedorArticuloPorId(int idProveedorArticulo);
        public Task<bool> EliminararticuloDeProveedor(int lineaInsumo);
        public Task<ProveedorArticulo> BuscarProveedorArticulo(int idArticulo, int idProveedor);
    }
}
