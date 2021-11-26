using Core.Aplicacion.Interfaces;
using Core.Dominio;
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
    public class ProveedorArticuloService : IProveedorArticuloService
    {
        private readonly AppDbContext _db;
        private readonly ILogger<ProveedorArticuloService> _logger;
        public ProveedorArticuloService(AppDbContext db, ILogger<ProveedorArticuloService> logger)
        {
            _logger = logger;
            _db = db;
        }

        public async Task<int> AgregarArticuloAProveedor(ProveedorArticulo articuloProveedor)
        {
            var exists = _db.ProveedoresArticulos.Any(x => x.IdArticulo == articuloProveedor.IdArticulo && x.IdProveedor == articuloProveedor.IdProveedor);
            if (exists)
                throw new ExcepcionControlada("Un proveedor no puede tener asignado el mismo articulo dos veces");
            _db.ProveedoresArticulos.Add(articuloProveedor);
            //_logger.LogInformation($"Insumo creado para receta : {insumoProveedor.IdReceta}");
            await _db.SaveChangesAsync();
            return articuloProveedor.Id;
        }

        public async Task<ProveedorArticulo> BuscarProveedorArticulo(int idArticulo, int idProveedor)
        {
            var detalle = await _db.ProveedoresArticulos.Include(x => x.Articulo).ThenInclude(x => x.ArticuloCategoria).SingleOrDefaultAsync(x => x.IdArticulo == idArticulo && x.IdProveedor == idProveedor);
            if (detalle == null)
                throw new ExcepcionControlada("No existe un proveedor que venda el articulo solicitado");
            return detalle;
        }

        public async Task<ProveedorArticulo> BuscarProveedorArticuloPorId(int idProveedorArticulo)
        {
            var detalle = await _db.ProveedoresArticulos.Include(x => x.Articulo).ThenInclude(x => x.ArticuloCategoria).SingleOrDefaultAsync(x => x.Id == idProveedorArticulo);
           
            if (detalle == null)
                throw new ExcepcionControlada("El ProveedorArticulo solicitado no existe");

            return detalle;
        }

        public async Task<bool> EliminarArticuloDeProveedor(int lineaArticulo)
        {
            try
            {
                var itemToRemove = _db.ProveedoresArticulos.SingleOrDefault(x => x.Id == lineaArticulo); //returns a single item.

                if (itemToRemove == null)
                {
                    return false;
                }
                _db.ProveedoresArticulos.Remove(itemToRemove);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error al borrar detalle: {lineaArticulo} - Error: {ex.Message}");
                return false;

            }
        }

        public async Task<decimal> GetPrecioArticulo(int idArticulo, int idProveedor)
        {
            var proveedorArticulo = await _db.ProveedoresArticulos.SingleOrDefaultAsync(x => x.IdArticulo == idArticulo && x.IdProveedor == idProveedor);
            if (proveedorArticulo == null)
                return 0;
            return proveedorArticulo.Precio;
        }

        public async Task ModificarPrecioArticulo(int idProveedorArticulo, decimal precio)
        {
            var proveedorArticulo = await _db.ProveedoresArticulos.SingleOrDefaultAsync(x => x.Id == idProveedorArticulo);

            if (proveedorArticulo == null)
                throw new ExcepcionControlada("El proveedorArticulo solicitado no existe");

            proveedorArticulo.Precio = precio;

            var historico = new ProveedorArticuloHistorico()
            {
                IdProveedor = proveedorArticulo.IdProveedor,
                IdArticulo = proveedorArticulo.IdArticulo,
                Precio = precio
            };

            _db.ProveedoresArticulosHistorico.Add(historico);

            _db.ProveedoresArticulos.Update(proveedorArticulo);

            await _db.SaveChangesAsync();
        }
    }
}
