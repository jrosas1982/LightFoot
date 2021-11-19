using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Aplicacion.Interfaces;
using Core.Dominio.AggregatesModel;
using Core.Infraestructura;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Core.Aplicacion.Services
{
    public class ArticuloCategoriaService : IArticuloCategoriaService
    {
        private readonly AppDbContext _db;
        private readonly ILogger<UsuarioService> _logger;

        public ArticuloCategoriaService(AppDbContext db, ILogger<UsuarioService> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<IEnumerable<ArticuloCategoria>> GetCategorias()
        {
            var categoriasList = await _db.ArticulosCategoria
                .Where(x => !x.Eliminado)
                .OrderByDescending(x => x.Descripcion)
                .ToListAsync();
            return categoriasList;
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

                categoriaDb.Eliminado = true;

                _db.Update(categoriaDb);
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
