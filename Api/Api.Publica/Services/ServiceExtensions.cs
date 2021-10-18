using LightFoot.Api.Publica.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace LightFoot.Api.Publica.Services
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ApiKeyAuth>();

            return services;
        }

        //public static void AddApplicactionDbContext(this IServiceCollection serviceCollection, string ConnectionString)
        //{
        //    serviceCollection.AddDbContext<appDbContext>(options => options.UseSqlServer(ConnectionString));
        //}

        //public static void AddConnectionString(this IServiceCollection serviceCollection, string ConnectionString)
        //{
        //    var connectionString = new ConnectionString(ConnectionString);
        //    serviceCollection.AddSingleton(connectionString);
        //}

    }
}
