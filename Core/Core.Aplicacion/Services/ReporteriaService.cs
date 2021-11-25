using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Aplicacion.Interfaces;
using Core.Dominio.AggregatesModel;
using Core.Dominio.CoreModelHelpers;
using Core.Infraestructura;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Core.Aplicacion.Services
{
    public class ReporteriaService : IReporteriaService
    {
        private readonly AppDbContext _db;
        private readonly ILogger<ReporteriaService> _logger;

        public ReporteriaService(AppDbContext db, ILogger<ReporteriaService> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<IEnumerable<RankingVentasPorSucursalModel>> GetRankingVentasPorSucursal(PeriodoModel periodo)
        {
            var ventasDb = await _db.Ventas
                .Where(x => !x.Eliminado)
                .Where(x => x.FechaCreacion.Month == periodo.Mes && x.FechaCreacion.Year == periodo.Año)              
                .ToListAsync();

            var ventas = ventasDb.GroupBy(x => x.IdSucursal);

            var sucursales = await _db.Sucursales.Where(x => !x.Eliminado).ToListAsync();

            var ranking = sucursales.Select(x => new RankingVentasPorSucursalModel()
            {
                Sucursal = x,
                Ventas = ventas.SingleOrDefault(v => v.Key == x.Id),
            });

            ranking = ranking.OrderByDescending(x => x.Ventas?.Sum(x => x.MontoTotal));

            return ranking;
        }

        public async Task<IEnumerable<RankingVentasPorUsuarioModel>> GetRankingVentasPorUsuario(PeriodoModel periodo)
        {
            var ventasDb = await _db.Ventas
                .Where(x => !x.Eliminado)
                .Where(x => x.FechaCreacion.Month == periodo.Mes && x.FechaCreacion.Year == periodo.Año)               
                .ToListAsync();

            var ventas = ventasDb.GroupBy(x => x.UsuarioVenta);

            var usuarios = await _db.Usuarios
                .Where(x => !x.Eliminado)
                .Where(x => x.UsuarioRol == UsuarioRol.Vendedor 
                         || x.UsuarioRol == UsuarioRol.Cajero
                         || x.UsuarioRol == UsuarioRol.Encargado
                         || x.UsuarioRol == UsuarioRol.Administrador)
                .ToListAsync();

            var ranking = usuarios.Select(x => new RankingVentasPorUsuarioModel()
            {
                Usuario = x,
                Ventas = ventas.SingleOrDefault(v => v.Key == x.NombreUsuario),
            });

            ranking = ranking.OrderByDescending(x => x.Ventas?.Sum(x => x.MontoTotal));

            return ranking;
        }
    }
}
