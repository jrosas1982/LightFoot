using Core.Aplicacion.Interfaces;
using Core.Dominio.AggregatesModel;
using Core.Infraestructura;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aplicacion.Services
{
    public class ClienteService : IClienteService
    {
        private readonly AppDbContext _db;
        private readonly ILogger<ClienteService> _logger;
        public ClienteService(AppDbContext db, ILogger<ClienteService> logger)
        {
            _logger = logger;
            _db = db;
        }
        public async Task<Cliente> BuscarPorId(int IdCliente)
        {
           return await _db.Clientes.SingleOrDefaultAsync(x => x.Id == IdCliente);
        }

        public async Task CrearCliente(Cliente cliente)
        {
            try
            {
                _db.Clientes.Add(cliente);
               await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al CrearCliente con mensaje:  {ex.Message}");
            }
        }

        public async Task EditarCliente(Cliente cliente)
        {
            try
            {
                _db.Clientes.Update(cliente);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al EditarCliente con mensaje:  {ex.Message}");
            }
}

        public async Task<bool> EliminarCliente(Cliente cliente)
        {
            try
            {
                _db.Clientes.Remove(cliente);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al EliminarCliente con mensaje:  {ex.Message}");
                return false;
            }
        }


        public async Task<IEnumerable<Cliente>> GetClientes()
        {
            return await _db.Clientes.
            OrderByDescending(x => x.Id)
            .ToListAsync();
        }
    }
}
