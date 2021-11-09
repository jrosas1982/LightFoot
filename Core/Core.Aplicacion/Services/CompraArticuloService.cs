﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Aplicacion.Helpers;
using Core.Aplicacion.Interfaces;
using Core.Dominio.AggregatesModel;
using Core.Dominio.CoreModelHelpers;
using Core.Infraestructura;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;


namespace Core.Aplicacion.Services
{
    public class CompraArticuloService : ICompraArticuloService
    {
        private readonly AppDbContext _db;
        private readonly ILogger<CompraArticuloService> _logger;
        private readonly IArticuloService _articuloService;
        private readonly IProveedorService _proveedorService;
        private readonly IProveedorArticuloService _proveedorArticuloService;
        private readonly ISolicitudService _solicitudService;
        private readonly IConfiguration _Configuration;
        private readonly int IdSucursal;

        public CompraArticuloService(ExtendedAppDbContext extendedAppDbContext, ILogger<CompraArticuloService> logger, IArticuloService articuloService, IProveedorService proveedorService, IProveedorArticuloService proveedorArticuloService, IConfiguration configuration, ISolicitudService solicitudService)
        {
            _db = extendedAppDbContext.context;
            _logger = logger;
            _articuloService = articuloService;
            _proveedorService = proveedorService;
            _proveedorArticuloService = proveedorArticuloService;
            _Configuration = configuration;
            IdSucursal = int.Parse(_db.GetSucursalId());
            _solicitudService = solicitudService;
        }

