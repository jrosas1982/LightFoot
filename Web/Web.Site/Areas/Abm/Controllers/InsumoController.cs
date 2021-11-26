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
            var proveedoresList = await _proveedorService.GetProveedores();
            ViewBag.TypeaheadNombreInsumo = insumosList.Select(x => x.Nombre).Distinct();
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
            return Json(new { redirectToUrl = @Url.Action("Index", "Insumo", new { area = "abm" }) });
            //return RedirectToAction("Index");
        }

        public async Task<IActionResult> Editar(Insumo insumo)
        {
            await _insumoService.EditarInsumo(insumo);
            return Json(new { redirectToUrl = @Url.Action("Index", "Insumo", new { area = "abm" }) });
            //return RedirectToAction("Index");
        }

        public async Task<IActionResult> Eliminar(Insumo insumo)
        {
            var result = await _insumoService.EliminarInsumo(insumo);
            return Ok(result);
        }

        public async Task<IActionResult> FiltrarInsumo(string nombreInsumo)
        {
            var Insumos = await _insumoService.GetInsumos();

            if (!string.IsNullOrWhiteSpace(nombreInsumo))
            {
                Insumos = Insumos.Where(x => x.Id.ToString().Equals(nombreInsumo.ToLower())
                                                            || x.Nombre.ToLower().Equals(nombreInsumo.ToLower())).ToList();

                if (!Insumos.Any())
                    Insumos = Insumos.Where(x => x.Id.ToString().Contains(nombreInsumo.ToLower())
                                                            || x.Nombre.ToLower().Contains(nombreInsumo.ToLower())).ToList();
            }

            return PartialView("_InsumoIndexTable", Insumos);
        }
    }
}
