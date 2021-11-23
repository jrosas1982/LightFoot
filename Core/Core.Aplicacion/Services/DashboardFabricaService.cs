using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Aplicacion.Helpers;
using Core.Aplicacion.Interfaces;
using Core.Dominio;
using Core.Dominio.AggregatesModel;
using Core.Dominio.CoreModelHelpers;
using Core.Infraestructura;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Core.Aplicacion.Services
{
    public class DashboardFabricaService : IDashboardFabricaService
    {

        private readonly AppDbContext _db;
        private readonly ILogger<ProveedorService> _logger;

        public DashboardFabricaService(AppDbContext db, ILogger<ProveedorService> logger)
        {
            _db = db;
            _logger = logger;
        }


        public async Task<IEnumerable<Solicitud>> GetSolicitudes()
        {
            var solicitudes = await _db.Solicitudes.Where(x => !x.Eliminado).ToListAsync();

            return solicitudes;
        }

        public async Task<IEnumerable<OrdenProduccion>> GetOrdenes()
        {
            var ordenes = await _db.OrdenesProduccion.Where(x => !x.Eliminado).ToListAsync();

            return ordenes;
        }
        public async Task<IEnumerable<Insumo>> GetInsumosBajoStock(int n)
        {
            int idSucursal = int.Parse(_db.GetSucursalId());

            var stockBajo = await _db.Insumos
                .Where(x => !x.Eliminado)
                .AsNoTracking()
                //.Where(x => x.StockTotal >= x.StockTotal * 0.25)
                .OrderByDescending(x => x.StockTotal)
                .Take(n)
                .ToListAsync();

            return stockBajo;
        }
        public async Task<IEnumerable<Tuple<int, int>>> RankingSucursales()
        {
            var solicitudes = await _db.Solicitudes.Where(x => !x.Eliminado).ToListAsync();

            //var RankingSucursales = solicitudes.GroupBy(x => x.IdSucursal).Select(sp => new { 
            //    IdSucursal = sp.Key,
            //    NombreSucursal = solicitudes.Select(x => x.Sucursal.Nombre),
            //    CantPedidos = sp.Count()
            //}).ToList();
            var masVendidos = await _db.Solicitudes
                         .Where(x => !x.Eliminado)
                         .AsNoTracking()
                         .Where(x => x.FechaCreacion > DateTime.UtcNow.AddDays(-30))
                         .Select(x => new { Solicitud = x , Cantidad = x })
                         .ToListAsync();

            var masVendidosList = masVendidos
                .GroupBy(x => x.Solicitud.IdSucursal)
                .Select(x => new Tuple<int, int>(x.Key, x.Count()))
                .OrderByDescending(x => x.Item2)
                .ThenBy(x => x.Item1);

            return masVendidosList;
        }

        public async Task<int> GetSolicitudesRecibidas(DateTime fecha)
        {
            var response = await _db.Solicitudes
                .Where(x => !x.Eliminado)
                .Where(x => x.FechaCreacion.Date == fecha.Date)
                .CountAsync();

            return response;
        }

        public async Task<int> GetSolicitudesRecibidas(DateTime fecha, TimeSpan plazoTiempo)
        {
            var start = fecha;
            var end = fecha.Add(-plazoTiempo);

            var response = await _db.Solicitudes
                    .Where(x => !x.Eliminado)
                    .Where(x => (x.FechaCreacion.Date > start.Date) && (x.FechaCreacion.Date < end.Date))
                    .CountAsync();

            return response;
        }

        public Task<int> GetOrdenesProduccionFinalizadas(DateTime fecha)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetOrdenesProduccionFinalizadas(DateTime fecha, TimeSpan plazoTiempo)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetOrdenesProduccionEnExpedicion(DateTime fecha)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetOrdenesProduccionEnExpedicion(DateTime fecha, TimeSpan plazoTiempo)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Tuple<Sucursal, int>>> GetTopSucursalesSolicitudes(int n)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Tuple<EtapaOrdenProduccion, int>>> GetAvanceProduccion(TimeSpan plazoTiempo)
        {
            throw new NotImplementedException();
        }



        //await _hubContext.Clients.All.SendAsync("FabricaDashboardUpdate");


    }
}
