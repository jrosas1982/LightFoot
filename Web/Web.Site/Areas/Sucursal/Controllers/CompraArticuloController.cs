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

        public CompraArticuloController(ICompraArticuloService compraArticuloService, IArticuloService articuloService, IProveedorService proveedorService)
        {
            _compraArticuloService = compraArticuloService;
            _articuloService = articuloService;
            _proveedorService = proveedorService;
        }

        public async Task<IActionResult> Index()
        {
            var compraArticulosList = await _compraArticuloService.GetCompras();

            return View(compraArticulosList);
        }

        public async Task<IActionResult> ComprarInsumos()
        {            
            var proveedoresListTask = _proveedorService.GetProveedores();
            var articulosListTask = _articuloService.GetArticulos();

            await Task.WhenAll(proveedoresListTask, articulosListTask);

            var proveedoresList = proveedoresListTask.Result;
            var articulosList = articulosListTask.Result;

            CompraArticuloModel compraArticuloModel = new CompraArticuloModel();

            compraArticuloModel.Insumos = articulosList.Select(x => new SelectListItem() { Text = $"{x.Nombre}", Value = $"{x.Id}" });
            compraArticuloModel.Proveedores = proveedoresList.Select(x => new SelectListItem() { Text = $"{x.Nombre}", Value = $"{x.Id}" });

            return View(compraArticuloModel);
        }

        public async Task<IActionResult> FiltrarProveedores(int idInsumo)
        {
            var proveedores = await _proveedorService.GetProveedores();

            var proveedoresInsumo = proveedores
                .Where(x => x.ProveedorInsumos.Any(y => y.IdInsumo == idInsumo))
                .Select(x => new SelectListItem() { Text = $"{x.Nombre}", Value = $"{x.Id}" });

            return PartialView("_CompraInsumoProveedores", proveedoresInsumo);
        }

        //public async Task<IActionResult> ActualizarProveedorPreferidoSugerido(int idInsumo)
        //{
        //    var insumo = await _insumosService.BuscarPorId(idInsumo);
        //    var proveedores = await _proveedorService.GetProveedores();
        //    var proveedoresInsumo = proveedores.Where(x => x.ProveedorInsumos.Any(y => y.IdInsumo == idInsumo));

        //    if (!proveedoresInsumo.Any())
        //        return PartialView("_CompraInsumoProveedorPreferidoSugerido", null);

        //    CompraInsumoProveedorModel proveedoresModel = new CompraInsumoProveedorModel();

        //    var proveedorSugerido = proveedoresInsumo.OrderByDescending(x => x.Calificacion).First();
        //    proveedoresModel.ProveedorSugerido = proveedorSugerido;

        //    if (insumo.IdProveedorPreferido == null)
        //        return PartialView("_CompraInsumoProveedorPreferidoSugerido", proveedoresModel);

        //    var proveedorPreferido = proveedoresInsumo.Single(x => x.Id == (int)insumo.IdProveedorPreferido);
        //    proveedoresModel.ProveedorPreferido = proveedorPreferido;

        //    return PartialView("_CompraInsumoProveedorPreferidoSugerido", proveedoresModel);
        //}

        //public async Task<IActionResult> ActualizarPrecio(int idInsumo, int idProveedor, decimal cantidad)
        //{
        //    if (idInsumo == 0 || idProveedor == 0)
        //        return Json(0);

        //    var precioInsumo = await _proveedorInsumoService.GetPrecioInsumo(idInsumo, idProveedor);            

        //    return Json(precioInsumo * cantidad);
        //}

        //public async Task<IActionResult> ActualizarComentario(int idProveedor, int idInsumo)
        //{
        //    var insumo = await _insumosService.BuscarPorId(idInsumo);
        //    var idproveedorPreferido = insumo.IdProveedorPreferido;

        //    if (idProveedor == idproveedorPreferido && idproveedorPreferido != null)
        //        return Json(true);

        //    return Json(false);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Crear(CompraInsumoModel comprainsumoModel)
        //{
        //    try
        //    {      
        //        IList<NuevaCompraInsumoModel> compra = new List<NuevaCompraInsumoModel>();

        //        foreach (var detalle in comprainsumoModel.CompraInsumoDetalleModels)
        //        {
        //            compra.Add(new NuevaCompraInsumoModel()
        //            {
        //                Cantidad = detalle.Cantidad,
        //                Comentario = detalle.Comentario,
        //                IdInsumo = detalle.IdInsumo,
        //                IdProveedor = detalle.IdProveedor
        //            });
        //        }
        //        await _compraInsumoService.CrearCompra(compra);

        //        return RedirectToAction("Index");
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public async Task<IActionResult> ActualizarDetalleCompra(int idCompra)
        {
            try
            {
                var compra = await _compraArticuloService.BuscarPorId(idCompra);

                return PartialView("_CompraArticuloIndexDetalle", compra);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public async Task<IActionResult> RecibirCompra(int idCompra, long nroRemito, int tiempoCalificacion, int distanciaCalificacion, int precioCalificacion, int calidadCalificacion)
        //{
        //    try
        //    {
        //        var resp = await _compraInsumoService.RecibirCompra(idCompra, nroRemito, tiempoCalificacion, distanciaCalificacion, precioCalificacion, calidadCalificacion);

        //        var compraInsumoList = await _compraInsumoService.GetCompras();

        //        return PartialView("_CompraInsumoIndexTable", compraInsumoList);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public async Task<IActionResult> PagarCompra(int idCompra, TipoPago tipoPago, decimal montoPagado)
        //{
        //    try
        //    {
        //        var resp = await _compraInsumoService.AgregarPagoCompra(idCompra, tipoPago, montoPagado);
        //        var compraInsumoList = await _compraInsumoService.GetCompras();

        //        return PartialView("_CompraInsumoIndexTable", compraInsumoList);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

        public async Task<IActionResult> AgregarDetalle(CompraArticuloDetalleModel data)
        {
            //var insumosList = await _insumosService.GetInsumos();
            //var insumo = insumosList.FirstOrDefault(x => x.Nombre == data.Insumo.Nombre);

            //var proveedoresList = await _proveedorService.GetProveedores();
            //var proveedor = proveedoresList.FirstOrDefault(x => x.Nombre == data.Proveedor.Nombre);


            //var insumo = await _insumosService.BuscarPorId(data.IdInsumo);
            //var proveedor = await _proveedorService.BuscarPorId(data.IdProveedor);

            //data.IdInsumo = insumo.Id;
            //data.Insumo = insumo;
            //data.Proveedor = proveedor;
                        
            return PartialView("_CompraArticuloDetalle", data);
        }



    }

}