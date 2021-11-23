using Core.Aplicacion.Helpers;
using Core.Aplicacion.Interfaces;
using Core.Dominio.AggregatesModel;
using Core.Infraestructura;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _Configuration;
        public ClienteService(AppDbContext db, ILogger<ClienteService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _db = db;
            _Configuration = configuration;
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

                await EnviarMailNuevoCliente(cliente.Id);
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
                var entidad = await _db.Clientes.FindAsync(cliente.Id);

                entidad.Eliminado = true;

                _db.Clientes.Update(entidad);
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
            return await _db.Clientes
                .Where(x => !x.Eliminado)
                .OrderByDescending(x => x.Id)
                .ToListAsync();
        }

        public async Task EnviarMailNuevoCliente(int idCliente)
        {
            var NuevoCliente = await _db.Clientes
                .SingleAsync(x => x.Id == idCliente);

            byte[] dataMail = Convert.FromBase64String(_Configuration.GetSection("EmailTemplates").GetSection("NuevoCliente")["EmailBody"]);
            string templateBaseMail = Encoding.UTF8.GetString(dataMail);

            var idSucursal = int.Parse(_db.GetSucursalId());

            var template = templateBaseMail.Replace("@Cliente", NuevoCliente.Nombre)
                                           .Replace("@Cliente", NuevoCliente.CUIT)
                                           .Replace("@Cliente", NuevoCliente.Direccion)
                                           .Replace("@Cliente", NuevoCliente.Telefono)
                                           .Replace("@Cliente", NuevoCliente.Contacto)
                                           .Replace("@Cliente", NuevoCliente.Email);

            await EmailSender.SendEmail($"LightFoot - Bienvenido {NuevoCliente.Nombre}", template, NuevoCliente.Email);
        }
    }
}
