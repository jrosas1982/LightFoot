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
    public class ProveedorInsumoService : IProveedorInsumoService
    {
        private readonly AppDbContext _db;
        private readonly ILogger<ProveedorInsumoService> _logger;
        public ProveedorInsumoService(ExtendedAppDbContext extendedAppDbContext, ILogger<ProveedorInsumoService> logger)
        {
            _logger = logger;
            _db = extendedAppDbContext.context;
        }
        public async Task<int> AgregarInsumoAProveedor(ProveedorInsumo insumoProveedor)
        {
            try
            {
                var exists = _db.ProveedoresInsumos.Any(x => x.IdInsumo == insumoProveedor.IdInsumo && x.IdProveedor == insumoProveedor.IdProveedor);
                if (exists)
                    throw new Exception("Un proveedor no puede tener asignado el mismo insumo dos veces");
                _db.ProveedoresInsumos.Add(insumoProveedor);
                //_logger.LogInformation($"Insumo creado para receta : {insumoProveedor.IdReceta}");
                await _db.SaveChangesAsync();
                return insumoProveedor.Id;
            }
            catch (Exception ex)
            {
                //_logger.LogInformation($"Error al crear detalle para la solicitud : {insumoProveedor.IdReceta} - Error: {ex.Message}");
                return -1;
            }
        }

        public async Task<ProveedorInsumo> BuscarProveedorInsumoPorId(int idInsumo)
        {
            var detalle = await _db.ProveedoresInsumos.Include(x => x.Insumo).SingleOrDefaultAsync(x => x.Id == idInsumo);
            if (detalle == null)
                throw new Exception("El ProveedorInsumo solicitado no existe");
            return detalle;
        }
        public async Task<ProveedorInsumo> BuscarProveedorInsumo(int idInsumo, int idProveedor)
        {
            var detalle = await _db.ProveedoresInsumos.Include(x => x.Insumo).SingleOrDefaultAsync(x => x.IdInsumo == idInsumo && x.IdProveedor == idProveedor);
            if (detalle == null)
                throw new Exception("No existe un proveedor que venda el insumo solicitado");
            return detalle;
        }

        public async Task<decimal> GetPrecioInsumo(int idInsumo, int idProveedor)
        {
            var proveedorInsumo = await _db.ProveedoresInsumos.SingleOrDefaultAsync(x => x.IdInsumo == idInsumo && x.IdProveedor == idProveedor);
            if (proveedorInsumo == null)
                return 0;
            return proveedorInsumo.Precio;
        }

        public async Task ModificarPrecioInsumo(int idProveedorInsumo, decimal precio)
        {
            var proveedorInsumo = await _db.ProveedoresInsumos.SingleOrDefaultAsync(x => x.Id == idProveedorInsumo);
            if (proveedorInsumo == null)
                throw new Exception("El ProveedorInsumo solicitado no existe");
            proveedorInsumo.Precio = precio;
            _db.ProveedoresInsumos.Update(proveedorInsumo);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> EliminarInsumoDeProveedor(int lineaInsumo)
        {
            try
            {
                var itemToRemove = _db.ProveedoresInsumos.SingleOrDefault(x => x.Id == lineaInsumo); //returns a single item.

                if (itemToRemove == null)
                {
                    return false;
                }
                _db.ProveedoresInsumos.Remove(itemToRemove);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error al borrar detalle: {lineaInsumo} - Error: {ex.Message}");
                return false;

            }
        }

    }
}
