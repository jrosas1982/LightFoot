using System.Linq;
using System.Threading.Tasks;
using Core.Aplicacion.Interfaces;
using Core.Dominio.AggregatesModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Site.Helpers;

namespace Web.Site.Areas
{
    [Authorize]
    [Area("abm")]
    [Route("[area]/[controller]/[action]")]
    public class InsumoController : CustomController
    {
        private IInsumoService _insumoService;
        private IProveedorService _proveedorService;

        public InsumoController(IInsumoService insumoService, IProveedorService proveedorService)
        {
            _insumoService = insumoService;
            _proveedorService = proveedorService;
        }

        public async Task<IActionResult> Index()
        {
            var insumosList = await _insumoService.GetInsumos();
            return View(insumosList);
        }

        public async Task<IActionResult> CrearEditarInsumo(int IdInsumo)
        {
            Insumo insumo;

            if (IdInsumo != 0) // 0 crear
                insumo = await _insumoService.BuscarPorId(IdInsumo);
            else
                insumo = new Insumo();

            var proveedores = await _insumoService.GetProveedoresInsumo(IdInsumo);

            InsumoModel insumoModel = new InsumoModel()
            {
                 Insumo = insumo,
                 Proveedores = proveedores
            };

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
