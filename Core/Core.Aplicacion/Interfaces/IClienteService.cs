using Core.Dominio.AggregatesModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aplicacion.Interfaces
{
    public interface IClienteService
    {
        public Task<IEnumerable<Cliente>> GetClientes();
        public Task<Cliente> BuscarPorId(int IdCliente);
        public Task CrearCliente(Cliente cliente);
        public Task EditarCliente(Cliente cliente);
        public Task<bool> EliminarCliente(Cliente cliente);
    }
}
