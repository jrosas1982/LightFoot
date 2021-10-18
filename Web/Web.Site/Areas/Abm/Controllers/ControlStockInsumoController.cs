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
    public class ControlStockInsumoController : Controller
    {
        private IControlStockInsumoService _controlStockInsumoService;

        public ControlStockInsumoController(IControlStockInsumoService controlStockInsumoService)
        {
            _controlStockInsumoService = controlStockInsumoService;
        }

        public async Task<IActionResult> Index()
        {
            var stockArticuloList = await _controlStockInsumoService.GetInsumoStock();
            return View(stockArticuloList.ToList());
        }

        public async Task<IActionResult> CrearEditarArticulo(int IdInsumoStock)
        {
            InsumoStock insumoStock;

            if (IdInsumoStock != 0) // 0 = crear
                insumoStock = await _controlStockInsumoService.BuscarPorId(IdInsumoStock);
            else
                insumoStock = new InsumoStock();

            InsumoStockModel insumoStockModel = new InsumoStockModel(insumoStock);

            return View(insumoStockModel);
        }

        public async Task<IActionResult> Crear(InsumoStock insumoStock)
        {
            await _controlStockInsumoService.CrearInsumoStock(insumoStock);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Editar(InsumoStock insumoStock)
        {
            await _controlStockInsumoService.EditarInsumoStock(insumoStock);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Eliminar(InsumoStock insumoStock)
        {
            var result = await _controlStockInsumoService.EliminarInsumoStock(insumoStock);
            return Ok(result);
        }

    }

}

