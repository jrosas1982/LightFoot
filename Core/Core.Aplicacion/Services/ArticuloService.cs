using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Aplicacion.Helpers;
using Core.Aplicacion.Interfaces;
using Core.Dominio.AggregatesModel;
using Core.Infraestructura;
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
            var articulosList = _db.Articulos;
            _logger.LogInformation("Se buscaron los articulos");
            return await Task.FromResult(articulosList);
        }

        public async Task<Articulo> BuscarPorId(int IdArticulo)
        {
            var articulo = await _db.Articulos.FindAsync(IdArticulo);
            return articulo;
        }

        public async Task CrearArticulo(Articulo articulo)
        {
            //if (_db.Usuarios.Any(x => x.NombreUsuario == usuario.NombreUsuario))
            //    throw new Exception($"El nombre de usuario {usuario.NombreUsuario} ya existe");

            //if (string.IsNullOrEmpty(usuario.Contraseña))
            //    throw new Exception($"La contraeña no puede estar vacia");           

            _db.Add(articulo);
            await _db.SaveChangesAsync();
            _logger.LogInformation($"Se creó el articulo nombre: {articulo.Nombre}");
        }

        public async Task EditarArticulo(Articulo articulo)
        {
            //if (string.IsNullOrEmpty(usuario.Contraseña))
            //    throw new Exception($"La contraeña no puede estar vacia");

            var articuloDb = await _db.Articulos.FindAsync(articulo.Id);

            articulo.CodigoArticulo = articulo.CodigoArticulo;
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
                var articuloDb = await _db.Usuarios.FindAsync(articulo.Id);

                _db.Remove(articuloDb);
                await _db.SaveChangesAsync();

                return true;
            }
            catch
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
