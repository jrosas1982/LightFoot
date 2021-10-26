using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Aplicacion.Interfaces;
using Core.Dominio.AggregatesModel;
using Core.Infraestructura;
using Microsoft.Extensions.Logging;

namespace Core.Aplicacion.Services
{
    public class ProveedorService : IProveedorService
    {
        private readonly AppDbContext _db;
        private readonly ILogger<ProveedorService> _logger;

        public ProveedorService(ExtendedAppDbContext extendedAppDbContext, ILogger<ProveedorService> logger)
        {
            _db = extendedAppDbContext.context;
            _logger = logger;
        }


        public Task<Proveedor> BuscarPorId(int IdProveedor) => throw new NotImplementedException();
        public Task CrearProveedor(Proveedor proveedor) => throw new NotImplementedException();
        public Task EditarProveedor(Proveedor proveedor) => throw new NotImplementedException();
        public Task<bool> EliminarProveedor(Proveedor proveedor) => throw new NotImplementedException();
        public Task<IEnumerable<Proveedor>> GetProveedores() => throw new NotImplementedException();
    }
}
