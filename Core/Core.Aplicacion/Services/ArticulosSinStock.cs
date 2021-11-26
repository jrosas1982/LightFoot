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
    public class ArticuloSinStockService : IArticulosSinStockService
    {
        private readonly AppDbContext _db;
        private readonly ILogger<ArticuloService> _logger;
        private readonly IConfiguration _Configuration;

        public ArticuloSinStockService(AppDbContext db, ILogger<ArticuloService> logger, IConfiguration configuration)
        {
            _db = db;
            _logger = logger;
            _Configuration = configuration;
        }

        public async Task<IEnumerable<ArticuloStock>> GetArticulosSinStock()
        {
            int idSucursal = int.Parse(_db.GetSucursalId());

            var stockBajo = await _db.ArticulosStock
                .Where(x => !x.Eliminado)
                .AsNoTracking()
                .Where(x => x.IdSucursal == idSucursal && x.StockTotal == 0)
                .Include(x => x.Articulo)
                    .ThenInclude(x => x.ArticuloCategoria)
                .OrderByDescending(x => x.FechaModificacion)
                .ThenBy(x => x.Articulo.Nombre)
                .ToListAsync();

            return stockBajo;
        }
    }
}
