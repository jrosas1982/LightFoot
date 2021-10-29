using System;
using System.Security.Claims;
using System.Security.Principal;
using Core.Dominio.AggregatesModel;
using Core.Infraestructura;
using Microsoft.AspNetCore.Http;

namespace Core.Aplicacion.Helpers
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

        public static string GetUsername(this IHttpContextAccessor httpContext)
        {
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
