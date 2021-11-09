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

namespace Web.Site.Areas
{
    //[Authorize (Policy = Policies.IsControlador)]
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

        public async Task<IActionResult> Index()
        {
            var compraInsumoList = await _compraInsumoService.GetCompras();

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

        //public async Task<IActionResult> PrecioCompra(string idInsumo, string idProveedor, string Cantidad)
        //{
        //    //var articulosList = await _articuloService.GetArticulos();
        //    var IdInsumo = int.Parse(idInsumo);
        //    var IdProveedor = int.Parse(idProveedor);

        //    var proveedor = await _proveedorService.BuscarPorId(IdProveedor);


        //    string precio = "22";
        //    //solicitudModel.Colores = articulosList.Where(x => x.TalleArticulo == NumeroTalle).Select(c => new SelectListItem() { Text = $"{c.Color}", Value = $"{c.Color}" }).GroupBy(p => new { p.Text }).Select(g => g.First()).ToList(); ;
        //    return Json(precio);
        //}

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

            var proveedorInsumo = await _proveedorInsumoService.BuscarProveedorInsumo(idInsumo, idProveedor);            

            return Json(proveedorInsumo.Precio * cantidad);
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
            try
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

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IActionResult> ActualizarDetalleCompra(int idCompra)
        {
            try
            {
                 var compra = await _compraInsumoService.BuscarPorId(idCompra);

                 return PartialView("_CompraInsumoIndexDetalle", compra);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IActionResult> AgregarDetalle(CompraInsumoDetalleModel data)
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
                        
            return PartialView("_CompraInsumoDetalle", data);
        }

    }

}