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
using Web.Site.Areas.Fabrica;
using Core.Dominio.CoreModelHelpers;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Core.Aplicacion.Auth;

namespace Web.Site.Areas
{
    [Authorize]
    [Area("fabrica")]
    [Route("[area]/[controller]/[action]")]
    public class CompraInsumoController : CustomController
    {
        private ICompraInsumoService _compraInsumoService;
        private IInsumoService _insumosService;
        private IProveedorService _proveedorService;
        private IProveedorInsumoService _proveedorInsumoService;

        public CompraInsumoController(ICompraInsumoService compraInsumoService, IInsumoService insumosService, IProveedorService proveedorService, IProveedorInsumoService proveedorInsumoService)
        {
            _compraInsumoService = compraInsumoService;
            _insumosService = insumosService;
            _proveedorService = proveedorService;
            _proveedorInsumoService = proveedorInsumoService;
        }


        public async Task<IActionResult> FiltrarCompras(string nombreCompra)
        {
            var compraInsumoList = await _compraInsumoService.GetCompras();

            if (!string.IsNullOrWhiteSpace(nombreCompra))
            {
                compraInsumoList = compraInsumoList.Where(x => x.Id.ToString().Equals(nombreCompra.ToLower())
                                                            || x.NroRemito.ToString().Equals(nombreCompra.ToLower())
                                                            || x.MontoTotal.ToString().Equals(nombreCompra.ToLower())
                                                            || x.Proveedor.Nombre.ToLower().Equals(nombreCompra.ToLower()));
                if (!compraInsumoList.Any())
                    compraInsumoList = compraInsumoList.Where(x => x.Id.ToString().Contains(nombreCompra.ToLower())
                                            || x.NroRemito.ToString().Contains(nombreCompra.ToLower())
                                            || x.MontoTotal.ToString().Contains(nombreCompra.ToLower())
                                            || x.Proveedor.Nombre.ToLower().Contains(nombreCompra.ToLower()));
            }

            return PartialView("_CompraInsumoIndexTable", compraInsumoList);
        }

        public async Task<IActionResult> Index()
        {
            var compraInsumoList = await _compraInsumoService.GetCompras();

            ViewBag.TypeaheadNumCompra = compraInsumoList.Select(x => x.Id.ToString()).Distinct();
            ViewBag.TypeaheadRemito = compraInsumoList.Where(x => !string.IsNullOrWhiteSpace(x.NroRemito.ToString())).Select(x => x.NroRemito.ToString()).Distinct();
            ViewBag.TypeaheadTotal = compraInsumoList.Select(x => x.MontoTotal.ToString()).Distinct();
            ViewBag.TypeaheadProveedor = compraInsumoList.Select(x => x.Proveedor.Nombre).Distinct();

            ViewBag.TiposPago = await _compraInsumoService.GetTiposPago();

            return View(compraInsumoList);
        }

        public async Task<IActionResult> ComprarInsumos()
        {            
            var proveedoresListTask = _proveedorService.GetProveedores();
            var insumosListTask = _insumosService.GetInsumos();

            await Task.WhenAll(proveedoresListTask, insumosListTask);

            var proveedoresList = proveedoresListTask.Result;
            var insumosList = insumosListTask.Result;

            CompraInsumoModel compraInsumoModel = new CompraInsumoModel();

            compraInsumoModel.Insumos = insumosList.Select(x => new SelectListItem() { Text = $"{x.Nombre}", Value = $"{x.Id}" });
            compraInsumoModel.Proveedores = proveedoresList.Select(x => new SelectListItem() { Text = $"{x.Nombre}", Value = $"{x.Id}" });

            return View(compraInsumoModel);
        }

        public async Task<IActionResult> FiltrarProveedores(int idInsumo)
        {
            var proveedores = await _proveedorService.GetProveedores();

            var proveedoresInsumo = proveedores
                .Where(x => x.ProveedorInsumos.Any(y => y.IdInsumo == idInsumo))
                .Select(x => new SelectListItem() { Text = $"{x.Nombre}", Value = $"{x.Id}" });

            return PartialView("_CompraInsumoProveedores", proveedoresInsumo);
        }

