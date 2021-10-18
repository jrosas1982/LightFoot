using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Core.Dominio.AggregatesModel;
using Core.Dominio.SeedWork;
using Core.Infraestructura.EntityConfiguration;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Core.Infraestructura
{
    public class AppDbContext : DbContext
    {
        public IHttpContextAccessor _httpContextAccessor;
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected AppDbContext()
        {

        }

        public DbSet<ArticuloCategoria> ArticuloCategorias { get; set; }
        public DbSet<Articulo> Articulos { get; set; }
        public DbSet<CajaSucursal> CajaSucursales { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Insumo> Insumos { get; set; }

        public DbSet<EtapaOrdenProduccion> EtapasOrdenProduccion { get; set; }
        public DbSet<OrdenProduccion> OrdenesProduccion { get; set; }
        public DbSet<OrdenProduccionEvento> OrdeneProduccionEventos { get; set; }

        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Solicitud> Solicitudes { get; set; }
        public DbSet<SolicitudDetalle> SolicitudDetalles { get; set; }
        public DbSet<SolicitudEvento> SolicitudEventos { get; set; }

        public DbSet<ArticuloStock> ArticulosStock { get; set; }
        public DbSet<InsumoStock> InsumosStock { get; set; }
        public DbSet<Sucursal> Sucursales { get; set; }

        public DbSet<UsuarioApi> UsuariosApi { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        //public DbSet<Usuario> Usuarios { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new EtapaOrdenProduccionEntityTypeConfiguration());
            //modelBuilder.Entity<Sucursal>(x => x.);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.NoAction;
            }

            //modelBuilder.Entity<Sucursal>().HasMany(x => x.MovimientoStockOrigen).WithOne(x => x.SucursalOrigen).HasForeignKey(x => x.IdSucursalOrigen).IsRequired();
            //modelBuilder.Entity<Sucursal>().HasMany(x => x.MovimientoStockDestino).WithOne(x => x.SucursalDestino).HasForeignKey(x => x.IdSucursalDestino).IsRequired();

            //modelBuilder.Entity<MovimientoStock>().HasOne(x => x.SucursalOrigen).WithOne().IsRequired().OnDelete(DeleteBehavior.NoAction);
            //modelBuilder.Entity<MovimientoStock>().HasOne(x => x.SucursalDestino).WithOne().IsRequired().OnDelete(DeleteBehavior.NoAction);
        }

        private string GetUsername(IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst(ClaimTypes.Name);
            return claim?.Value;
        }

        public override int SaveChanges()
        {
            this.ChangeTracker.DetectChanges();
            var currentUsername = GetUsername(_httpContextAccessor.HttpContext.User);

            var added = this.ChangeTracker.Entries()
                        .Where(t => t.Entity is EntityBase && t.State == EntityState.Added)
                        .Select(t => t.Entity)
                        .ToArray();

            foreach (var entity in added)
            {
                var baseEntity = entity as EntityBase;
                baseEntity.FechaCreacion = DateTime.Now;
                baseEntity.CreadoPor = currentUsername;
            }

            var modified = this.ChangeTracker.Entries()
                        .Where(t => t.Entity is EntityBase && t.State == EntityState.Modified)
                        .Select(t => t.Entity)
                        .ToArray();

            foreach (var entity in modified)
            {
                var track = entity as EntityBase;
                track.FechaModificacion = DateTime.Now;
                track.ModificadoPor = currentUsername;
            }

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            this.ChangeTracker.DetectChanges();
            var currentUsername = GetUsername(_httpContextAccessor.HttpContext.User);

            var added = this.ChangeTracker.Entries()
                        .Where(t => t.Entity is EntityBase && t.State == EntityState.Added)
                        .Select(t => t.Entity)
                        .ToArray();

            foreach (var entity in added)
            {
                var baseEntity = entity as EntityBase;
                baseEntity.FechaCreacion = DateTime.Now;
                baseEntity.CreadoPor = currentUsername;
            }

            var modified = this.ChangeTracker.Entries()
                        .Where(t => t.Entity is EntityBase && t.State == EntityState.Modified)
                        .Select(t => t.Entity)
                        .ToArray();

            foreach (var entity in modified)
            {
                var track = entity as EntityBase;
                track.FechaModificacion = DateTime.Now;
                track.ModificadoPor = currentUsername;
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
