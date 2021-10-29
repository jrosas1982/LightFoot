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
                _db.ProveedoresInsumos.Add(insumoProveedor);
                //_logger.LogInformation($"Insumo creado para receta : {insumoProveedor.IdReceta}");
                _db.SaveChanges();
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
            var detalle = await _db.ProveedoresInsumos.SingleOrDefaultAsync(x => x.Id == idInsumo);
            if (detalle == null)
                throw new Exception("La solicitud solicitada no existe");
            return detalle;
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
                _logger.LogInformation($"Error al borrar detalle : {lineaInsumo} - Error: {ex.Message}");
                return false;

            }
        }

    }
}
