using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Aplicacion.Helpers;
using Core.Aplicacion.Interfaces;
using Core.Dominio.AggregatesModel;
using Core.Dominio.CoreModelHelpers;
using Core.Infraestructura;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Core.Aplicacion.Services
{
    public class CompraInsumoService : ICompraInsumoService
    {

        private readonly AppDbContext _db;
        private readonly ILogger<CompraInsumoService> _logger;
        private readonly IInsumoService _insumoService;
        private readonly IProveedorService _proveedorService;
        private readonly IProveedorInsumoService _proveedorInsumoService;
        private readonly ICompraInsumoService _compraInsumoService;

        public CompraInsumoService(ExtendedAppDbContext extendedAppDbContext, ILogger<CompraInsumoService> logger)
        {
            _db = extendedAppDbContext.context;
            _logger = logger;
        }

        public async Task<bool> AgregarPagoCompra(int idCompra, TipoPago tipoPago, decimal montoPagado)
        {
            throw new System.NotImplementedException();
        }

        public async Task<CompraInsumo> BuscarPorId(int IdCompra)
        {
            var compra = await _db.ComprasInsumos
                .AsNoTracking()
                .Include(x => x.CompraInsumoDetalles)
                    .ThenInclude(x => x.Insumo)
                        .ThenInclude(x => x.Proveedor)
                .Include(x => x.Proveedor)
                .SingleAsync(x => x.Id ==IdCompra);
            return compra;
        }

        public async Task CrearCompra(IEnumerable<NuevaCompraInsumoModel> detalles)
        {
            var comprasAgrupadas = detalles.GroupBy(x => x.IdProveedor).ToList();
            var proveedores = await _proveedorService.GetProveedores();

            foreach (var grupo in comprasAgrupadas)
            {
                var compra = new CompraInsumo()
                {
                    IdProveedor = grupo.Key,
                };

                decimal montoTotal = 0;
                foreach (var item in grupo)
                {
                    var proveedorInsumo = await _proveedorInsumoService.BuscarProveedorInsumo(item.IdInsumo, item.IdProveedor);
                    var proveedoresInsumoList = proveedores.Where(x => x.ProveedorInsumos.Any(y => y.IdInsumo == item.IdInsumo));

                    if (!proveedoresInsumoList.Any() || proveedorInsumo == null)
                        throw new Exception($"No existe ningun proveedor asignado para el insumo {(await _insumoService.BuscarPorId(item.IdInsumo)).Nombre}");

                    var proveedorSugerido = proveedoresInsumoList.OrderByDescending(x => x.Calificacion).First();

                    var montoDetalle = proveedorInsumo.Precio * item.Cantidad;
                    montoTotal += montoDetalle;

                    var detalle = new CompraInsumoDetalle()
                    {
                        IdInsumo = item.IdInsumo,
                        IdProveedorSugerido = proveedorSugerido.Id,
                        Monto = montoDetalle,
                        Cantidad = item.Cantidad,
                        Comentario = item.Comentario,
                    };

                    compra.CompraInsumoDetalles.Add(detalle);
                }
                compra.MontoTotal = montoTotal;
                _db.ComprasInsumos.Add(compra);
            }
            await _db.SaveChangesAsync();

            //TODO enviar mail a proveedores
        }

        public async Task<IEnumerable<CompraInsumoDetalle>> GetCompraDetalles(int IdCompra)
        {
            var compraDetalles = await _db.CompraInsumoDetalles
                .AsNoTracking()
                .Include(x => x.Insumo)
                .ThenInclude(x => x.Proveedor)
                .Where(x => x.IdCompraInsumo == IdCompra)
                .ToListAsync();
            return compraDetalles;
        }

        public async Task<IEnumerable<CompraInsumo>> GetCompras()
        {
            var comprasList = await _db.ComprasInsumos
                .AsNoTracking()
                .Include(x => x.CompraInsumoDetalles)
                    .ThenInclude(x => x.Insumo)
                        .ThenInclude(x => x.Proveedor)
                .Include(x => x.Proveedor)
                .ToListAsync();

            _logger.LogInformation("Se buscaron las compras de insumos");
            return comprasList;
        }

        public async Task<bool> RecibirCompra(int idCompra, long nroRemito, double calificacionProveedor)
        {
            try
            {
                var compra = await _compraInsumoService.BuscarPorId(idCompra);
                if (compra == null)
                    throw new Exception("No existe la compra");

                compra.Recibido = true;
                compra.NroRemito = nroRemito;

                foreach (var detalle in compra.CompraInsumoDetalles)
                {
                    var insumo = await _insumoService.BuscarPorId(detalle.IdInsumo);
                    insumo.StockTotal += detalle.Cantidad;
                }

                _db.ComprasInsumos.Update(compra);

                var proveedor = await _proveedorService.BuscarPorId(compra.IdProveedor);
                proveedor.Calificacion = calificacionProveedor;
                _db.Proveedores.Update(proveedor);

                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
