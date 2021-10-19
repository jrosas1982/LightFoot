using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Aplicacion.Interfaces;
using Core.Dominio.AggregatesModel;
using Core.Infraestructura;
using Microsoft.Extensions.Logging;

namespace Core.Aplicacion.Services
{
    public class ArticuloCategoriaService : IArticuloCategoriaService
    {
        private readonly AppDbContext _db;
        private readonly ILogger<UsuarioService> _logger;

        public ArticuloCategoriaService(ExtendedAppDbContext extendedAppDbContext, ILogger<UsuarioService> logger)
        {
            _db = extendedAppDbContext.context;
            _logger = logger;
        }

        public async Task<IEnumerable<ArticuloCategoria>> GetCategorias()
        {
            var categoriasList = _db.ArticulosCategoria;
            return await Task.FromResult(categoriasList);
        }

        public async Task<ArticuloCategoria> BuscarPorId(int IdCategoria)
        {
            var categoria = await _db.ArticulosCategoria.FindAsync(IdCategoria);
            return categoria;
        }

        public async Task CrearCategoria(ArticuloCategoria categoria)
        {
            _db.Add(categoria);
            await _db.SaveChangesAsync();
        }

        public async Task EditarCategoria(ArticuloCategoria categoria)
        {
            var categoriaDb = await _db.ArticulosCategoria.FindAsync(categoria.Id);

            categoriaDb.Descripcion = categoria.Descripcion;
            categoriaDb.Comentario = categoria.Comentario;
            categoriaDb.Activo = categoria.Activo;

            _db.Update(categoriaDb);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> EliminarCategoria(ArticuloCategoria categoria)
        {
            try
            {
                var categoriaDb = await _db.ArticulosCategoria.FindAsync(categoria.Id);
                _db.Remove(categoriaDb);
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
