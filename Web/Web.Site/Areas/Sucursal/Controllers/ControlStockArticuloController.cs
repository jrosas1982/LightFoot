using System.Linq;
using System.Threading.Tasks;
using Core.Aplicacion.Interfaces;
using Core.Dominio.AggregatesModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Site.Helpers;
using Web.Site.Areas.Abm;

namespace Web.Site.Areas
{
    [Authorize]
    [Area("sucursal")]
    [Route("[area]/[controller]/[action]")]
    public class ControlStockArticuloController : CustomController
    {
        private IControlStockArticuloService _controlStockArticuloService;

        public ControlStockArticuloController(IControlStockArticuloService controlStockArticuloService)
        {
            _controlStockArticuloService = controlStockArticuloService;
        }

        public async Task<IActionResult> Index()
        {
            var stockArticuloList = await _controlStockArticuloService.GetArticuloStock();
            //if (User.GetUserRole().ToString() != "Administrador") {
            //    return View(stockArticuloList.Where(x => x.IdSucursal == int.Parse(User.GetUserIdSucursal())).ToList());
            //}
            return View(stockArticuloList);
        }

        public async Task<IActionResult> CrearEditarStockArticulo(int IdStockArticulo)
        {
            ArticuloStock articuloStock;


            if (IdStockArticulo != 0)
            {// 0 = crear
                articuloStock = await _controlStockArticuloService.BuscarPorId(IdStockArticulo);
                ViewBag.ArticuloNombre = $"{articuloStock.Articulo.Nombre} - {articuloStock.Articulo.Color} - {articuloStock.Articulo.TalleArticulo}";
            }
            else
                articuloStock = new ArticuloStock();

            ArticuloStockModel articuloStockModel = new ArticuloStockModel(articuloStock);

            return View(articuloStockModel);
        }

        public async Task<IActionResult> Crear(ArticuloStock articuloStock)
        {
            articuloStock.IdSucursal = int.Parse(User.GetUserIdSucursal());
            await _controlStockArticuloService.CrearArticuloStock(articuloStock);
            return Json(new { redirectToUrl = @Url.Action("Index", "ControlStockArticulo", new { area = "abm" }) });
            //return RedirectToAction("Index");
        }

        public async Task<IActionResult> Editar(ArticuloStock articuloStock)
        {
            articuloStock.IdSucursal = int.Parse(User.GetUserIdSucursal());
            await _controlStockArticuloService.EditarArticuloStock(articuloStock);
            return Json(new { redirectToUrl = @Url.Action("Index", "ControlStockArticulo", new { area = "abm" }) });
            //return RedirectToAction("Index");
        }

        public async Task<IActionResult> Eliminar(ArticuloStock articuloStock)
        {
            var result = await _controlStockArticuloService.EliminarArticuloStock(articuloStock);
            return Ok(result);
        }

    }

}

