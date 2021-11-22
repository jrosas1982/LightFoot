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
            var sucursal = await _db.Sucursales.Where(x => x.Id == idSucursal).SingleOrDefaultAsync(x => x.Id == idSucursal);
            return sucursal;
        }

        public Task<IEnumerable<Articulo>> Get5MasVendidos()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ArticuloStock>> GetArticulosBajoStock()
        {
            int idSucursal = int.Parse(_db.GetSucursalId());
            var stockBajo = await _db.ArticulosStock
                .Include(x => x.Articulo)
                    .ThenInclude(x => x.ArticuloCategoria)
                .Where(x => x.IdSucursal == idSucursal && x.StockTotal <= x.StockMinimo)
                .OrderByDescending(x => x.StockMinimo)
                .ThenBy(x => x.TamañoLote)
                .Take(2)
                .ToListAsync();

            return stockBajo;

        }
    }
}
