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
    public class DashboardSucursalService : IDashboardSucursalService
    {
        private readonly AppDbContext _db;
        private readonly ILogger<ProveedorService> _logger;

        public DashboardSucursalService(AppDbContext db, ILogger<ProveedorService> logger)
        {
            _db = db;
            _logger = logger;
        }
        public async Task<Sucursal> GetSucursal()
        {
            int idSucursal = int.Parse(_db.GetSucursalId());
            var sucursal = await _db.Sucursales.SingleOrDefaultAsync(x => x.Id == idSucursal);
            return sucursal;
        }

        public async Task<IEnumerable<Tuple<Articulo, int>>> Get5MasVendidos()
        {
            int idSucursal = int.Parse(_db.GetSucursalId());

            var masVendidos = await _db.VentasDetalle
                .Where(x => !x.Eliminado)
                .AsNoTracking()
                .Where(x => x.Venta.IdSucursal == idSucursal)
                .Include(x => x.Articulo)
                    .ThenInclude(x => x.ArticuloCategoria)
                .Select(x => new { Articulo = x.Articulo, Cantidad = x.Cantidad })
                .ToListAsync();

            var masVendidosList = masVendidos
                .GroupBy(x => x.Articulo)
                .Select(x => new Tuple<Articulo, int>(x.Key, x.Sum(x => x.Cantidad)))
                .OrderByDescending(x => x.Item2)
                .ThenBy(x => x.Item1.Nombre)
                .Take(5);

            return masVendidosList;
        }

        public async Task<IEnumerable<ArticuloStock>> GetArticulosBajoStock()
        {
            int idSucursal = int.Parse(_db.GetSucursalId());

            var stockBajo = await _db.ArticulosStock
                .Where(x => !x.Eliminado)
                .AsNoTracking()
                .Where(x => x.IdSucursal == idSucursal && x.StockMinimo * 1.5 >= x.StockTotal)
                .Include(x => x.Articulo)
                    .ThenInclude(x => x.ArticuloCategoria)
                .OrderByDescending(x => x.StockTotal - x.StockMinimo)
                .ThenBy(x => x.Articulo.Nombre)
                .Take(5)
                .ToListAsync();

            return stockBajo;

        }
    }
}
