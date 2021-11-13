using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Aplicacion.Interfaces;
using Core.Dominio.AggregatesModel;
using Core.Infraestructura;
using Microsoft.EntityFrameworkCore;
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


        public async Task<Proveedor> BuscarPorId(int IdProveedor)
        {
            var proveedor = await _db.Proveedores
                .Include(x => x.ProveedorInsumos)
                    .ThenInclude(x => x.Insumo)
                .Include(x => x.ProveedorArticulos)
                    .ThenInclude(x => x.Articulo)
                .SingleOrDefaultAsync(x => x.Id == IdProveedor);
            return proveedor;
        }

        public async Task CrearProveedor(Proveedor proveedor)
        {
            _db.Proveedores.Add(proveedor);
            await _db.SaveChangesAsync();
        }

        public async Task EditarProveedor(Proveedor proveedor)
        {
            var proveedorDb = await _db.Proveedores.FindAsync(proveedor.Id);
            proveedorDb.CUIT = proveedor.CUIT;
            proveedorDb.Nombre = proveedor.Nombre;
            proveedorDb.Direccion = proveedor.Direccion;
            proveedorDb.Telefono = proveedor.Telefono;
            proveedorDb.Email = proveedor.Email;
            proveedorDb.Calificacion = proveedor.Calificacion;

            _db.Proveedores.Update(proveedorDb);
            await _db.SaveChangesAsync();
        }
        public async Task<bool> EliminarProveedor(Proveedor proveedor)
        {
            try
            {
                var proveedorDb = await _db.Proveedores.FindAsync(proveedor.Id);
                _db.Remove(proveedorDb);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<IEnumerable<Proveedor>> GetProveedores()
        {
            var proveedoresList = await _db.Proveedores
                .Include(x => x.ProveedorInsumos.OrderBy(x => x.Insumo.Nombre))
                    .ThenInclude(x => x.Insumo)
                .Include(x => x.ProveedorArticulos.OrderBy(x => x.Articulo.Nombre))
                    .ThenInclude(x => x.Articulo)
                    .ThenInclude(x => x.ArticuloCategoria)
                .OrderBy(x => x.Nombre)
                .ToListAsync();

            return proveedoresList;
        }

        public Task CalificarProveedor(double calificacion)
        {
            throw new NotImplementedException();
        }
    }
}
