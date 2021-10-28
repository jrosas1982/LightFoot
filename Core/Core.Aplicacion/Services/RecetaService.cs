﻿using Core.Aplicacion.Interfaces;
using Core.Dominio.AggregatesModel;
using Core.Infraestructura;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MoreLinq;
using MoreLinq.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Core.Aplicacion.Services
{
    public class RecetaService : IRecetaService
    {
        private readonly AppDbContext _db;
        private readonly IRecetaDetalleService _recetaDetalleService;
        private readonly ILogger<SolicitudService> _logger;

        public RecetaService(ExtendedAppDbContext extendedAppDbContext, ILogger<SolicitudService> logger , IRecetaDetalleService recetaDetalleService)
        {
            _db = extendedAppDbContext.context;
            _logger = logger;
            _recetaDetalleService = recetaDetalleService;
        }

        public async Task<Receta> BuscarPorId(int IdReceta)
        {
            var receta = await _db.Recetas
                .Include(x => x.Articulo)
                .Include(x => x.RecetaDetalles)
                .ThenInclude(x => x.Insumo)
                .Include(x => x.RecetaDetalles)
                .ThenInclude(x => x.EtapaOrdenProduccion).Where(x =>  x.Id == IdReceta).SingleOrDefaultAsync();

            return receta;
        }

        public async Task<IEnumerable<Receta>> GetRecetas()
        {
            var recetasList = _db.Recetas
                .Include(x => x.Articulo)
                .Include(x => x.RecetaDetalles)
                .ThenInclude(x => x.Insumo)
                .Include(x => x.RecetaDetalles)
                .ThenInclude(x => x.EtapaOrdenProduccion);
           
            _logger.LogInformation("Se buscaron las solicitudes");
            return await recetasList.ToListAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<RecetaDetalle>> GetRecetaDetalles(int Idreceta)
        {
            var detallesSolicitud = _db.RecetaDetalles.Where(x => x.IdReceta == Idreceta);

            return await detallesSolicitud.ToListAsync();
        }

        public async Task<bool> EliminarReceta(int IdReceta)
        {
            try
            {
                var receta = _db.Recetas.Where(x =>x.Id == IdReceta).Include(x => x.RecetaDetalles).SingleOrDefault();
                _db.Recetas.Remove(receta);
               await  _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"  { ex.Message }");
                return false;
            }
        }

        public async Task<bool> ActivarDesactivarReceta(int IdReceta)
        {
            var item = _db.Recetas.SingleOrDefault(x => x.Id == IdReceta); //returns a single item.

            if (item == null)
            {
                return false;
            }
            item.Activo = item.Activo ? false : true;
            _db.Recetas.Update(item);
            _db.SaveChanges();
            return true;
        }

        public async Task<bool> CrearReceta(Receta receta)
        {
           try
           {
               _db.Recetas.Add(receta);
               await _db.SaveChangesAsync();
               return true;
           }
           catch (Exception ex)
           {
               _logger.LogInformation($"  { ex.Message }");
               return false;
           }
        }
    }
}
