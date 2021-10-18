using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Dominio.AggregatesModel;

namespace Core.Aplicacion.Interfaces
{
    public interface IUsuarioService
    {
        public Task<IEnumerable<Usuario>> GetUsuarios();
        public Task<Usuario> BuscarPorId(int IdUsuario);
        public Task CrearUsuario(Usuario usuario);
        public Task EditarUsuario(Usuario usuario);
        public Task<bool> EliminarUsuario(Usuario usuario);
        public Task<IEnumerable<UsuarioRol>> GetUsuarioRoles();
    }
}
