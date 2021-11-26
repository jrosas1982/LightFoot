using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Dominio.AggregatesModel;
using Core.Infraestructura;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace LightFoot.Api.Publica.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiKeyAuth : Attribute, IAuthorizationFilter
    {
        private readonly AppDbContext _db;
        public const string ApiKeyHeaderName = "ApiKey";

        public ApiKeyAuth(AppDbContext db)
        {
            _db = db;
        }

        public async void OnAuthorization(AuthorizationFilterContext context)
        {
            var IsApiKey = context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeaderName, out var PossibleApiKey);

            if (IsApiKey)
            {
                // Busco usuario por ApiKey en la base de datos
                var usuario = _db.UsuariosApi.SingleOrDefault(x => x.ApiKey.Equals(PossibleApiKey));
                if (usuario != null)
                {
                    var sucursal = _db.Sucursales.SingleOrDefault(x => x.Id == usuario.IdSucursal);
                    if (sucursal != null)
                    {
                        // Genero claims
                        var Claims = new ClaimsIdentity(new Claim[]
                            {
                        new Claim(ClaimTypes.Name, $"{usuario.Nombre}"),
                        new Claim("IdSucursal", usuario.IdSucursal.ToString()),
                        new Claim("NombreSucursal", sucursal.Nombre),
                                //new Claim("UserId", user.Id.ToString(), ClaimValueTypes.Integer),
                                //new Claim(ClaimTypes.Name, user.Name),
                                //new Claim("ApiKey", user.ApiKey),
                                //new Claim("Sucursal", user.IdSucursal.ToString(), ClaimValueTypes.Integer)
                            });
                        context.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(Claims));
                        return;
                    }
                }
            }
            context.Result = new UnauthorizedResult();
        }

    }
}
