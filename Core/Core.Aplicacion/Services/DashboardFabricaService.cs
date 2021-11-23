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

        public async Task<IEnumerable<Tuple<Sucursal, int>>> GetTopSucursalesSolicitudes(int n)
        {
            int idSucursal = int.Parse(_db.GetSucursalId());

            var masSolicitudes = await _db.Solicitudes
                .Where(x => !x.Eliminado)
                .AsNoTracking()
                .GroupBy(x => x.Sucursal)
                .Select(x => new Tuple<Sucursal, int>(x.Key, x.Count()))
                .Take(n)
                .ToListAsync();

            // var masVendidos = await _db.VentasDetalle
            //    .Where(x => !x.Eliminado)
            //    .AsNoTracking()
            //    .Where(x => x.Venta.IdSucursal == idSucursal && x.Venta.FechaCreacion > DateTime.UtcNow.AddDays(-30))
            //    .Include(x => x.Articulo)
            //        .ThenInclude(x => x.ArticuloCategoria)
            //    .Select(x => new { Articulo = x.Articulo, Cantidad = x.Cantidad })
            //    .ToListAsync();

            //var masVendidosList = masVendidos
            //    .GroupBy(x => x.Articulo)
            //    .Select(x => new Tuple<Articulo, int>(x.Key, x.Sum(x => x.Cantidad)))
            //    .OrderByDescending(x => x.Item2)
            //    .ThenBy(x => x.Item1.Nombre)
            //    .Take(n);

            return masSolicitudes;


        }

        public async Task<IEnumerable<Tuple<EtapaOrdenProduccion, int>>> GetAvanceProduccion(DateTime fecha, TimeSpan plazoTiempo)
        {
            var start = fecha;
            var end = fecha.Add(-plazoTiempo);

            var ordenesPorEtapa = await _db.OrdenesProduccion
                .Where(x => !x.Eliminado)
                .AsNoTracking()
                .Where(x => (x.FechaCreacion.Date > start.Date) && (x.FechaCreacion.Date < end.Date))
                .GroupBy(x => x.EtapaOrdenProduccion)
                .Select(x => new Tuple<EtapaOrdenProduccion, int>(x.Key, x.Count()))
                .ToListAsync();

            return ordenesPorEtapa;
        }



        //await _hubContext.Clients.All.SendAsync("FabricaDashboardUpdate");


    }
}
