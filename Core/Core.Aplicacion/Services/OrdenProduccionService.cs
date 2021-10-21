using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Aplicacion.Helpers;
using Core.Aplicacion.Interfaces;
using Core.Dominio.AggregatesModel;
using Core.Infraestructura;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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


        public async Task<bool> AvanzarEtapa(int idOrdenProduccion)
        {
            //
            throw new NotImplementedException();
        }

        public async Task<bool> PausarEtapa(int idOrdenProduccion)
        {
            //pausa la etapa si no estaba pausada
            throw new NotImplementedException();
        }

        public async Task<bool> ReanudarEtapa(int idOrdenProduccion)
        {
            //reanuda la etapa si no estaba reanudada
            throw new NotImplementedException();
        }

        public async Task<bool> RetrabajarEtapa(int idOrdenProduccion)
        {
            //marca la orden como retrabajo
            throw new NotImplementedException();
        }

        public async Task<bool> FinalizarOrden(int idOrdenProduccion)
        {
            //marca la orden como finalizada si esta todo bien
            throw new NotImplementedException();
        }

        public async Task<bool> CancelarOrden(int idOrdenProduccion)
        {
            // cancelar orden y liberar todos los insumos que quedaban reservados
            throw new NotImplementedException();
        }


        public async Task<OrdenProduccion> BuscarPorId(int idOrdenProduccion)
        {
            var orden = await _db.OrdenesProduccion.Include(x => x.OrdenProduccionEventos).SingleOrDefaultAsync(x => x.Id == idOrdenProduccion);
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
