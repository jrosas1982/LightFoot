using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LightFoot.Api.Publica.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiKeyAuth : Attribute, IAuthorizationFilter
    {
        public const string ApiKeyHeaderName = "ApiKey";

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var IsApiKey = context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeaderName, out var PossibleApiKey);

            if (IsApiKey)
            {
                // Busco usuario por ApiKey en la base de datos
                //ApiUser user = await _context.ApiUsers.SingleOrDefaultAsync(x => x.ApiKey.Equals(PossibleApiKey));
                var user = "usuario base de datos";

                if (user != null)
                {
                    // Genero claims
                    var Claims = new ClaimsIdentity(new Claim[]
                    {
                        //new Claim("UserId", user.Id.ToString(), ClaimValueTypes.Integer),
                        //new Claim(ClaimTypes.Name, user.Name),
                        //new Claim("ApiKey", user.ApiKey),
                        //new Claim("Sucursal", user.IdSucursal.ToString(), ClaimValueTypes.Integer)
                    });
                    context.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(Claims));

                }
                else
                {
                    context.Result = new UnauthorizedResult();
                }
            }
            else
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
