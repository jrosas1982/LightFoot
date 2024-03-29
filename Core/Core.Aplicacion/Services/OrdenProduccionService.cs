﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Aplicacion.Helpers;
using Core.Aplicacion.Hubs;
using Core.Aplicacion.Interfaces;
using Core.Dominio;
using Core.Dominio.AggregatesModel;
using Core.Infraestructura;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MoreLinq;

namespace Core.Aplicacion.Services
{
    public class OrdenProduccionService : IOrdenProduccionService
    {
        private readonly AppDbContext _db;
        private readonly ILogger<OrdenProduccionService> _logger;
        private readonly IHubContext<NotificationsHub> _hubContext;
        private readonly IFabricacionService _fabricacionService;

        public OrdenProduccionService(AppDbContext db, ILogger<OrdenProduccionService> logger, IHubContext<NotificationsHub> hubContext, IFabricacionService fabricacionService)
        {
            _db = db;
            _logger = logger;
            _hubContext = hubContext;
            _fabricacionService = fabricacionService;
        }

        public async Task IniciarEtapa(int idOrdenProduccion, string comentario = "")
        {
            //reanuda la etapa si no estaba reanudada
            var ordenDb = await _db.OrdenesProduccion.Include(x => x.Articulo).Include(x => x.EtapaOrdenProduccion).ThenInclude(x => x.RecetaDetalle).SingleAsync(x => x.Id == idOrdenProduccion);

            if (ordenDb.Articulo.Eliminado)
                throw new ExcepcionControlada($"El articulo perteneciente a la orden de produccion {ordenDb.Id} no existe o fue eliminado");

            if (ordenDb.EstadoEtapaOrdenProduccion != EstadoEtapaOrdenProduccion.Pendiente)
                throw new ExcepcionControlada("La etapa de la orden debe encontrarse en estado Pendiente para poder ser Iniciada");
            
            ordenDb.EstadoOrdenProduccion = EstadoOrdenProduccion.EnProceso;
            ordenDb.EstadoEtapaOrdenProduccion = EstadoEtapaOrdenProduccion.Iniciada;

            _db.OrdenesProduccion.Update(ordenDb);

            var insumosNecesarios = _db.Recetas.Single(x => x.Activo && x.IdArticulo == ordenDb.IdArticulo).RecetaDetalles.Select(x => new CantidadInsumo() { Cantidad = x.Cantidad * ordenDb.CantidadTotal, IdInsumo = x.IdInsumo });
            //ordenDb.EtapaOrdenProduccion.RecetaDetalle.Select(x => new CantidadInsumo() { Cantidad = x.Cantidad, IdInsumo = x.IdInsumo });

            var insumosVerificados = await _fabricacionService.VerificarStockInsumos(insumosNecesarios);

            if (insumosVerificados.Any(x => x.CantidadDisponible < x.CantidadNecesaria))
                throw new ExcepcionControlada("No hay suficiente stock disponible para iniciar la etapa");

            await _fabricacionService.DescontarStockInsumosReservados(insumosNecesarios);
            
            await _db.SaveChangesAsync();
            
            await GuardarEventoAsync(ordenDb, comentario);

            await _hubContext.Clients.All.SendAsync("OrdenProduccionUpdate");
        }

        public async Task PausarEtapa(int idOrdenProduccion, string comentario)
        {
            //pausa la etapa si no estaba pausada
            var ordenDb = await _db.OrdenesProduccion.FindAsync(idOrdenProduccion);

            if (ordenDb.EstadoEtapaOrdenProduccion != EstadoEtapaOrdenProduccion.Iniciada)
                throw new ExcepcionControlada("La etapa de la orden debe encontrarse en estado Iniciada para poder ser Pausada");

            ordenDb.EstadoOrdenProduccion = EstadoOrdenProduccion.Pausada;
            ordenDb.EstadoEtapaOrdenProduccion = EstadoEtapaOrdenProduccion.Pausada;

            _db.OrdenesProduccion.Update(ordenDb);
            await _db.SaveChangesAsync();
            await GuardarEventoAsync(ordenDb, comentario);

            await _hubContext.Clients.All.SendAsync("OrdenProduccionUpdate");
        }

