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
    public class InsumoController : Controller
    {
        private IInsumoService _insumoService;

        public InsumoController(IInsumoService insumoService)
        {
            _insumoService = insumoService;
        }

        public async Task<IActionResult> Index()
        {
            var usuariosList = await _insumoService.GetInsumos();
            return View(usuariosList.ToList());
        }

        public async Task<IActionResult> CrearEditarInsumo(int IdInsumo)
        {
            Insumo insumo;

            if (IdInsumo != 0) // 0 crear
                insumo = await _insumoService.BuscarPorId(IdInsumo);
            else
                insumo = new Insumo();

            InsumoModel insumoModel = new InsumoModel(insumo);

            return View(insumoModel);
        }

        public async Task<IActionResult> Crear(Insumo insumo)
        {
            if (!ModelState.IsValid)
            {
                return View(insumo);
            }
            await _insumoService.CrearInsumo(insumo);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Editar(Insumo insumo)
        {
            await _insumoService.EditarInsumo(insumo);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Eliminar(Insumo insumo)
        {
            var result = await _insumoService.EliminarInsumo(insumo);
            return Ok(result);
        }
    }
}
