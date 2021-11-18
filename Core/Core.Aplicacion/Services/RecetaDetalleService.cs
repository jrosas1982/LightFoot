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
    public class RecetaDetalleService : IRecetaDetalleService
    {
        private readonly AppDbContext _db;
        private readonly ILogger<RecetaDetalleService> _logger;
        public RecetaDetalleService(AppDbContext db, ILogger<RecetaDetalleService> logger)
        {
            _logger = logger;
            _db = db;
        }
        public async Task<int> AgregarInsumoAReceta(RecetaDetalle detalle)
        {
            try
            {
                _db.RecetaDetalles.Add(detalle);
                _logger.LogInformation($"Insumo creado para receta : {detalle.IdReceta}");
                await _db.SaveChangesAsync();
                return detalle.Id;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error al crear detalle para la solicitud : {detalle.IdReceta} - Error: {ex.Message}");
                return -1;
            }
          
        }

        public async Task<RecetaDetalle> BuscarInsumoDeRecetaPorId(int idInsumo)
        {
            var detalle = await _db.RecetaDetalles.SingleOrDefaultAsync(x => x.Id == idInsumo);
            if (detalle == null)
                throw new Exception("La solicitud solicitada no existe");
            return detalle;
        }

        public async Task<bool> EliminarInsumoAReceta(int lineaInsumo)
        {
            try
            {
                var itemToRemove = _db.RecetaDetalles.SingleOrDefault(x => x.Id == lineaInsumo); //returns a single item.

                if (itemToRemove == null)
                {
                    return false;
                }
                _db.RecetaDetalles.Remove(itemToRemove);
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
