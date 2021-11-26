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
    public class SucursalController : CustomController
    {
        private ISucursalService _sucursalService;
        public SucursalController(ISucursalService sucursalService)
        {
            _sucursalService = sucursalService;
        }

        public async Task<IActionResult> FiltrarSucursales(string nombreSucursal)
        {
            var sucursalesList = await _sucursalService.GetSucursales();

            if (!string.IsNullOrWhiteSpace(nombreSucursal))
            {
                sucursalesList = sucursalesList.Where(x => x.Nombre.ToLower().Equals(nombreSucursal.ToLower())
                                                        || x.Descripcion.ToLower().Equals(nombreSucursal.ToLower())
                                                        || x.Direccion.ToLower().Equals(nombreSucursal.ToLower()));
                if (!sucursalesList.Any())
                    sucursalesList = sucursalesList.Where(x => x.Nombre.ToLower().Contains(nombreSucursal.ToLower())
                                                            || x.Descripcion.ToLower().Contains(nombreSucursal.ToLower())
                                                            || x.Direccion.ToLower().Contains(nombreSucursal.ToLower()));
            }

            return PartialView("_SucursalIndexTable", sucursalesList);
        }

        public async Task<IActionResult> Index()
        {
            var sucursalesList = await _sucursalService.GetSucursales();

            ViewBag.TypeaheadNombre = sucursalesList.Select(x => x.Nombre).Distinct();
            ViewBag.TypeaheadDescripcion = sucursalesList.Select(x => x.Descripcion).Distinct();
            ViewBag.TypeaheadDireccion = sucursalesList.Select(x => x.Direccion).Distinct();
            
            return View(sucursalesList.ToList());
        }


        public async Task<IActionResult> CrearEditarSucursal(int IdSucursal)
        {
            Sucursal sucursal;

            if (IdSucursal != 0) // 0 = crear
                sucursal = await _sucursalService.BuscarPorId(IdSucursal);
            else
                sucursal = new Sucursal();

            return View(sucursal);
        }

        public async Task<IActionResult> Crear(Sucursal sucursal)
        {
            if (ModelState.IsValid)
            {
                await _sucursalService.CrearSucursal(sucursal);
            }
            return Json(new { redirectToUrl = @Url.Action("Index", "Sucursal", new { area = "abm" }) });
            //return RedirectToAction("Index");
        }

        public async Task<IActionResult> Editar(Sucursal sucursal)
        {
            if (ModelState.IsValid)
            {
                await _sucursalService.EditarSucursal(sucursal);
            }
            return Json(new { redirectToUrl = @Url.Action("Index", "Sucursal", new { area = "abm" }) });
            //return RedirectToAction("Index");
        }

        public async Task<IActionResult> Eliminar(Sucursal sucursal)
        {
            var result = false;
            if (ModelState.IsValid)
            {
                result = await _sucursalService.EliminarSucursal(sucursal);
            }
            return Ok(result);
        }


    }
}
