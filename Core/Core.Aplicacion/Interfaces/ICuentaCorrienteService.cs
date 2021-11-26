using Core.Dominio.AggregatesModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.Aplicacion.Services.CuentaCorrienteService;

namespace Core.Aplicacion.Interfaces
{
    public interface ICuentaCorrienteService
    {
        public Task<IEnumerable<ClienteCuentaCorriente>> GetCuentaCorrientes();
        public Task<ClienteCuentaCorriente> BuscarPorId(int IdCliente);
        public Task<IEnumerable<ClienteCuentaCorriente>> GetCuentaCorrientesPorCliente(int IdCliente);
        public Task<IEnumerable<ClienteCuentaCorriente>> GetCuentaCorrientesPorVenta(int IdVenta);
    }
}
