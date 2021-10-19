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

        public async Task<IEnumerable<EstadoEtapaOrdenProduccion>> GetEstadosEtapaOrden()
        {
            return await Task.FromResult(EnumExtensions.GetValues<EstadoEtapaOrdenProduccion>());
        }
        public async Task<IEnumerable<EstadoOrdenProduccion>> GetEstadosOrden()
        {
            return await Task.FromResult(EnumExtensions.GetValues<EstadoOrdenProduccion>());
        }
        public async Task<IEnumerable<EtapaOrdenProduccion>> GetEtapasOrden()
        {
            return await _db.EtapasOrdenProduccion.ToListAsync();
        }

        public Task<OrdenProduccion> BuscarPorId(int idOrdenProduccion) => throw new NotImplementedException();

        public Task<bool> SetEstadoEtapaOrdenProduccion(int IdOrdenProduccion, EstadoEtapaOrdenProduccion estadoEtapaOrdenProduccion) => throw new NotImplementedException();
        public Task<bool> SetEtapaOrdenProduccion(int idOrdenProduccion, int idEtapaOrdenProduccion) => throw new NotImplementedException();
        public Task<bool> SetEtapaOrdenProduccion(int idOrdenProduccion, EtapaOrdenProduccion etapaOrdenProduccion) => throw new NotImplementedException();
    }
}
