using System;
using System.Security.Claims;
using System.Security.Principal;
using Core.Dominio.AggregatesModel;

namespace Web.Site.Helpers
{
    public static class IdentityExtensions
    {
        public static string GetUserNombreApellido(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst("NombreApellido");
            return claim?.Value;
        }
        public static string GetUserNombreSucursal(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst("NombreSucursal");
            return claim?.Value;
        }

        public static string GetUserIdSucursal(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst("IdSucursal");
            return claim?.Value;
        }

        public static string GetUsername(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst(ClaimTypes.Name);
            return claim?.Value;
        }

        public static string GetUserEmail(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst(ClaimTypes.Email);
            return claim?.Value;
        }

        public static UsuarioRol? GetUserRole(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst(ClaimTypes.Role);

            if (claim is null)
                return null;

            var rol = Enum.Parse<UsuarioRol>(claim?.Value);
            //if (!Enum.IsDefined(typeof(UsuarioRol), rol))
            //    return null;

            return rol;
        }

    }
}
