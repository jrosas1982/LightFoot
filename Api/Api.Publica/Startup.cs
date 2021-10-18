using System.Collections.Generic;
using System.Text.Json.Serialization;
using LightFoot.Api.Publica.Filters;
using LightFoot.Api.Publica.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace ApiPublic
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(
                    "Open",
                    builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });

            services.AddControllers(options =>
            {
                options.Conventions.Add(new GroupingByNamespaceConvention());
            });

            services.AddSwaggerGen(config =>
            {
                config.EnableAnnotations();
                config.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "LightFoot Public API",
                    Description = "Api de servicio de comunicacion LightFoot",
                    // TermsOfService = TermsOfService,
                    // License = License,
                    // Contact = Contact
                });
                config.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                      new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "ApiKey"
                            },
                        },
                        new List<string>()
                    }
                });
                config.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
                {
                    Description = "Ingresar tu ApiKey de integracion:",
                    Name = ApiKeyAuth.ApiKeyHeaderName,
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
            });

            services.AddControllersWithViews().AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            services.AddHttpContextAccessor();

            services.AddServices();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("v1/swagger.json", "ApiPublic v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(policyName: "Open");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
