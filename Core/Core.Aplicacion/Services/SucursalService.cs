using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Aplicacion.Interfaces;
using Core.Dominio.AggregatesModel;
using Core.Infraestructura;
using Microsoft.EntityFrameworkCore;

namespace Core.Aplicacion.Services
{
    public class SucursalService : ISucursalService
    {
        private readonly AppDbContext _db;

        public SucursalService(ExtendedAppDbContext extendedAppDbContext)
        {
            _db = extendedAppDbContext.context;
        }

        public async Task<IEnumerable<Sucursal>> GetSucursales()
        {
            var sucursalesList = await _db.Sucursales.ToListAsync();
            return sucursalesList;
        }

        public async Task<Sucursal> BuscarPorId(int IdSucursal)
        {
            var sucursal = await _db.Sucursales.FindAsync(IdSucursal);
            return sucursal;
        }

        public async Task CrearSucursal(Sucursal sucursal)
        {
            _db.Add(sucursal);
            await _db.SaveChangesAsync();
        }

        public async Task EditarSucursal(Sucursal sucursal)
        {
            var sucursalDb = await _db.Sucursales.FindAsync(sucursal.Id);

            sucursalDb.Nombre = sucursal.Nombre;
            sucursalDb.Activo = sucursal.Activo;
            sucursalDb.Direccion = sucursal.Direccion;
            sucursalDb.Descripcion = sucursal.Descripcion;

            _db.Update(sucursalDb);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> EliminarSucursal(Sucursal sucursal)
        {
            try
            {
                var sucursalDb = await _db.Sucursales.FindAsync(sucursal.Id);

                _db.Remove(sucursalDb);
                await _db.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