        public async Task<bool> AgregarPagoCompra(int idCompra, TipoPago tipoPago, decimal montoPagado)
        {
            try
            {
                var compra = await _db.ComprasArticulos.Where(x => x.IdSucursal == IdSucursal).SingleOrDefaultAsync(x => x.Id == idCompra);
                if (compra == null)
                    throw new Exception("No existe la compra");

                var proveedorCuentaCorriente = new ProveedorArticuloCuentaCorriente()
                {
                    IdProveedor = compra.IdProveedor,
                    IdCompraArticulo = compra.Id,
                    TipoPago = tipoPago,
                    MontoPagado = montoPagado
                };

                compra.Pagado = true;
                compra.FechaPagado = DateTime.Now;

                _db.ComprasArticulos.Update(compra);

                _db.ProveedoresArticulosCuentaCorriente.Add(proveedorCuentaCorriente);

                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<CompraArticulo> BuscarPorId(int IdCompra)
        {
            var compra = await _db.ComprasArticulos
                .AsNoTracking()
                .Include(x => x.CompraArticuloDetalles)
                    .ThenInclude(x => x.Articulo)
                        //.ThenInclude(x => x.) TODO completar
                .Include(x => x.Proveedor)
                .Where(x => x.IdSucursal == IdSucursal)
                .SingleAsync(x => x.Id == IdCompra);
            return compra;
        }

        public async Task CrearCompra(IEnumerable<NuevaCompraArticuloModel> detalles)
        {
            var comprasAgrupadas = detalles.GroupBy(x => x.IdProveedor).ToList();
            var proveedores = await _proveedorService.GetProveedores();

            foreach (var grupo in comprasAgrupadas)
            {
                var compra = new CompraArticulo()
                {
                    IdProveedor = grupo.Key,
                    IdSucursal = IdSucursal
                };

                decimal montoTotal = 0;
                foreach (var item in grupo)
                {
                    var proveedorArticulo = await _proveedorArticuloService.BuscarProveedorArticulo(item.IdArticulo, item.IdProveedor);
                    var proveedoresArticuloList = proveedores.Where(x => x.ProveedorArticulos.Any(y => y.IdArticulo == item.IdArticulo));

                    if (!proveedoresArticuloList.Any() || proveedorArticulo == null)
                        throw new Exception($"No existe ningun proveedor asignado para el articulo {(await _articuloService.BuscarPorId(item.IdArticulo)).Nombre}");

                    var proveedorSugerido = proveedoresArticuloList.OrderByDescending(x => x.Calificacion).First();

                    var montoDetalle = proveedorArticulo.Precio * item.Cantidad;
                    montoTotal += montoDetalle;

                    var detalle = new CompraArticuloDetalle()
                    {

                        IdArticulo = item.IdArticulo,
                        IdProveedorSugerido = proveedorSugerido.Id,
                        Monto = montoDetalle,
                        Cantidad = item.Cantidad,
                        Comentario = item.Comentario,
                    };

                    compra.CompraArticuloDetalles.Add(detalle);
                }
                compra.MontoTotal = montoTotal;
                _db.ComprasArticulos.Add(compra);

                await _db.SaveChangesAsync();

                var proveedorEsFabrica = _db.Proveedores.Single(x => x.Id == compra.IdProveedor).EsFabrica;
                if (proveedorEsFabrica)                
                    await GenerarSolicitud(compra);                
                else                 
                    await EnviarMailCompra(compra.Id);
            }
        }

        public async Task<IEnumerable<CompraArticuloDetalle>> GetCompraDetalles(int IdCompra)
        {
            var compraDetalles = await _db.CompraArticuloDetalles
                .AsNoTracking()
                .Include(x => x.Articulo)
                    //.ThenInclude(x => x.ArticuloStock)
                    //    .ThenInclude(x => x.Proveedor)
                .Where(x => x.IdCompraArticulo == IdCompra && x.CompraArticulo.IdSucursal == IdSucursal)
                .ToListAsync();
            return compraDetalles;
        }

        public async Task<IEnumerable<CompraArticulo>> GetCompras()
        {
            var comprasList = await _db.ComprasArticulos
                .AsNoTracking()
                .Include(x => x.CompraArticuloDetalles)
                    .ThenInclude(x => x.Articulo)
                        //.ThenInclude(x => x.ArticuloStock)
                        //    .ThenInclude(x => x.Proveedor)
                .Include(x => x.Proveedor)
                .Where(x => x.IdSucursal == IdSucursal)
                .ToListAsync();

            _logger.LogInformation("Se buscaron las compras de insumos");
            return comprasList;
        }

        public async Task<bool> RecibirCompra(int idCompra, long nroRemito, double calificacionProveedor)
        {
            try
            {
                var compra = await _db.ComprasArticulos.Where(x => x.IdSucursal == IdSucursal).SingleOrDefaultAsync(x => x.Id == idCompra);
                if (compra == null)
                    throw new Exception("No existe la compra");

                compra.Recibido = true;
                compra.FechaRecibido = DateTime.Now;
                compra.NroRemito = nroRemito;

                foreach (var detalle in compra.CompraArticuloDetalles)
                {
                    var articulo = await _db.ArticulosStock.SingleOrDefaultAsync(x => x.Id == detalle.IdArticulo && x.IdSucursal == IdSucursal);
                    articulo.StockTotal += detalle.Cantidad;
                    _db.ArticulosStock.Update(articulo);
                }

                _db.ComprasArticulos.Update(compra);

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

        public async Task EnviarMailCompra(int IdCompra)
        {
            var compraRealizada = await _db.ComprasArticulos
                    .Include(x => x.CompraArticuloDetalles)
                        .ThenInclude(x => x.Articulo)
                        .ThenInclude(x => x.ProveedoresArticulo)
                    .Include(x => x.Proveedor)
                    .SingleAsync(x => x.Id == IdCompra);

            byte[] dataRow = Convert.FromBase64String(_Configuration.GetSection("EmailTemplates").GetSection("CompraItem")["EmailTableRow"]);
            string templateBaseRow = Encoding.UTF8.GetString(dataRow);

            string Maildetalles = "";
            foreach (var item in compraRealizada.CompraArticuloDetalles)
            {
                var row = templateBaseRow.Replace("@NombreItem", item.Articulo.Nombre)
                                         .Replace("@DescripcionItem", item.Articulo.Descripcion)
                                         .Replace("@UnidadesItem", item.Cantidad.ToString() + "u")
                                         .Replace("@PrecioItem", item.Articulo.ProveedoresArticulo.Single(x => x.IdProveedor == compraRealizada.IdProveedor).Precio.ToString());
                Maildetalles += row;
            }


            byte[] dataMail = Convert.FromBase64String(_Configuration.GetSection("EmailTemplates").GetSection("CompraInsumo")["EmailBody"]);
            string templateBaseMail = Encoding.UTF8.GetString(dataMail);

            var template = templateBaseMail.Replace("@NombreProveedor", compraRealizada.Proveedor.Nombre)
                                           .Replace("@IdCompra", compraRealizada.Id.ToString())
                                           .Replace("@Fecha", DateTime.Now.ToLongDateString())
                                           .Replace("@TableRows", Maildetalles)
                                           .Replace("@Cliente", "Fabrica LightFoot")
                                           .Replace("@MontoTotal", compraRealizada.MontoTotal.ToString())
                                           .Replace("@Direccion", "4562 Hazy Panda Limits, Chair Crossing, Kentucky, US, 607898");

            await EmailSender.SendEmail($"Nueva Compra Orden #{compraRealizada.Id}", template, compraRealizada.Proveedor.Email);
        }

        private async Task GenerarSolicitud(CompraArticulo compra)
        {
            var solicitud = new Solicitud()
            {
                IdSucursal = IdSucursal,
                Comentario = "Compra manual",
            };

            IList<SolicitudDetalle> solicitudDetalles = new List<SolicitudDetalle>();

            foreach (var item in compra.CompraArticuloDetalles)
            {
                solicitudDetalles.Add(new SolicitudDetalle()
                {
                    IdArticulo = item.IdArticulo,
                    CantidadSolicitada = item.Cantidad,
                    Motivo = item.Comentario
                });
            }

            await _solicitudService.CrearSolicitud(solicitud, solicitudDetalles);
        }
    }
}
