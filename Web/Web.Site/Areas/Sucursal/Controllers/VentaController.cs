using System.Linq;
using System.Threading.Tasks;
using Core.Aplicacion.FIlters;
using Core.Aplicacion.Interfaces;
using Core.Dominio.AggregatesModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Site.Dtos;
using Web.Site.Helpers;
using System;
using Core.Dominio.CoreModelHelpers;
using System.Collections.Generic;

namespace Web.Site.Areas
{
    [Area("sucursal")]
    [Route("[area]/[controller]/[action]")]
    public class VentaController : CustomController
    {
        private IVentaService _ventaService;
        private IClienteService _clienteService;
        private IArticuloService _articuloService;
        private IArticuloCategoriaService _articuloCategoriaService;
        public ICuentaCorrienteService _cuentaCorrienteService;

        public VentaController(IVentaService ventaService, IClienteService clienteService, IArticuloService articuloService, IArticuloCategoriaService articuloCategoriaService, ICuentaCorrienteService cuentaCorrienteService)
        {
            _ventaService = ventaService;
            _clienteService = clienteService;
            _articuloService = articuloService;
            _articuloCategoriaService = articuloCategoriaService;
            _cuentaCorrienteService = cuentaCorrienteService;
        }

        public async Task<IActionResult> FiltrarVentas(string nombreVenta)
        {
            var ventaList = await _ventaService.GetVentas();

            if (!string.IsNullOrWhiteSpace(nombreVenta))
            {
                ventaList = ventaList.Where(x => x.Id.ToString().Equals(nombreVenta.ToLower())
                                                            || x.CreadoPor.ToString().Equals(nombreVenta.ToLower())
                                                            || x.Cliente.Nombre.ToLower().Equals(nombreVenta.ToLower())
                                                            || x.VentaTipo.ToString().Equals(nombreVenta.ToLower())
                                                            || x.MontoTotal.ToString().Equals(nombreVenta.ToLower()));
                if (!ventaList.Any())
                    ventaList = ventaList.Where(x => x.Id.ToString().Contains(nombreVenta.ToLower())
                                                            || x.CreadoPor.ToString().Contains(nombreVenta.ToLower())
                                                            || x.Cliente.Nombre.ToLower().Contains(nombreVenta.ToLower())
                                                            || x.VentaTipo.ToString().Contains(nombreVenta.ToLower())
                                                            || x.MontoTotal.ToString().Contains(nombreVenta.ToLower()));
            }
            return PartialView("_VentaIndexTable", ventaList);
        }

        public async Task<IActionResult> Index()
        {
            var ventasListTask = _ventaService.GetVentas();
            var ventaTiposListTask = _ventaService.GetTiposVenta();
            var clienteListTask = _clienteService.GetClientes();
            var tipoPagoTask = _ventaService.GetTiposPago();

            await Task.WhenAll(ventasListTask, clienteListTask, ventaTiposListTask, tipoPagoTask);

            var ventasList = ventasListTask.Result;
            var clienteList = clienteListTask.Result;
            var ventasTipoList = ventaTiposListTask.Result;
            var tipoPagoList = tipoPagoTask.Result;

            ViewBag.Clientes = clienteList.Select(x => new SelectListItem() { Text = $"{x.Nombre}", Value = $"{x.Id}" });
            ViewBag.VentaTipos = ventasTipoList;
            ViewBag.TiposPago = tipoPagoList;

            ViewBag.TypeaheadNumVenta = ventasList.Select(x => x.Id.ToString()).Distinct();
            ViewBag.TypeaheadVendedor = ventasList.Select(x => x.CreadoPor).Distinct();
            ViewBag.TypeaheadCliente = ventasList.Select(x => x.Cliente.Nombre).Distinct();
            ViewBag.TypeaheadTipoVenta = ventasList.Select(x => x.VentaTipo.ToString()).Distinct();
            ViewBag.TypeaheadTipoMontoTotal = ventasList.Select(x => x.MontoTotal.ToString()).Distinct();

            return View(ventasList);
        }

