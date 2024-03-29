﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Aplicacion.Interfaces;
using Core.Dominio;
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

        public ProveedorService(AppDbContext db, ILogger<ProveedorService> logger)
        {
            _db = db;
            _logger = logger;
        }


        public async Task<Proveedor> BuscarPorId(int IdProveedor)
        {
            var proveedor = await _db.Proveedores
                .Include(x => x.ProveedorInsumos.OrderBy(x => x.Insumo.Nombre))
                    .ThenInclude(x => x.Insumo)
                .Include(x => x.ProveedorArticulos.OrderBy(x => x.Articulo.Nombre))
                    .ThenInclude(x => x.Articulo)
                    .ThenInclude(x => x.ArticuloCategoria)
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
        public async Task EliminarProveedor(Proveedor proveedor)
        {
                var entidad = await _db.Proveedores.FindAsync(proveedor.Id);

                if (entidad.EsFabrica)
                    throw new ExcepcionControlada("No se puede eliminar un proveedor Fabrica");

                entidad.Eliminado = true;

                _db.Proveedores.Update(entidad);
                await _db.SaveChangesAsync();
        }
        public async Task<IEnumerable<Proveedor>> GetProveedores()
        {
            var proveedoresList = await _db.Proveedores
                .Where(x => !x.Eliminado)
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
