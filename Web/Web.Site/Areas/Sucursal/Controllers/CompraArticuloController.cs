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
    //[Authorize (Policy = Policies.IsControlador)]
    [Area("sucursal")]
    [Route("[area]/[controller]/[action]")]
    public class CompraArticuloController : CustomController
    {
        private ICompraArticuloService _compraArticuloService;
        private IArticuloService _articuloService;
        private IProveedorService _proveedorService;
        private IProveedorArticuloService _proveedorArticuloService;

        public CompraArticuloController(ICompraArticuloService compraArticuloService, IArticuloService articuloService, IProveedorService proveedorService, IProveedorArticuloService proveedorArticuloService)
        {
            _compraArticuloService = compraArticuloService;
            _articuloService = articuloService;
            _proveedorService = proveedorService;
            _proveedorArticuloService = proveedorArticuloService;
        }


        public async Task<IActionResult> FiltrarCompras(string nombreCompra)
        {
            var compraArticulosList = await _compraArticuloService.GetCompras();

            if (!string.IsNullOrWhiteSpace(nombreCompra))
            {
                compraArticulosList = compraArticulosList.Where(x => x.Id.ToString().Equals(nombreCompra.ToLower())
                                                            || x.NroRemito.ToString().Equals(nombreCompra.ToLower())
                                                            || x.MontoTotal.ToString().Equals(nombreCompra.ToLower())
                                                            || x.Proveedor.Nombre.ToLower().Equals(nombreCompra.ToLower()));
                if (!compraArticulosList.Any())
                    compraArticulosList = compraArticulosList.Where(x => x.Id.ToString().Contains(nombreCompra.ToLower())
                                            || x.NroRemito.ToString().Contains(nombreCompra.ToLower())
                                            || x.MontoTotal.ToString().Contains(nombreCompra.ToLower())
                                            || x.Proveedor.Nombre.ToLower().Contains(nombreCompra.ToLower()));
            }

            return PartialView("_CompraArticuloIndexTable", compraArticulosList);
        }

        public async Task<IActionResult> Index()
        {
            var compraArticulosList = await _compraArticuloService.GetCompras();

            ViewBag.TypeaheadNumCompra = compraArticulosList.Select(x => x.Id.ToString()).Distinct();
            ViewBag.TypeaheadRemito = compraArticulosList.Where(x => !string.IsNullOrWhiteSpace(x.NroRemito.ToString())).Select(x => x.NroRemito.ToString()).Distinct();
            ViewBag.TypeaheadTotal = compraArticulosList.Select(x => x.MontoTotal.ToString()).Distinct();
            ViewBag.TypeaheadProveedor = compraArticulosList.Select(x => x.Proveedor.Nombre).Distinct();

            ViewBag.TiposPago = await _compraArticuloService.GetTiposPago();

            return View(compraArticulosList);
        }

        public async Task<IActionResult> ComprarArticulos()
        {
            var proveedoresListTask = _proveedorService.GetProveedores();
            var articulosListTask = _articuloService.GetArticulos();

            await Task.WhenAll(proveedoresListTask, articulosListTask);

            var proveedoresList = proveedoresListTask.Result;
            var articulosList = articulosListTask.Result;

            CompraArticuloModel compraArticuloModel = new CompraArticuloModel();

            compraArticuloModel.Articulos = articulosList.Select(x => new SelectListItem() { Text = $"{x.ArticuloCategoria.Descripcion} - {x.CodigoArticulo} - {x.Nombre} - {x.Color} - Talle {x.TalleArticulo} ", Value = $"{x.Id}" });
            compraArticuloModel.Proveedores = proveedoresList.Select(x => new SelectListItem() { Text = $"{x.Nombre}", Value = $"{x.Id}" });

            return View(compraArticuloModel);
        }

        public async Task<IActionResult> FiltrarProveedores(int idArticulo)
        {
            var proveedores = await _proveedorService.GetProveedores();

            var proveedoresArticulo = proveedores
                .Where(x => x.ProveedorArticulos.Any(y => y.IdArticulo == idArticulo))
                .Select(x => new SelectListItem() { Text = $"{x.Nombre}", Value = $"{x.Id}" });

            return PartialView("_CompraArticuloProveedores", proveedoresArticulo);
        }

        public async Task<IActionResult> ActualizarPrecio(int idArticulo, int idProveedor, decimal cantidad)
        {
            if (idArticulo == 0 || idProveedor == 0)
                return Json(0);

            var precioInsumo = await _proveedorArticuloService.GetPrecioArticulo(idArticulo, idProveedor);

            return Json(precioInsumo * cantidad);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(CompraArticuloModel compraArticuloModel)
        {
            IList<NuevaCompraArticuloModel> compra = new List<NuevaCompraArticuloModel>();
            foreach (var detalle in compraArticuloModel.CompraArticuloDetalleModels)
            {
                compra.Add(new NuevaCompraArticuloModel()
                {
                    Cantidad = detalle.Cantidad,
                    Comentario = detalle.Comentario,
                    IdArticulo = detalle.IdArticulo,
                    IdProveedor = detalle.IdProveedor
                });
            }
            await _compraArticuloService.CrearCompra(compra);
            return Json(new { redirectToUrl = @Url.Action("Index", "CompraArticulo", new { area = "sucursal" }) });
            //return RedirectToAction("Index");
        }

        public async Task<IActionResult> ActualizarDetalleCompra(int idCompra)
        {
            var compra = await _compraArticuloService.BuscarPorId(idCompra);

            return PartialView("_CompraArticuloIndexDetalle", compra);
        }

        public async Task<IActionResult> RecibirCompra(int idCompra, long nroRemito, int tiempoCalificacion, int distanciaCalificacion, int precioCalificacion, int calidadCalificacion)
        {
            var resp = await _compraArticuloService.RecibirCompra(idCompra, nroRemito);

            var compraArticulosList = await _compraArticuloService.GetCompras();

            ViewBag.TypeaheadNumCompra = compraArticulosList.Select(x => x.Id.ToString()).Distinct();
            ViewBag.TypeaheadRemito = compraArticulosList.Where(x => !string.IsNullOrWhiteSpace(x.NroRemito.ToString())).Select(x => x.NroRemito.ToString()).Distinct();
            ViewBag.TypeaheadTotal = compraArticulosList.Select(x => x.MontoTotal.ToString()).Distinct();
            ViewBag.TypeaheadProveedor = compraArticulosList.Select(x => x.Proveedor.Nombre).Distinct();

            return PartialView("_CompraArticuloIndexTable", compraArticulosList);
        }

        public async Task<IActionResult> PagarCompra(int idCompra, TipoPago tipoPago, decimal montoPagado)
        {
            var resp = await _compraArticuloService.AgregarPagoCompra(idCompra, tipoPago, montoPagado);
            var compraArticuloList = await _compraArticuloService.GetCompras();

            return PartialView("_CompraArticuloIndexTable", compraArticuloList);
        }


        public async Task<IActionResult> CargarPagosRealizados(int idCompra)
        {
            var resp = await _compraArticuloService.GetPagosRealizados(idCompra);

            return PartialView("_CompraArticuloIndexPagos", resp);
        }

        public async Task<IActionResult> AgregarDetalle(CompraArticuloDetalleModel data)
        {
            return PartialView("_CompraArticuloDetalle", data);
        }

        public async Task<IActionResult> CargarInfoProveedor(int idCompra)
        {
            var response = await _compraArticuloService.BuscarPorId(idCompra);
            return PartialView("_CompraArticuloProveedorCabeceraDetalle", response.Proveedor);

        }

    }

}