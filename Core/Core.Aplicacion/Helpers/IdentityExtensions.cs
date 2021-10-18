using System;
using System.Security.Claims;
using System.Security.Principal;
using Core.Dominio.AggregatesModel;
using Core.Infraestructura;

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

    }
}
