using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Dominio.AggregatesModel;


namespace Core.Aplicacion.Interfaces
{
    public interface IArticuloCategoriaService
    {
        public Task<IEnumerable<ArticuloCategoria>> GetCategorias();
        public Task<ArticuloCategoria> BuscarPorId(int IdCategoria);
        public Task CrearCategoria(ArticuloCategoria categoria);
        public Task EditarCategoria(ArticuloCategoria categoria);
        public Task<bool> EliminarCategoria(ArticuloCategoria categoria);
    }
}
