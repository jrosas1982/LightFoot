using System;
using System.Security.Claims;
using System.Security.Principal;
using Core.Dominio.AggregatesModel;
using Core.Infraestructura;
using Microsoft.AspNetCore.Http;

namespace Core.Infraestructura
{
    public static class IdentityExtensions
    {
        //TODO mejorar
        public static string GetUsername(this AppDbContext db)
        {
            var user = db._httpContextAccessor.HttpContext.User;
            var claim = ((ClaimsIdentity)user.Identity).FindFirst(ClaimTypes.Name);
            return claim?.Value;
        }
        public static string GetSucursalId(this AppDbContext db)
        {
            var user = db._httpContextAccessor.HttpContext.User;
            var claim = ((ClaimsIdentity)user.Identity).FindFirst("IdSucursal");
            return claim?.Value;
        }

        public static UsuarioRol? GetUserRole(this IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor == null)
                return null;

            var claim = ((ClaimsIdentity)httpContextAccessor.HttpContext.User.Identity).FindFirst(ClaimTypes.Role);

            if (claim is null)
                return null;

            var rol = Enum.Parse<UsuarioRol>(claim?.Value);
            //if (!Enum.IsDefined(typeof(UsuarioRol), rol))
            //    return null;

            return rol;
        }

        public static string GetUsername(this IHttpContextAccessor httpContext)
        {
            if (httpContext == null)
                return null;

            var claim = ((ClaimsIdentity)httpContext.HttpContext.User.Identity).FindFirst(ClaimTypes.Name);
            return claim?.Value;
        }

        public static string GetUsername(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst(ClaimTypes.Name);
            return claim?.Value;
        }       



    }
}
