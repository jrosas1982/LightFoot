using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Aplicacion.Helpers;
using Core.Aplicacion.Interfaces;
using Core.Dominio.AggregatesModel;
using Core.Infraestructura;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Core.Aplicacion.Services
{
    public class AdministracionDeCajaService : IAdministracionDeCajaService
    {
        private readonly AppDbContext _db;
        private readonly ILogger<AdministracionDeCajaService> _logger;

        public AdministracionDeCajaService(AppDbContext db, ILogger<AdministracionDeCajaService> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task AgregarMovimiento(CajaSucursal cajaSucursal)
        {
            _db.CajaSucursales.Add(cajaSucursal);

            await _db.SaveChangesAsync();
        }

        public async Task<CajaSucursal> BuscarPorId(int idCajaSucursal)
        {
            int idSucursal = int.Parse(_db.GetSucursalId());

            var movimiento = await _db.CajaSucursales
                .Where(x => x.IdSucursal == idSucursal)
                .Include(x => x.Sucursal)
                .SingleOrDefaultAsync(x => x.Id == idCajaSucursal);
                
            return movimiento;
        }

        public async Task<IEnumerable<CajaSucursal>> GetMovimientos()
        {
            int idSucursal = int.Parse(_db.GetSucursalId());

            var movimientosList = await _db.CajaSucursales
                .Where(x => x.IdSucursal == idSucursal)
                .Include(x => x.Sucursal)
                .OrderByDescending(x => x.FechaModificacion)
                .ThenByDescending(x => x.FechaCreacion)                
                .ToListAsync();

            return movimientosList;
        }
    }
}
