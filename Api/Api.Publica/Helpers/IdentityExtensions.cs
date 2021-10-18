using System.Security.Claims;
using System.Security.Principal;

namespace LightFoot.Api.Publica.Helpers
{
    public static class IdentityExtensions
    {
        public static string GetUserId(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("UserId");
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string GetUsername(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst(ClaimTypes.Name);
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string GetApiKey(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("ApiKey");
            return (claim != null) ? claim.Value : string.Empty;
        }
    }
}