        public async Task ReanudarEtapa(int idOrdenProduccion, string comentario = "")
        {
            //reanuda la etapa si no estaba reanudada
            var ordenDb = await _db.OrdenesProduccion.FindAsync(idOrdenProduccion);

            if (ordenDb.EstadoEtapaOrdenProduccion != EstadoEtapaOrdenProduccion.Pausada)
                throw new ExcepcionControlada("La etapa de la orden debe encontrarse en estado Pausada para poder ser Reanudada");

            ordenDb.EstadoOrdenProduccion = EstadoOrdenProduccion.EnProceso;
            ordenDb.EstadoEtapaOrdenProduccion = EstadoEtapaOrdenProduccion.Iniciada;

            _db.OrdenesProduccion.Update(ordenDb);
            await _db.SaveChangesAsync();
            await GuardarEventoAsync(ordenDb, comentario);

            await _hubContext.Clients.All.SendAsync("OrdenProduccionUpdate");
        }

        public async Task FinalizarEtapa(int idOrdenProduccion, string comentario = "")
        {
            //pausa la etapa si no estaba pausada
            var ordenDb = await _db.OrdenesProduccion.FindAsync(idOrdenProduccion);

            if (ordenDb.EstadoEtapaOrdenProduccion != EstadoEtapaOrdenProduccion.Iniciada && ordenDb.EstadoEtapaOrdenProduccion != EstadoEtapaOrdenProduccion.Retrabajo)
                throw new ExcepcionControlada("La etapa de la orden debe encontrarse en estado Iniciada o retrabajo para poder ser Finalizada");

            ordenDb.EstadoOrdenProduccion = EstadoOrdenProduccion.EnProceso;
            ordenDb.EstadoEtapaOrdenProduccion = EstadoEtapaOrdenProduccion.Finalizada;

            _db.OrdenesProduccion.Update(ordenDb);
            await _db.SaveChangesAsync();
            await GuardarEventoAsync(ordenDb, comentario);

            
            await _hubContext.Clients.All.SendAsync("OrdenProduccionUpdate");
        }

        public async Task RetrabajarEtapa(int idOrdenProduccion, string comentario)
        {
            //marca la orden como retrabajo
            var ordenDb = await _db.OrdenesProduccion.FindAsync(idOrdenProduccion);

            if (ordenDb.EstadoEtapaOrdenProduccion != EstadoEtapaOrdenProduccion.Finalizada)
                throw new ExcepcionControlada("La etapa de la orden debe encontrarse en estado Finalizada para poder ser Retrabajada");

            ordenDb.EstadoOrdenProduccion = EstadoOrdenProduccion.EnProceso;
            ordenDb.EstadoEtapaOrdenProduccion = EstadoEtapaOrdenProduccion.Retrabajo;

            _db.OrdenesProduccion.Update(ordenDb);
            await _db.SaveChangesAsync();
            await GuardarEventoAsync(ordenDb, comentario);

            await _hubContext.Clients.All.SendAsync("OrdenProduccionUpdate");
        }

        //public async Task<bool> CompletarRetrabado(int idOrdenProduccion, string comentario)
        //{
        //    //pasa la etapa a finalizada
        //    var ordenDb = await _db.OrdenesProduccion.FindAsync(idOrdenProduccion);

        //    if (ordenDb.EstadoEtapaOrdenProduccion != EstadoEtapaOrdenProduccion.Retrabajo)
        //        throw new Exception("La etapa de la orden debe encontrarse en estado retrabajo para poder ser Completado");

        //    ordenDb.EstadoOrdenProduccion = EstadoOrdenProduccion.EnProceso;
        //    ordenDb.EstadoEtapaOrdenProduccion = EstadoEtapaOrdenProduccion.Finalizada;

        //    _db.OrdenesProduccion.Update(ordenDb);
        //    await _db.SaveChangesAsync();
        //    await GuardarEventoAsync(ordenDb, comentario);


        //    await _hubContext.Clients.All.SendAsync("OrdenProduccionUpdate");
        //    return true;
        //}

