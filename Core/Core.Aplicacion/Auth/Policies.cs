using Core.Aplicacion.Helpers;
using Core.Dominio.AggregatesModel;
using Microsoft.AspNetCore.Authorization;

namespace Core.Aplicacion.Auth
{
    public static class Policies
    {
        public const string IsGod = "IsGod";
        public const string IsFabrica = "IsFabrica";
        public const string IsSucursal = "IsSucursal";

        public const string IsAdmin = "IsAdmin";

        public const string IsGerente = "IsGerente";
        public const string IsSupervisor = "IsSupervisor";
        public const string IsControlador = "IsControlador";
        public const string IsOperario = "IsOperario";

        public const string IsEncargado = "IsEncargado";
        public const string IsCajero = "IsCajero";
        public const string IsVendedor = "IsVendedor";


        public static AuthorizationPolicy FabricaPolicy()
        {
            return OperarioPolicy();
        }

        public static AuthorizationPolicy SucursalPolicy()
        {
            return VendedorPolicy();
        }

        public static AuthorizationPolicy GodPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                                                   .RequireAssertion(ctx =>
                                                   {
                                                       return ctx.User.GetUsername().ToLower() == "super";
                                                   })
                                                   .Build();
        }

        public static AuthorizationPolicy AdminPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                                                   .RequireRole(UsuarioRol.Administrador.ToString())
                                                   .Build();
        }

        public static AuthorizationPolicy GerentePolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                                                   .RequireAssertion(ctx =>
                                                   {
                                                       return ctx.User.IsInRole(UsuarioRol.Administrador.ToString())
                                                              || ctx.User.IsInRole(UsuarioRol.Gerente.ToString());
                                                   })
                                                   .Build();
        }

        public static AuthorizationPolicy SupervisorPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                                                   .RequireAssertion(ctx =>
                                                   {
                                                       return ctx.User.IsInRole(UsuarioRol.Administrador.ToString())
                                                              || ctx.User.IsInRole(UsuarioRol.Gerente.ToString())
                                                              || ctx.User.IsInRole(UsuarioRol.Supervisor.ToString());
                                                   })
                                                   .Build();
        }

        public static AuthorizationPolicy ControladorPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                                                   .RequireAssertion(ctx =>
                                                   {
                                                       return ctx.User.IsInRole(UsuarioRol.Administrador.ToString())
                                                              || ctx.User.IsInRole(UsuarioRol.Gerente.ToString())
                                                              || ctx.User.IsInRole(UsuarioRol.Supervisor.ToString())
                                                              || ctx.User.IsInRole(UsuarioRol.Controlador.ToString());
                                                   })
                                                   .Build();
        }

        public static AuthorizationPolicy OperarioPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                                                   .RequireAssertion(ctx =>
                                                   {
                                                       return ctx.User.IsInRole(UsuarioRol.Administrador.ToString())
                                                              || ctx.User.IsInRole(UsuarioRol.Gerente.ToString())
                                                              || ctx.User.IsInRole(UsuarioRol.Supervisor.ToString())
                                                              || ctx.User.IsInRole(UsuarioRol.Controlador.ToString())
                                                              || ctx.User.IsInRole(UsuarioRol.Operario.ToString());
                                                   })
                                                   .Build();
        }

        public static AuthorizationPolicy EncargadoPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                                                   .RequireAssertion(ctx =>
                                                   {
                                                       return ctx.User.IsInRole(UsuarioRol.Administrador.ToString())
                                                              || ctx.User.IsInRole(UsuarioRol.Encargado.ToString());
                                                   })
                                                   .Build();
        }

        public static AuthorizationPolicy CajeroPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                                                   .RequireAssertion(ctx =>
                                                   {
                                                       return ctx.User.IsInRole(UsuarioRol.Administrador.ToString())
                                                              || ctx.User.IsInRole(UsuarioRol.Encargado.ToString())
                                                              || ctx.User.IsInRole(UsuarioRol.Cajero.ToString());
                                                   })
                                                   .Build();
        }

        public static AuthorizationPolicy VendedorPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                                                   .RequireAssertion(ctx =>
                                                   {
                                                       return ctx.User.IsInRole(UsuarioRol.Administrador.ToString())
                                                              || ctx.User.IsInRole(UsuarioRol.Encargado.ToString())
                                                              || ctx.User.IsInRole(UsuarioRol.Cajero.ToString())
                                                              || ctx.User.IsInRole(UsuarioRol.Vendedor.ToString());
                                                   })
                                                   .Build();
        }
    }
}
