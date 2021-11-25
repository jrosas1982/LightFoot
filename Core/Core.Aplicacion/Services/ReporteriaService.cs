using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Aplicacion.Interfaces;
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
            var ventas = await _db.Ventas
                .Where(x => !x.Eliminado)
                .Where(x => x.FechaCreacion.Month == periodo.Mes && x.FechaCreacion.Year == periodo.Año)
                .GroupBy(x => x.IdSucursal)
                .ToListAsync();
            var sucursales = await _db.Sucursales.ToListAsync();

            var ranking = sucursales.Select(x => new RankingVentasPorSucursalModel()
            {
                Sucursal = x,
                Ventas = ventas.SingleOrDefault(v => v.Key == x.Id),
            });

            return ranking;
        }

        public async Task<IEnumerable<RankingVentasPorUsuarioModel>> GetRankingVentasPorUsuario(PeriodoModel periodo)
        {
            var ventas = await _db.Ventas
                .Where(x => !x.Eliminado)
                .Where(x => x.FechaCreacion.Month == periodo.Mes && x.FechaCreacion.Year == periodo.Año)
                .GroupBy(x => x.UsuarioVenta)
                .ToListAsync();
            var usuarios = await _db.Usuarios.ToListAsync();

            var ranking = usuarios.Select(x => new RankingVentasPorUsuarioModel()
            {
                Usuario = x,
                Ventas = ventas.SingleOrDefault(v => v.Key == x.NombreUsuario),
            });

            return ranking;
        }
    }
}
