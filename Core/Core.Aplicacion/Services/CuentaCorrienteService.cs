using Core.Aplicacion.Interfaces;
using Core.Dominio.AggregatesModel;
using Core.Infraestructura;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MoreLinq;

namespace Core.Aplicacion.Services
{


    public class CuentaCorrienteService : ICuentaCorrienteService
    {
        private readonly AppDbContext _db;
        private readonly ILogger<CuentaCorrienteService> _logger;

        public CuentaCorrienteService(AppDbContext db , ILogger<CuentaCorrienteService> logger, IClienteService clienteService)
        {
            _db = db;
            _logger = logger;
        }
        public Task<ClienteCuentaCorriente> BuscarPorId(int IdCliente)
        {

            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ClienteCuentaCorriente>> GetCuentaCorrientesPorCliente(int IdCliente)
        {
            return await _db.ClientesCuentaCorriente
                .Where(x => !x.Eliminado && x.IdCliente == IdCliente)
                .Include(c => c.Cliente)
                .Include(d => d.Venta)
                .ToListAsync();

        }

        public async Task<IEnumerable<ClienteCuentaCorriente>> GetCuentaCorrientes()
        {
            return  await _db.ClientesCuentaCorriente
                .Where(x => !x.Eliminado)
                .Include(c => c.Cliente)
                .Include(d => d.Venta)
                .ToListAsync();

        }

        public async Task<IEnumerable<ClienteCuentaCorriente>> GetCuentaCorrientesPorVenta(int IdVenta)
        {
            return await _db.ClientesCuentaCorriente
                .Where(x => !x.Eliminado && x.IdVenta == IdVenta)
                .Include(v => v.Venta)
                .ToListAsync();

        }

    }
}
