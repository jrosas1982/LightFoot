﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Aplicacion.Helpers;
using Core.Aplicacion.Interfaces;
using Core.Dominio;
using Core.Dominio.AggregatesModel;
using Core.Dominio.CoreModelHelpers;
using Core.Infraestructura;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Core.Aplicacion.Services
{
    public class VentaService : IVentaService
    {
        private readonly AppDbContext _db;
        private readonly ILogger<VentaService> _logger;
        private readonly IArticuloService _articuloService;
        private readonly IProveedorService _proveedorService;
        private readonly IProveedorArticuloService _proveedorArticuloService;
        private readonly IControlStockArticuloService _controlStockArticuloService;
        private readonly IConfiguration _Configuration;

        public VentaService( AppDbContext db, ILogger<VentaService> logger, IArticuloService articuloService, IProveedorService proveedorService, IProveedorArticuloService proveedorArticuloService, IControlStockArticuloService controlStockArticuloService, IConfiguration configuration)
        {
            _db = db;
            _logger = logger;
            _articuloService = articuloService;
            _proveedorService = proveedorService;
            _proveedorArticuloService = proveedorArticuloService;
            _controlStockArticuloService = controlStockArticuloService;
            _Configuration = configuration;
        }

        public async Task AgregarPagoVenta(int idVenta, TipoPago tipoPago, decimal montoPercibido)
        {
            int idSucursal = int.Parse(_db.GetSucursalId());
            var venta = await _db.Ventas.Where(x => x.IdSucursal == idSucursal).SingleOrDefaultAsync(x => x.Id == idVenta);
            if (venta == null)
                throw new ExcepcionControlada("No existe la compra");

            var montoPercibidoTotal = await _db.ClientesCuentaCorriente.Where(x => x.IdVenta == idVenta).SumAsync(x => x.MontoPercibido);

            if (venta.MontoTotal < (montoPercibido + montoPercibidoTotal))
                throw new ExcepcionControlada("No se puede cobrar mas del total de la venta");

            var clienteCuentaCorriente = new ClienteCuentaCorriente()
            {
                IdCliente = venta.IdCliente,
                IdVenta = venta.Id,
                TipoPago = tipoPago,
                MontoPercibido = montoPercibido
            };

            venta.Pagado = true;
            venta.FechaUltimoPago = DateTime.Now;

            _db.Ventas.Update(venta);
            _db.ClientesCuentaCorriente.Add(clienteCuentaCorriente);

            await _db.SaveChangesAsync();
        }

        public async Task<Venta> BuscarPorId(int idVenta)
        {
            int idSucursal = int.Parse(_db.GetSucursalId());

            var venta = await _db.Ventas
                           .AsNoTracking()
                           .Where(x => x.IdSucursal == idSucursal && x.Id == idVenta)
                           .Include(x => x.VentaDetalles)
                               .ThenInclude(x => x.Articulo)
                           .Include(x => x.Cliente)
                           .Include(x => x.Sucursal)
                           .OrderBy(x => x.FechaModificacion)
                           .ThenBy(x => x.Descuento)
                           .ThenByDescending(x => x.FechaModificacion.HasValue)
                           .ThenByDescending(x => x.FechaCreacion)
                           .SingleAsync();

            return venta;
        }

        public async Task<Venta> CrearVenta(int idCliente, VentaTipo ventaTipo, IEnumerable<NuevaVentaDetalleModel> detalles)
        {
            IEnumerable<ArticuloPrecio> articuloPrecioList;
            if (ventaTipo == VentaTipo.Mayorista)
                articuloPrecioList = _db.Articulos.Select(x => new ArticuloPrecio (){ IdArticulo = x.Id, Precio = x.PrecioMayorista }).ToList();
            else
                articuloPrecioList = _db.Articulos.Select(x => new ArticuloPrecio() { IdArticulo = x.Id, Precio = x.PrecioMinorista }).ToList();

            IList<VentaDetalle> ventaDetalles = new List<VentaDetalle>();
            decimal montoTotalAcum = 0;
            foreach (var item in detalles)
            {
                var detalle = new VentaDetalle()
                {
                    IdArticulo = item.IdArticulo,
                    Cantidad = item.Cantidad,
                    PrecioUnitario = articuloPrecioList.Single(x => x.IdArticulo == item.IdArticulo).Precio,

                };
                ventaDetalles.Add(detalle);
                montoTotalAcum += (detalle.PrecioUnitario * item.Cantidad);
            }

            var idSucursal = int.Parse(_db.GetSucursalId());

            var venta = new Venta()
            {
                IdSucursal = idSucursal,
                IdCliente = idCliente,
                UsuarioVenta = _db.GetUsername(),
                MontoTotal = montoTotalAcum,//  - descuentoRealizado,
                VentaTipo = ventaTipo,
                //Descuento = descuentoRealizado,
                VentaDetalles = ventaDetalles,                
            };

            _db.Ventas.Add(venta);
            await _db.SaveChangesAsync();

            await EnviarMailVenta(venta.Id);

            return venta;
        }

        private class ArticuloPrecio
        {
            public int IdArticulo { get; set; }
            public decimal Precio { get; set; }
        }

        public async Task<IEnumerable<TipoPago>> GetTiposPago()
        {
            return await Task.FromResult(EnumExtensions.GetValues<TipoPago>());
        }

        public async Task<IEnumerable<VentaTipo>> GetTiposVenta()
        {
            return await Task.FromResult(new List<VentaTipo>() { VentaTipo.Mayorista, VentaTipo.Minorista });
        }

        public async Task<IEnumerable<VentaDetalle>> GetVentaDetalles(int idVenta)
        {
            var ventaDetalles = _db.VentasDetalle
                .Include(x => x.Articulo)
                .Where(x => x.IdVenta == idVenta);

            return await Task.FromResult(ventaDetalles);
        }

        public async Task<IEnumerable<Venta>> GetVentas()
        {
            int idSucursal = int.Parse(_db.GetSucursalId());

            var ventasList = await _db.Ventas
                .Where(x => !x.Eliminado)
                .AsNoTracking()
                .Where(x => x.IdSucursal == idSucursal)
                .Include(x => x.VentaDetalles)
                    .ThenInclude(x => x.Articulo)
                .Include(x => x.Cliente)
                .Include(x => x.Sucursal)
                .OrderBy(x => x.FechaModificacion)
                .ThenBy(x => x.Descuento)
                .ThenByDescending(x => x.FechaModificacion.HasValue)
                .ThenByDescending(x => x.FechaCreacion)
                .ToListAsync();
            return ventasList;
        }
        public async Task<IEnumerable<Venta>> GetVentasPorCliente(int IdCliente)
        {

            var ventasList = await _db.Ventas
                .Where(x => !x.Eliminado && x.IdCliente == IdCliente)            
                .ToListAsync();
            return ventasList;
        }


        public async Task EnviarMailVenta(int IdVenta)
        {
            var ventaRealizada = await _db.Ventas  
                .Include(x => x.VentaDetalles)
                .Include(x => x.Cliente)
                .SingleAsync(x => x.Id == IdVenta);

            byte[] dataRow = Convert.FromBase64String(_Configuration.GetSection("EmailTemplates").GetSection("VentaCliente")["EmailTableRow"]);
            string templateBaseRow = Encoding.UTF8.GetString(dataRow);

            string Maildetalles = "";
            foreach (var item in ventaRealizada.VentaDetalles)
            {
                var row = templateBaseRow.Replace("@NombreArticulo", $"{item.Articulo.ArticuloCategoria.Descripcion} - {item.Articulo.Nombre}")
                                         .Replace("@TalleArticulo", item.Articulo.TalleArticulo)
                                         .Replace("@ColorArticulo", item.Articulo.Color)
                                         .Replace("@Cantidad", item.Cantidad.ToString() + "u")
                                         .Replace("@PrecioUnitario", item.PrecioUnitario.ToString());
                Maildetalles += row;
            }


            byte[] dataMail = Convert.FromBase64String(_Configuration.GetSection("EmailTemplates").GetSection("VentaCliente")["EmailBody"]);
            string templateBaseMail = Encoding.UTF8.GetString(dataMail);

            var idSucursal = int.Parse(_db.GetSucursalId());
            var sucursalCompra = await _db.Sucursales.FindAsync(idSucursal);

            var template = templateBaseMail.Replace("@TotalArticulos", ventaRealizada.VentaDetalles.Count().ToString())
                                           .Replace("@MontoTotal", ventaRealizada.MontoTotal.ToString())
                                           .Replace("@Cliente", ventaRealizada.Cliente.Nombre);

            await EmailSender.SendEmail($"LightFoot - Nueva Compra #{ventaRealizada.Id}", template, ventaRealizada.Cliente.Email);
        }
    }
}
