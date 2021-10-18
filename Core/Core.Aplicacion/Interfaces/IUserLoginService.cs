using System.Threading.Tasks;
using Core.Aplicacion.Auth;
using Core.Dominio.AggregatesModel;

namespace Core.Aplicacion.Interfaces
{
    public interface IUserLoginService
    {
        Task<Usuario> ValidarUsuario(UserLoginDTO usuario);
    }
}
