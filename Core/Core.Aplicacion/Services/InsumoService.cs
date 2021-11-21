using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Aplicacion.Interfaces;
using Core.Dominio;
using Core.Dominio.AggregatesModel;
using Core.Infraestructura;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Core.Aplicacion.Services
{
    public class InsumoService : IInsumoService
    {
        private readonly AppDbContext _db;
        private readonly ILogger<InsumoService> _logger;
        public InsumoService(AppDbContext db, ILogger<InsumoService> logger)
        {
            _logger = logger;
            _db = db;
        }

        public async Task<Insumo> BuscarPorId(int IdInsumo)
        {
            var insumo = await _db.Insumos.FindAsync(IdInsumo);

            if (insumo == null)
                throw new ExcepcionControlada("El insumo solicitado no existe");
            
            return insumo;
        }

        public async Task CrearInsumo(Insumo insumo)
        {
            _db.Insumos.Add(insumo);
            await _db.SaveChangesAsync();
            _logger.LogInformation($"Se creo el insumo con nombre: {insumo.Nombre}");
        }

        public async Task EditarInsumo(Insumo insumo)
        {
            var insumoDb = await _db.Insumos.FindAsync(insumo.Id);
            insumoDb.Nombre = insumo.Nombre;
            insumoDb.Descripcion = insumo.Descripcion;
            insumoDb.Unidad = insumo.Unidad;
            insumoDb.IdProveedorPreferido = insumo.IdProveedorPreferido;
            insumoDb.StockTotal = insumo.StockTotal;

            _db.Insumos.Update(insumoDb);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> EliminarInsumo(Insumo insumo)
        {
            try
            {

                var entidad = await _db.Insumos.FindAsync(insumo.Id);

                entidad.Eliminado = true;

                _db.Insumos.Update(entidad);
                await _db.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<Insumo>> GetInsumos()
        {
            var insumosList = await _db.Insumos.AsNoTracking().Where(x => !x.Eliminado).Include(x => x.Proveedor).OrderByDescending(x => x.Nombre).ToListAsync();
            _logger.LogInformation("Se buscaron los insumos");
            return insumosList;
        }

        public async Task<IEnumerable<Proveedor>> GetProveedoresInsumo(int idInsumo)
        {
            var proveedoresList = await _db.Proveedores.Where(x => !x.Eliminado).Where(x => x.ProveedorInsumos.Any(x => x.IdInsumo == idInsumo)).ToListAsync();
            return proveedoresList;
        }
    }
}
