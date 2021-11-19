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

        public async Task<IEnumerable<ClienteCuentaCorriente>> GetCuentaCorrientes()
        {
            try
            {
                var test = await _db.ClientesCuentaCorriente.Where(x => !x.Eliminado)
                             .OrderByDescending(x => x.Id).ToListAsync();

                return test;
            }
            catch (Exception ex)
            {
                var a = ex.Message;
                throw;
            }
  
        }
    }
}
