using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Core.Infraestructura
{
    public class UserResolverService
    {
        public readonly IHttpContextAccessor _context;

        public UserResolverService(IHttpContextAccessor context)
        {
            _context = context;
        }

        public string GetUsername()
        {
            return _context.HttpContext.User.FindFirst(ClaimTypes.Name).Value;
        }

    }
}
