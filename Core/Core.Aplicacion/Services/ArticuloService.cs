using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Aplicacion.Helpers;
using Core.Aplicacion.Interfaces;
using Core.Dominio.AggregatesModel;
using Core.Infraestructura;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Core.Aplicacion.Services
{
    public class ArticuloService : IArticuloService
    {
        private readonly AppDbContext _db;
        private readonly ILogger<ArticuloService> _logger;

        public ArticuloService(ExtendedAppDbContext extendedAppDbContext, ILogger<ArticuloService> logger)
        {
            _db = extendedAppDbContext.context;
            _logger = logger;
        }

        public async Task<IEnumerable<Articulo>> GetArticulos()
        {
            var articulosList = await _db.Articulos
                .Include(x => x.ArticuloCategoria)
                .OrderBy(x => x.ArticuloCategoria.Descripcion)
                .ToListAsync();
            _logger.LogInformation("Se buscaron los articulos");
            return articulosList;
        }

        public async Task<IEnumerable<Articulo>> GetArticulosFabrica()
        {
            var articulosList = await _db.Articulos
                .Where(x => x.ProveedoresArticulo.Any(z => z.Proveedor.EsFabrica == true))
                .Include(x => x.ArticuloCategoria)
                .OrderBy(x => x.ArticuloCategoria.Descripcion)
                .ToListAsync();
            _logger.LogInformation("Se buscaron los articulos");
            return articulosList;
        }

        public async Task<Articulo> BuscarPorId(int IdArticulo)
        {
            var articulo = await _db.Articulos.FindAsync(IdArticulo);
            return articulo;
        }

        public async Task CrearArticulo(Articulo articulo)
        {
            if (_db.Articulos.Any(x => x.Nombre == articulo.Nombre && x.Color == articulo.Color && x.TalleArticulo == articulo.TalleArticulo))
                throw new Exception($"El articulo ya existe");

            //if (string.IsNullOrEmpty(usuario.Contraseña))
            //    throw new Exception($"La contraeña no puede estar vacia");           

            _db.Articulos.Add(articulo);
            await _db.SaveChangesAsync();
            _logger.LogInformation($"Se creó el articulo nombre: {articulo.Nombre}");
        }

        public async Task EditarArticulo(Articulo articulo)
        {
            //if (string.IsNullOrEmpty(usuario.Contraseña))
            //    throw new Exception($"La contraeña no puede estar vacia");

            var articuloDb = await _db.Articulos.FindAsync(articulo.Id);

            articuloDb.CodigoArticulo = articulo.CodigoArticulo;
            articuloDb.Nombre = articulo.Nombre;
            articuloDb.ArticuloEstado = articulo.ArticuloEstado;
            articuloDb.TalleArticulo = articulo.TalleArticulo;
            articuloDb.Descripcion = articulo.Descripcion;
            articuloDb.Color = articulo.Color;

            _db.Update(articuloDb);
            await _db.SaveChangesAsync();

        }

        public async Task<bool> EliminarArticulo(Articulo articulo)
        {
            try
            {
                var articuloDb = await _db.Articulos.FindAsync(articulo.Id);

                _db.Remove(articuloDb);
                await _db.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<ArticuloEstado>> GetArticuloEstados()
        {
            return await Task.FromResult(EnumExtensions.GetValues<ArticuloEstado>());
        }

    }
}