        public async Task<IActionResult> VentaArticulo(int IdCliente, VentaTipo ventaTipo)
        {
            var articulosList = await _articuloService.GetArticulos();

            Cliente cliente = await _clienteService.BuscarPorId(IdCliente);

            VentaModel ventaModel = new VentaModel()
            {
                Cliente = cliente,
                IdCliente = IdCliente,
                VentaTipo = ventaTipo
            };
            ventaModel.Articulos = articulosList.Select(x => new SelectListItem() { Text = $"{x.ArticuloCategoria.Descripcion} - {x.CodigoArticulo} - {x.Nombre} - {x.Color} - Talle {x.TalleArticulo} ", Value = $"{x.Id}" });

            return View(ventaModel);
        }

        public async Task<IActionResult> ActualizarPrecio(int idArticulo, VentaTipo ventatipo, decimal cantidad)
        {
            if (idArticulo == 0)
                return Json(0);

            var Articulo = await _articuloService.BuscarPorId(idArticulo);

            var ArticuloPrecio = new decimal(1);
            if (ventatipo == VentaTipo.Mayorista)
            {
                ArticuloPrecio = Articulo.PrecioMayorista;
            }
            else
            {
                ArticuloPrecio = Articulo.PrecioMinorista;
            }

            return Json(ArticuloPrecio * cantidad);
        }

        public async Task<IActionResult> AgregarDetalle(VentaDetalleModel data)
        {
            int idArticulo = data.IdArticulo;
            var articulo = await _articuloService.BuscarPorId(idArticulo);
            var categoria = await _articuloCategoriaService.BuscarPorId(articulo.IdArticuloCategoria);
            data.CodigoArticulo = articulo.CodigoArticulo;
            data.NombreArticulo = articulo.Nombre;
            data.DescripcionArticulo = articulo.Descripcion;
            data.ArticuloCategoria = categoria.Descripcion;
            data.ColorArticulo = articulo.Color;
            data.TalleArticulo = articulo.TalleArticulo;

            return PartialView("_VentaArticuloDetalle", data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(VentaModel ventaModel)
        {
            var ventaDetalles = new List<NuevaVentaDetalleModel>();
            foreach (var detalle in ventaModel.VentaDetalleModels)
            {
                ventaDetalles.Add(new NuevaVentaDetalleModel()
                {
                    IdArticulo = detalle.IdArticulo,
                    Cantidad = detalle.Cantidad
                });
            }
            await _ventaService.CrearVenta(ventaModel.IdCliente, ventaModel.VentaTipo, ventaDetalles);
            return Json(new { redirectToUrl = @Url.Action("Index", "Venta", new { area = "sucursal" }) });
            //return RedirectToAction("Index");

        }
        public async Task<IActionResult> ActualizarDetalleVenta(int idVenta)
        {
            try
            {
                var venta = await _ventaService.BuscarPorId(idVenta);

                return PartialView("_VentaIndexDetalle", venta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IActionResult> ActualizarDetallePagos(int idVenta)
        {
            var cuentaCorrienteCliente = await _cuentaCorrienteService.GetCuentaCorrientesPorVenta(idVenta);
            var venta = await _ventaService.BuscarPorId(idVenta);
            var dato = venta.MontoTotal;
            ViewBag.TotalVenta = venta.MontoTotal;

            return PartialView("_VentaIndexPagos", cuentaCorrienteCliente);
        }

        public async Task<IActionResult> CobrarVenta(int idVenta, TipoPago tipoPago, decimal montoPagado)
        {
            await _ventaService.AgregarPagoVenta(idVenta, tipoPago, montoPagado);
            var VentasList = await _ventaService.GetVentas();

            return PartialView("_VentaIndexTable", VentasList);
        }

    }
}
