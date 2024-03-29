﻿using System.Globalization;
using AutoMapper;
using Core.Aplicacion.Auth;
using Core.Aplicacion.Hubs;
using Core.Infraestructura;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Web.Site.Helpers;

namespace LightFoot.Web.Site
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            var cultureInfo = new CultureInfo("es-AR");
            cultureInfo.NumberFormat.CurrencySymbol = "$";

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            // DbContext
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Conexion")), ServiceLifetime.Transient);

            // Core Services
            services.AddCoreServices();
            services.AddSecurityServices();
            services.AddHttpContextAccessor();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddSignalR();
            services.AddAutoMapper(typeof(Startup));

            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            services.AddAuthorization(config =>
            {
                config.AddPolicy(Policies.IsGod, Policies.GodPolicy());
                config.AddPolicy(Policies.IsFabrica, Policies.FabricaPolicy());
                config.AddPolicy(Policies.IsSucursal, Policies.SucursalPolicy());

                config.AddPolicy(Policies.IsAdmin, Policies.AdminPolicy());
                config.AddPolicy(Policies.IsGerente, Policies.GerentePolicy());
                config.AddPolicy(Policies.IsSupervisor, Policies.SupervisorPolicy());
                config.AddPolicy(Policies.IsTesorero, Policies.TesoreroPolicy());
                config.AddPolicy(Policies.IsOperario, Policies.OperarioPolicy());

                config.AddPolicy(Policies.IsEncargado, Policies.EncargadoPolicy());
                config.AddPolicy(Policies.IsCajero, Policies.CajeroPolicy());
                config.AddPolicy(Policies.IsVendedor, Policies.VendedorPolicy());
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc(options => options.EnableEndpointRouting = false);

            services.AddAuthentication(options =>
             {
                 options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                 options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                 options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
             }).AddCookie(options =>
                            {
                                options.LoginPath = "/Auth/LogIn"; //test
                                options.LogoutPath = "/Auth/LogIn"; //test
                                options.AccessDeniedPath = "/"; //si no cumplis con authorize te lleva aca
                            });
            //services.ConfigureApplicationCookie(options => {

            //    options.Events = new Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationEvents
            //    {
            //        OnRedirectToLogin = ctx =>
            //        {
            //            var requestPath = ctx.Request.Path;
            //            ctx.Response.Redirect(requestPath);
            //            return Task.CompletedTask;
            //        }
            //    };

            //});

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            //app.UseExceptionHandler(a => a.Run(async context =>
            //{
            //    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
            //    var exception = exceptionHandlerPathFeature.Error;

            //    var result = JsonConvert.SerializeObject(new { error = exception.Message });
            //    context.Response.ContentType = "application/json";
            //    await context.Response.WriteAsync(result);
            //}));

            //app.Use(async (context, next) =>
            //{
            //    await next();
            //    if (context.Response.StatusCode == 401)
            //    {
            //        context.Request.Path = "/Auth/LogIn";
            //        await next();
            //    }
            //    if (context.Response.StatusCode == 404)
            //    {
            //        context.Request.Path = "/Pages/NotFoundError";
            //        await next();
            //    }
            //});

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<NotificationsHub>("/NotificationsHub");
            });

            //app.UseSignalR(routes =>
            //{
            //    routes.MapHub<NotificationsHub>("/NotificationsHub");
            //});

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "default",
                  template: "{controller=Dashboards}/{action=Dashboard_1}/{id?}");

                routes.MapRoute(
                  name: "areas",
                  template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
