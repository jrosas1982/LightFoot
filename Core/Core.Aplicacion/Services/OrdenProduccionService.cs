﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Aplicacion.Helpers;
using Core.Aplicacion.Interfaces;
using Core.Dominio.AggregatesModel;
using Core.Infraestructura;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MoreLinq;

namespace Core.Aplicacion.Services
{
    public class OrdenProduccionService : IOrdenProduccionService
    {
        private readonly AppDbContext _db;
        private readonly ILogger<OrdenProduccionService> _logger;

        public OrdenProduccionService(ExtendedAppDbContext extendedAppDbContext, ILogger<OrdenProduccionService> logger)
        {
            _db = extendedAppDbContext.context;
            _logger = logger;
        }

        public async Task<bool> IniciarEtapa(int idOrdenProduccion)
        {
            //reanuda la etapa si no estaba reanudada
            var ordenDb = await _db.OrdenesProduccion.FindAsync(idOrdenProduccion);

            if (ordenDb.EstadoEtapaOrdenProduccion != EstadoEtapaOrdenProduccion.Pendiente)
                throw new Exception("La etapa de la orden debe encontrarse en estado Pendiente para poder ser Iniciada");

            ordenDb.EstadoEtapaOrdenProduccion = EstadoEtapaOrdenProduccion.Iniciada;

            _db.OrdenesProduccion.Update(ordenDb);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> PausarEtapa(int idOrdenProduccion)
        {
            //pausa la etapa si no estaba pausada
            var ordenDb = await _db.OrdenesProduccion.FindAsync(idOrdenProduccion);

            if (ordenDb.EstadoEtapaOrdenProduccion != EstadoEtapaOrdenProduccion.Iniciada)
                throw new Exception("La etapa de la orden debe encontrarse en estado Iniciada para poder ser Pausada");

            ordenDb.EstadoOrdenProduccion = EstadoOrdenProduccion.Pausada;
            ordenDb.EstadoEtapaOrdenProduccion = EstadoEtapaOrdenProduccion.Pausada;

            _db.OrdenesProduccion.Update(ordenDb);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ReanudarEtapa(int idOrdenProduccion)
        {
            //reanuda la etapa si no estaba reanudada
            var ordenDb = await _db.OrdenesProduccion.FindAsync(idOrdenProduccion);

            if (ordenDb.EstadoEtapaOrdenProduccion != EstadoEtapaOrdenProduccion.Pausada)
                throw new Exception("La etapa de la orden debe encontrarse en estado Pausada para poder ser Reanudada");

            ordenDb.EstadoOrdenProduccion = EstadoOrdenProduccion.EnProceso;
            ordenDb.EstadoEtapaOrdenProduccion = EstadoEtapaOrdenProduccion.Iniciada;

            _db.OrdenesProduccion.Update(ordenDb);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> FinalizarEtapa(int idOrdenProduccion)
        {
            //pausa la etapa si no estaba pausada
            var ordenDb = await _db.OrdenesProduccion.FindAsync(idOrdenProduccion);

            if (ordenDb.EstadoEtapaOrdenProduccion != EstadoEtapaOrdenProduccion.Iniciada)
                throw new Exception("La etapa de la orden debe encontrarse en estado Iniciada para poder ser Finalizada");

            ordenDb.EstadoEtapaOrdenProduccion = EstadoEtapaOrdenProduccion.Finalizada;

            _db.OrdenesProduccion.Update(ordenDb);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RetrabajarEtapa(int idOrdenProduccion)
        {
            //marca la orden como retrabajo
            var ordenDb = await _db.OrdenesProduccion.FindAsync(idOrdenProduccion);

            if (ordenDb.EstadoEtapaOrdenProduccion != EstadoEtapaOrdenProduccion.Finalizada)
                throw new Exception("La etapa de la orden debe encontrarse en estado Finalizada para poder ser Retrabajada");

            ordenDb.EstadoEtapaOrdenProduccion = EstadoEtapaOrdenProduccion.Retrabajo;

            _db.OrdenesProduccion.Update(ordenDb);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> AvanzarSiguienteEtapa(int idOrdenProduccion)
        {
            var ordenDb = await _db.OrdenesProduccion.FindAsync(idOrdenProduccion);

            if (ordenDb.EstadoEtapaOrdenProduccion != EstadoEtapaOrdenProduccion.Finalizada)
                throw new Exception("La etapa de la orden debe encontrarse en estado Finalizada para poder avanzar a la siguiente etapa");

            var etapaActual = ordenDb.EtapaOrdenProduccion;
            var siguienteEtapa = _db.EtapasOrdenProduccion.Where(x => x.Orden > etapaActual.Orden).MinBy(x => x.Orden).SingleOrDefault();

            if (siguienteEtapa == null)
                throw new Exception("La orden se ya encuentra en la ultima etapa");

            ordenDb.EtapaOrdenProduccion = siguienteEtapa;
            ordenDb.EstadoEtapaOrdenProduccion = EstadoEtapaOrdenProduccion.Pendiente;

            _db.OrdenesProduccion.Update(ordenDb);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> FinalizarOrden(int idOrdenProduccion)
        {
            //marca la orden como finalizada si esta todo bien
            var ordenDb = await _db.OrdenesProduccion.FindAsync(idOrdenProduccion);

            var etapaActual = ordenDb.EtapaOrdenProduccion;
            var siguienteEtapa = _db.EtapasOrdenProduccion.Where(x => x.Orden > etapaActual.Orden).MinBy(x => x.Orden).SingleOrDefault();

            if (siguienteEtapa != null)
                throw new Exception("Aun quedan etapas por realizar");

            ordenDb.EstadoOrdenProduccion = EstadoOrdenProduccion.Finalizada;

            _db.OrdenesProduccion.Update(ordenDb);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> CancelarOrden(int idOrdenProduccion)
        {
            // TODO liberar todos los insumos que quedaban reservados
            var ordenDb = await _db.OrdenesProduccion.FindAsync(idOrdenProduccion);
            ordenDb.EstadoOrdenProduccion = EstadoOrdenProduccion.Cancelada;

            _db.OrdenesProduccion.Update(ordenDb);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<OrdenProduccion> BuscarPorId(int idOrdenProduccion)
        {
            var orden = await _db.OrdenesProduccion
                .Include(x => x.OrdenProduccionEventos)
                       .ThenInclude(x => x.EtapaOrdenProduccion)
                .Include(x => x.EtapaOrdenProduccion)         
                .SingleOrDefaultAsync(x => x.Id == idOrdenProduccion);
            if (orden == null)
                throw new Exception("La orden de produccion solicitada no existe");

            return orden;
        }

        public async Task<IEnumerable<OrdenProduccion>> GetOrdenes()
        {
            var ordenesList = _db.OrdenesProduccion
                .Include(x => x.Articulo)
                .Include(x => x.EtapaOrdenProduccion)
                .Include(x => x.SolicitudDetalle)
                    .ThenInclude(x => x.Solicitud)
                    .ThenInclude(x => x.Sucursal)
                .ToListAsync();

            _logger.LogInformation("Se buscaron las ordenes");
            return await ordenesList;
        }

        public async Task<IEnumerable<OrdenProduccionEvento>> GetOrdenEventos(int idOrdenProduccion)
        {
            return await _db.OrdenesProduccionEventos.ToListAsync();
        }

        public async Task<IEnumerable<EstadoOrdenProduccion>> GetEstadosOrden() 
        {
            return await Task.FromResult(EnumExtensions.GetValues<EstadoOrdenProduccion>());
        }

        public async Task<IEnumerable<EtapaOrdenProduccion>> GetEtapasOrden()
        {
            return await _db.EtapasOrdenProduccion.ToListAsync();
        }

        public async Task<IEnumerable<EstadoEtapaOrdenProduccion>> GetEstadoEtapasOrden()
        {
            return await Task.FromResult(EnumExtensions.GetValues<EstadoEtapaOrdenProduccion>());
        }

        public async Task<int> GetProgreso(int IdOrdenProduccion)
        {
            return await Task.FromResult(50);
        }
        public async Task<int> GetTiempoTranscurrido(int IdOrdenProduccion)
        {
            return await Task.FromResult(50);
        }


    }
}