        public async Task<IActionResult> ActualizarProveedorPreferidoSugerido(int idInsumo)
        {
            var insumo = await _insumosService.BuscarPorId(idInsumo);
            var proveedores = await _proveedorService.GetProveedores();
            var proveedoresInsumo = proveedores.Where(x => x.ProveedorInsumos.Any(y => y.IdInsumo == idInsumo));

            if (!proveedoresInsumo.Any())
                return PartialView("_CompraInsumoProveedorPreferidoSugerido", null);
            
            CompraInsumoProveedorModel proveedoresModel = new CompraInsumoProveedorModel();

            var proveedorSugerido = proveedoresInsumo.OrderByDescending(x => x.Calificacion).First();
            proveedoresModel.ProveedorSugerido = proveedorSugerido;

            if (insumo.IdProveedorPreferido == null)
                return PartialView("_CompraInsumoProveedorPreferidoSugerido", proveedoresModel);

            var proveedorPreferido = proveedoresInsumo.Single(x => x.Id == (int)insumo.IdProveedorPreferido);
            proveedoresModel.ProveedorPreferido = proveedorPreferido;

            return PartialView("_CompraInsumoProveedorPreferidoSugerido", proveedoresModel);
        }

        public async Task<IActionResult> ActualizarPrecio(int idInsumo, int idProveedor, decimal cantidad)
        {
            if (idInsumo == 0 || idProveedor == 0)
                return Json(0);

            var precioInsumo = await _proveedorInsumoService.GetPrecioInsumo(idInsumo, idProveedor);            

            return Json(precioInsumo * cantidad);
        }

        public async Task<IActionResult> ActualizarComentario(int idProveedor, int idInsumo)
        {
            var insumo = await _insumosService.BuscarPorId(idInsumo);
            var idproveedorPreferido = insumo.IdProveedorPreferido;
            
            if (idProveedor == idproveedorPreferido && idproveedorPreferido != null)
                return Json(true);

            return Json(false);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(CompraInsumoModel comprainsumoModel)
        {
            IList<NuevaCompraInsumoModel> compra = new List<NuevaCompraInsumoModel>();
            foreach (var detalle in comprainsumoModel.CompraInsumoDetalleModels)
            {
                compra.Add(new NuevaCompraInsumoModel()
                {
                    Cantidad = detalle.Cantidad,
                    Comentario = detalle.Comentario,
                    IdInsumo = detalle.IdInsumo,
                    IdProveedor = detalle.IdProveedor
                });
            }
            await _compraInsumoService.CrearCompra(compra);
            return Json(new { redirectToUrl = @Url.Action("Index", "CompraInsumo", new { area = "fabrica" }) });
            //return RedirectToAction("Index");
        }

        public async Task<IActionResult> ActualizarDetalleCompra(int idCompra)
        {
            var compra = await _compraInsumoService.BuscarPorId(idCompra);

            return PartialView("_CompraInsumoIndexDetalle", compra);
        }

        public async Task<IActionResult> RecibirCompra(int idCompra, long nroRemito, int tiempoCalificacion, int distanciaCalificacion, int precioCalificacion, int calidadCalificacion)
        {
            await _compraInsumoService.RecibirCompra(idCompra, nroRemito, tiempoCalificacion, distanciaCalificacion, precioCalificacion, calidadCalificacion);

            var compraInsumoList = await _compraInsumoService.GetCompras();

            ViewBag.TypeaheadNumCompra = compraInsumoList.Select(x => x.Id.ToString()).Distinct();
            ViewBag.TypeaheadRemito = compraInsumoList.Where(x => !string.IsNullOrWhiteSpace(x.NroRemito.ToString())).Select(x => x.NroRemito.ToString()).Distinct();
            ViewBag.TypeaheadTotal = compraInsumoList.Select(x => x.MontoTotal.ToString()).Distinct();
            ViewBag.TypeaheadProveedor = compraInsumoList.Select(x => x.Proveedor.Nombre).Distinct();

            return PartialView("_CompraInsumoIndexTable", compraInsumoList);
        }

        public async Task<IActionResult> PagarCompra(int idCompra, TipoPago tipoPago, decimal montoPagado)
        {

            await _compraInsumoService.AgregarPagoCompra(idCompra, tipoPago, montoPagado);
            var compraInsumoList = await _compraInsumoService.GetCompras();

            return PartialView("_CompraInsumoIndexTable", compraInsumoList);
        }

        public async Task<IActionResult> CargarPagosRealizados(int idCompra)
        {
            var resp = await _compraInsumoService.GetPagosRealizados(idCompra);

            return PartialView("_CompraInsumoIndexPagos", resp);
        }

        public async Task<IActionResult> AgregarDetalle(CompraInsumoDetalleModel data)
        {
            return PartialView("_CompraInsumoDetalle", data);
        }

        public async Task<IActionResult> CargarInfoProveedor(int idCompra)
        {
            var response = await _compraInsumoService.BuscarPorId(idCompra);

            return PartialView("_CompraInsumoProveedorCabeceraDetalle", response.Proveedor);
        }

    }

}