        public async Task AvanzarSiguienteEtapa(int idOrdenProduccion, string comentario = "")
        {
            var ordenDb = await _db.OrdenesProduccion.Include(x => x.EtapaOrdenProduccion).SingleOrDefaultAsync(x => x.Id == idOrdenProduccion);

            if (ordenDb.EstadoEtapaOrdenProduccion != EstadoEtapaOrdenProduccion.Finalizada)
                throw new ExcepcionControlada("La etapa de la orden debe encontrarse en estado Finalizada para poder avanzar a la siguiente etapa");

            var etapaActual = ordenDb.EtapaOrdenProduccion;
            var siguienteEtapa = _db.EtapasOrdenProduccion.Where(x => x.Orden > etapaActual.Orden).MinBy(x => x.Orden).SingleOrDefault();
            //var asd = _db.EtapasOrdenProduccion.Where(x => x.Orden > etapaActual.Orden).MinBy(x => x.Orden);
            //var siguienteEtapa = asd.Take(1).Single();

            if (siguienteEtapa == null)
                throw new ExcepcionControlada("La orden se ya encuentra en la ultima etapa");

            ordenDb.EtapaOrdenProduccion = siguienteEtapa;
            ordenDb.EstadoOrdenProduccion = EstadoOrdenProduccion.EnProceso;
            ordenDb.EstadoEtapaOrdenProduccion = EstadoEtapaOrdenProduccion.Pendiente;

            _db.OrdenesProduccion.Update(ordenDb);
            await _db.SaveChangesAsync();
            await GuardarEventoAsync(ordenDb, comentario);

            await _hubContext.Clients.All.SendAsync("OrdenProduccionUpdate");
        }

        public async Task FinalizarOrden(int idOrdenProduccion, string comentario = "")
        {
            //marca la orden como finalizada si esta todo bien
            var ordenDb = await _db.OrdenesProduccion.Include(x => x.EtapaOrdenProduccion).SingleOrDefaultAsync(x => x.Id == idOrdenProduccion);

            var etapaActual = ordenDb.EtapaOrdenProduccion;
            var siguienteEtapa = _db.EtapasOrdenProduccion.Where(x => x.Orden > etapaActual.Orden).MinBy(x => x.Orden).SingleOrDefault();

            if (siguienteEtapa != null)
                throw new ExcepcionControlada("Aun quedan etapas por realizar");

            if (ordenDb.EstadoEtapaOrdenProduccion != EstadoEtapaOrdenProduccion.Finalizada)
                throw new ExcepcionControlada("La etapa de la orden debe encontrarse en estado Finalizada para poder ser finalizar la Orden");

            ordenDb.EstadoOrdenProduccion = EstadoOrdenProduccion.Finalizada;
            ordenDb.EstadoEtapaOrdenProduccion = EstadoEtapaOrdenProduccion.Finalizada;

            _db.OrdenesProduccion.Update(ordenDb);
            await _db.SaveChangesAsync();
            await GuardarEventoAsync(ordenDb, comentario);

            await _hubContext.Clients.All.SendAsync("OrdenProduccionFinalizada", $"La Orden {idOrdenProduccion} fue marcada como finalizada", idOrdenProduccion);
            await _hubContext.Clients.All.SendAsync("OrdenProduccionUpdate");
        }

        public async Task CancelarOrden(int idOrdenProduccion, string comentario)
        {
            // TODO liberar todos los insumos que quedaban reservados => no se hace
            var ordenDb = await _db.OrdenesProduccion.FindAsync(idOrdenProduccion);
            ordenDb.EstadoOrdenProduccion = EstadoOrdenProduccion.Cancelada;
            ordenDb.EstadoEtapaOrdenProduccion = EstadoEtapaOrdenProduccion.Cancelada;

            _db.OrdenesProduccion.Update(ordenDb);

            await _fabricacionService.LiberarStockReservadoPendiente(idOrdenProduccion);

            await _db.SaveChangesAsync();
            await GuardarEventoAsync(ordenDb, comentario);

            await _hubContext.Clients.All.SendAsync("OrdenProduccionUpdate");
        }

        public async Task<OrdenProduccion> BuscarPorId(int idOrdenProduccion)
        {
            var orden = await _db.OrdenesProduccion
                .Include(x => x.Articulo)
                    .ThenInclude(x => x.ArticuloCategoria)
                .Include(x => x.EtapaOrdenProduccion)
                .Include(x => x.SolicitudDetalle)
                    .ThenInclude(x => x.Solicitud)
                    .ThenInclude(x => x.Sucursal)
                .Include(x => x.OrdenProduccionEventos)
                       .ThenInclude(x => x.EtapaOrdenProduccion)
                .Include(x => x.EtapaOrdenProduccion)
                .SingleOrDefaultAsync(x => x.Id == idOrdenProduccion);
            if (orden == null)
                throw new ExcepcionControlada("La orden de produccion solicitada no existe");

            return orden;
        }

