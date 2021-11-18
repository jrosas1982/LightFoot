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
        private string username;
        public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
            username = _httpContextAccessor.HttpContext.User.GetUsername();
        }

        public DbSet<Articulo> Articulos { get; set; }
        public DbSet<ArticuloCategoria> ArticulosCategoria { get; set; }
        public DbSet<ArticuloStock> ArticulosStock { get; set; }
        public DbSet<MovimientoStock> MovimientosStock { get; set; }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<ClienteCuentaCorriente> ClientesCuentaCorriente { get; set; }

        public DbSet<CompraArticulo> ComprasArticulos { get; set; }
        public DbSet<CompraArticuloDetalle> CompraArticuloDetalles { get; set; }
        public DbSet<CompraInsumo> ComprasInsumos { get; set; }
        public DbSet<CompraInsumoDetalle> CompraInsumoDetalles { get; set; }

        public DbSet<Insumo> Insumos { get; set; }
        public DbSet<Receta> Recetas { get; set; }
        public DbSet<RecetaDetalle> RecetaDetalles { get; set; }

        public DbSet<EtapaOrdenProduccion> EtapasOrdenProduccion { get; set; }
        public DbSet<OrdenProduccion> OrdenesProduccion { get; set; }
        public DbSet<OrdenProduccionEvento> OrdenesProduccionEventos { get; set; }

        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<ProveedorArticulo> ProveedoresArticulos { get; set; }
        public DbSet<ProveedorArticuloHistorico> ProveedoresArticulosHistorico { get; set; }
        public DbSet<ProveedorArticuloCuentaCorriente> ProveedoresArticulosCuentaCorriente { get; set; }
        public DbSet<ProveedorInsumo> ProveedoresInsumos { get; set; }
        public DbSet<ProveedorInsumoHistorico> ProveedoresInsumosHistorico { get; set; }
        public DbSet<ProveedorInsumoCuentaCorriente> ProveedoresInsumosCuentaCorriente { get; set; }

        public DbSet<Solicitud> Solicitudes { get; set; }
        public DbSet<SolicitudDetalle> SolicitudDetalles { get; set; }
        public DbSet<SolicitudEvento> SolicitudEventos { get; set; }

        public DbSet<CajaSucursal> CajaSucursales { get; set; }
        public DbSet<Sucursal> Sucursales { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<UsuarioApi> UsuariosApi { get; set; }

        public DbSet<Venta> Ventas { get; set; }
        public DbSet<VentaDetalle> VentasDetalle { get; set; }

        public DbSet<FabricaParametro> FabricaParametros { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new EtapaOrdenProduccionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProveedorEntityTypeConfiguration());

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.NoAction;
            }

            //modelBuilder.Entity<EntityBase>().HasQueryFilter(e => e.Eliminado == false);
            modelBuilder.ApplyGlobalFilters<EntityBase>(e => username != null && username.ToLower() != "super" ? e.Eliminado == false : true);

            //modelBuilder.Entity<Sucursal>().HasMany(x => x.MovimientoStockOrigen).WithOne(x => x.SucursalOrigen).HasForeignKey(x => x.IdSucursalOrigen).IsRequired();
            //modelBuilder.Entity<Sucursal>().HasMany(x => x.MovimientoStockDestino).WithOne(x => x.SucursalDestino).HasForeignKey(x => x.IdSucursalDestino).IsRequired();

            //modelBuilder.Entity<MovimientoStock>().HasOne(x => x.SucursalOrigen).WithOne().IsRequired().OnDelete(DeleteBehavior.NoAction);
            //modelBuilder.Entity<MovimientoStock>().HasOne(x => x.SucursalDestino).WithOne().IsRequired().OnDelete(DeleteBehavior.NoAction);
        }



        public override int SaveChanges()
        {
            this.ChangeTracker.DetectChanges();
            var currentUsername = _httpContextAccessor.GetUsername();

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
            var currentUsername = _httpContextAccessor.GetUsername();

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
