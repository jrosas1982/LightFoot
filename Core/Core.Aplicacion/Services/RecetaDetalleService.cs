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
        public RecetaDetalleService(ExtendedAppDbContext extendedAppDbContext, ILogger<RecetaDetalleService> logger)
        {
            _logger = logger;
            _db = extendedAppDbContext.context;
        }
        public async Task<int> AgregarInsumoAReceta(RecetaDetalle detalle)
        {
            try
            {
                _db.Add(detalle);
                _logger.LogInformation($"Insumo creado para receta : {detalle.IdReceta}");
                _db.SaveChanges();
                return detalle.Id;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error al crear detalle para la solicitud : {detalle.IdReceta} - Error: {ex.Message}");
                return -1;
            }
          
        }

        public RecetaDetalle BuscarInsumoDeRecetaPorId(int IdInsumo)
        {
            var detalle = _db.RecetaDetalles.SingleOrDefault(x => x.Id == IdInsumo);
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
                _db.SaveChanges();
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
