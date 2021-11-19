using Core.Dominio.AggregatesModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aplicacion.Interfaces
{
    public interface ICuentaCorrienteService
    {
        public Task<IEnumerable<ClienteCuentaCorriente>> GetCuentaCorrientes();
        public Task<ClienteCuentaCorriente> BuscarPorId(int IdCliente);

    }
}
