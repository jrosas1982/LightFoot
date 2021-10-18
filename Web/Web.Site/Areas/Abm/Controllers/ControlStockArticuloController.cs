using System.Linq;
using System.Threading.Tasks;
using Core.Aplicacion.Interfaces;
using Core.Dominio.AggregatesModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Site.Areas
{
    [Authorize]
    [Area("abm")]
    [Route("[area]/[controller]/[action]")]
    public class ControlStockArticuloController : Controller
    {
        private IControlStockArticuloService _controlStockArticuloService;

        public ControlStockArticuloController(IControlStockArticuloService controlStockArticuloService)
        {
            _controlStockArticuloService = controlStockArticuloService;
        }

        public async Task<IActionResult> Index()
        {
            var stockArticuloList = await _controlStockArticuloService.GetArticuloStock();
            return View(stockArticuloList.ToList());
        }

        public async Task<IActionResult> CrearEditarArticulo(int IdArticuloStock)
        {
            ArticuloStock articuloStock;

            if (IdArticuloStock != 0) // 0 = crear
                articuloStock = await _controlStockArticuloService.BuscarPorId(IdArticuloStock);
            else
                articuloStock = new ArticuloStock();

            ArticuloStockModel articuloStockModel = new ArticuloStockModel(articuloStock);

            return View(articuloStockModel);
        }

        public async Task<IActionResult> Crear(ArticuloStock articuloStock)
        {
            await _controlStockArticuloService.CrearArticuloStock(articuloStock);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Editar(ArticuloStock articuloStock)
        {
            await _controlStockArticuloService.EditarArticuloStock(articuloStock);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Eliminar(ArticuloStock articuloStock)
        {
            var result = await _controlStockArticuloService.EliminarArticuloStock(articuloStock);
            return Ok(result);
        }

    }

}