        public async Task<IEnumerable<OrdenProduccion>> GetOrdenes()
        {
            var ordenesList = _db.OrdenesProduccion
                .Where(x => !x.Eliminado)
                .Include(x => x.Articulo)
                    .ThenInclude(x => x.ArticuloCategoria)
                .Include(x => x.EtapaOrdenProduccion)
                .Include(x => x.OrdenProduccionEventos)
                    .ThenInclude(x => x.EtapaOrdenProduccion)
                .Include(x => x.SolicitudDetalle)
                    .ThenInclude(x => x.Solicitud)
                    .ThenInclude(x => x.Sucursal)
                .OrderBy(x => x.EstadoOrdenProduccion == EstadoOrdenProduccion.Finalizada)
                .ThenBy(x => x.EstadoEtapaOrdenProduccion == EstadoEtapaOrdenProduccion.Cancelada)
                .ThenByDescending(x => x.FechaModificacion.HasValue)
                .ThenByDescending(x => x.FechaCreacion)
                .ToListAsync();

            _logger.LogInformation("Se buscaron las ordenes");
            return await ordenesList;
        }

        public async Task<IEnumerable<OrdenProduccionEvento>> GetOrdenEventos(int idOrdenProduccion)
        {
            return await _db.OrdenesProduccionEventos.OrderByDescending(x => x.FechaCreacion).ToListAsync();
        }

        public async Task<IEnumerable<EstadoOrdenProduccion>> GetEstadosOrden() 
        {
            return await Task.FromResult(EnumExtensions.GetValues<EstadoOrdenProduccion>());
        }

        public async Task<IEnumerable<EtapaOrdenProduccion>> GetEtapasOrden()
        {
            return await _db.EtapasOrdenProduccion.AsNoTracking().OrderBy(x => x.Orden).ToListAsync();
        }

        public async Task<IEnumerable<EstadoEtapaOrdenProduccion>> GetEstadoEtapasOrden()
        {
            return await Task.FromResult(EnumExtensions.GetValues<EstadoEtapaOrdenProduccion>());
        }

        public async Task<int> GetProgreso(int IdOrdenProduccion)
        {
            var orden = await _db.OrdenesProduccion.FindAsync(IdOrdenProduccion);

            var etapas = await _db.EtapasOrdenProduccion.OrderBy(x => x.Orden).Select(x => x.Id).ToListAsync();

            var idEtapaActual = orden.EtapaOrdenProduccion.Id;

            var progresoActual = etapas.IndexOf(idEtapaActual) + 1;

            var porcentaje = Math.DivRem(progresoActual * 100, etapas.Count(), out int rest);

            return porcentaje;
        }
        public async Task<TimeSpan> GetTiempoTranscurrido(int IdOrdenProduccion)
        {
            var orden = await _db.OrdenesProduccionEventos.FindAsync(IdOrdenProduccion);

            if (orden.EstadoOrdenProduccion != EstadoOrdenProduccion.Finalizada)
                return DateTime.Now - orden.FechaCreacion;

            var ultimoEvento = await _db.OrdenesProduccionEventos.Where(x => x.IdOrdenProduccion == IdOrdenProduccion).OrderByDescending(x => x.FechaCreacion).FirstOrDefaultAsync();

            return ultimoEvento.FechaCreacion - orden.FechaCreacion;
        }


        private async Task GuardarEventoAsync(OrdenProduccion orden, string comentario)
        {
            var evento = new OrdenProduccionEvento()
            {
                IdOrdenProduccion = orden.Id,
                IdEtapaOrdenProduccion = orden.IdEtapaOrdenProduccion,
                EstadoEtapaOrdenProduccion = orden.EstadoEtapaOrdenProduccion,
                EstadoOrdenProduccion = orden.EstadoOrdenProduccion,
                CantidadFabricada = orden.CantidadFabricada,
                Comentario = comentario
            };

            _db.OrdenesProduccionEventos.Add(evento);
            await _db.SaveChangesAsync();
        }


    }
}
