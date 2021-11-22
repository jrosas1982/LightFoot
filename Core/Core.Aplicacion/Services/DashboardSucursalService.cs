using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Aplicacion.Interfaces;
using Core.Dominio.AggregatesModel;
using Core.Infraestructura;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<Tuple<Articulo, int>>> GetMasVendidos(int n)
        {
            int idSucursal = int.Parse(_db.GetSucursalId());

            var masVendidos = await _db.VentasDetalle
                .Where(x => !x.Eliminado)
                .AsNoTracking()
                .Where(x => x.Venta.IdSucursal == idSucursal && x.Venta.FechaCreacion > DateTime.UtcNow.AddDays(-30))
                .Include(x => x.Articulo)
                    .ThenInclude(x => x.ArticuloCategoria)
                .Select(x => new { Articulo = x.Articulo, Cantidad = x.Cantidad })
                .ToListAsync();

            var masVendidosList = masVendidos
                .GroupBy(x => x.Articulo)
                .Select(x => new Tuple<Articulo, int>(x.Key, x.Sum(x => x.Cantidad)))
                .OrderByDescending(x => x.Item2)
                .ThenBy(x => x.Item1.Nombre)
                .Take(n);

            return masVendidosList;
        }

        public async Task<IEnumerable<ArticuloStock>> GetArticulosBajoStock(int n)
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
                .Take(n)
                .ToListAsync();

            return stockBajo;

        }

        public async Task<IEnumerable<CajaSucursal>> GetUltimosMovimientos(int n)
        {
            int idSucursal = int.Parse(_db.GetSucursalId());

            var ultimosMovimientos = await _db.CajaSucursales
                .Where(x => !x.Eliminado)
                .AsNoTracking()
                .Where(x => x.IdSucursal == idSucursal)
                .OrderByDescending(x => x.FechaModificacion)
                .ThenByDescending(x => x.FechaCreacion)
                .Take(n)
                .ToListAsync();

            return ultimosMovimientos;
        }

        public async Task<IEnumerable<Venta>> GetUltimasVentas(int n)
        {
            int idSucursal = int.Parse(_db.GetSucursalId());

            var ultimasVentas = await _db.Ventas
                .Where(x => !x.Eliminado)
                .AsNoTracking()
                .Where(x => x.IdSucursal == idSucursal)
                .Include(x => x.Cliente)
                .Include(x => x.VentaDetalles)
                .OrderByDescending(x => x.FechaModificacion)
                .ThenByDescending(x => x.FechaCreacion)
                .Take(n)
                .ToListAsync();

            return ultimasVentas;
        }
    }
}
