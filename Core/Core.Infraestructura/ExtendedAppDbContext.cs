using Microsoft.AspNetCore.Http;

namespace Core.Infraestructura
{
    public class ExtendedAppDbContext
    {
        public AppDbContext context;
        public IHttpContextAccessor _httpContextAccessor;

        public ExtendedAppDbContext(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.context._httpContextAccessor = httpContextAccessor;
        }
    }
}
