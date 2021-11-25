using Core.Aplicacion.Auth;
using Core.Aplicacion.Helpers;
using Core.Aplicacion.Interfaces;
using Core.Aplicacion.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Infraestructura
{
    public static class DependencyInjection
    {
        public static void AddCoreServices(this IServiceCollection serviceCollection)
        {
            
            serviceCollection.AddScoped<IArticulosSinStockService, ArticuloSinStockService>();
            serviceCollection.AddScoped<IUsuarioService, UsuarioService>();
            serviceCollection.AddScoped<IUserLoginService, UserLoginService>();
            serviceCollection.AddScoped<ISucursalService, SucursalService>();
            serviceCollection.AddScoped<IArticuloService, ArticuloService>();
            serviceCollection.AddScoped<IInsumoService, InsumoService>();
            serviceCollection.AddScoped<ISolicitudService, SolicitudService>();
            serviceCollection.AddScoped<IOrdenProduccionService, OrdenProduccionService>();
            serviceCollection.AddScoped<IArticuloCategoriaService, ArticuloCategoriaService>();
            serviceCollection.AddScoped<IFabricacionService, FabricacionService>();
            serviceCollection.AddScoped<IRecetaService, RecetaService>();
            serviceCollection.AddScoped<IRecetaDetalleService, RecetaDetalleService>();
            serviceCollection.AddScoped<IProveedorService, ProveedorService>();
            serviceCollection.AddScoped<IProveedorInsumoService, ProveedorInsumoService>();
            serviceCollection.AddScoped<ICompraInsumoService, CompraInsumoService>();
            serviceCollection.AddScoped<IControlStockArticuloService, ControlStockArticuloService>();
            serviceCollection.AddScoped<IParametroService, ParametroService>();
            serviceCollection.AddScoped<ICompraArticuloService, CompraArticuloService>();
            serviceCollection.AddScoped<IProveedorArticuloService, ProveedorArticuloService>();
            serviceCollection.AddScoped<IClienteService, ClienteService>();
            serviceCollection.AddScoped<IVentaService, VentaService>();
            serviceCollection.AddScoped<ICuentaCorrienteService, CuentaCorrienteService>();
            serviceCollection.AddScoped<IAdministracionDeCajaService, AdministracionDeCajaService>();
            serviceCollection.AddScoped<IDashboardSucursalService, DashboardSucursalService>();
            serviceCollection.AddScoped<IDashboardFabricaService, DashboardFabricaService>();

            serviceCollection.AddScoped<UserResolverService>();
        }
        public static void AddSecurityServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<PasswordHasher>();
        }
    }
}
