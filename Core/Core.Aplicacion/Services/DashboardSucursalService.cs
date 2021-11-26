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
                .Where(x => x.Venta.IdSucursal == idSucursal && x.Venta.FechaCreacion.Month == DateTime.UtcNow.Month)
                .Include(x => x.Articulo)
                .Select(x => new { IdArticulo = x.IdArticulo, Cantidad = x.Cantidad })
                .ToListAsync();

            var masVendidosList = masVendidos
                .GroupBy(x => x.IdArticulo)
                .Select(x => new Tuple<int, int>(x.Key, x.Sum(x => x.Cantidad)))
 
                .Take(n);


            var articulos = await _db.Articulos
                    .Include(x => x.ArticuloCategoria)
                    .ToListAsync();

            IList<Tuple<Articulo, int>> result = new List<Tuple<Articulo, int>>();

            foreach (var item in masVendidosList)
            {
                var articulo = articulos.Single(x => x.Id == item.Item1);

                result.Add(new Tuple<Articulo, int>(articulo, item.Item2));
            }

            result = result.OrderByDescending(x => x.Item2).ThenBy(x => x.Item1.Nombre).ToList();

            return result;
        }

        public async Task<IEnumerable<ArticuloStock>> GetArticulosBajoStock(int n)
        {
            int idSucursal = int.Parse(_db.GetSucursalId());
            if (n == -1)
            {
                n = _db.ArticulosStock.Count();
            }
            var stockBajo = await _db.ArticulosStock
                .Where(x => !x.Eliminado)
                .AsNoTracking()
                .Where(x => x.IdSucursal == idSucursal && x.StockMinimo * 1.5 >= x.StockTotal)
                .Include(x => x.Articulo)
                    .ThenInclude(x => x.ArticuloCategoria)
                .OrderBy(x => x.StockTotal - x.StockMinimo)
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
