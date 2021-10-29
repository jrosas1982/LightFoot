using System.Linq;
using System.Threading.Tasks;
using Core.Aplicacion.Interfaces;
using Core.Dominio.AggregatesModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Site.Dtos;
using Web.Site.Helpers;

namespace Web.Site.Areas
{
    [Authorize]
    [Area("abm")]
    [Route("[area]/[controller]/[action]")]
    public class ProveedorController : CustomController
    {
        private IProveedorService _proveedorService;

        public ProveedorController(IProveedorService proveedorService)
        {
            _proveedorService = proveedorService;
        }

        public async Task<IActionResult> Index()
        {
            var proveedores = await _proveedorService.GetProveedores();
            return View(proveedores);
        }

        public async Task<IActionResult> CrearEditarProveedor(int idProveedor)
        {
            var proveedor = await _proveedorService.BuscarPorId(idProveedor);

            return View();
        }

        public async Task<IActionResult> Crear(Articulo articulo)
        {
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Editar(Articulo articulo)
        {
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Eliminar(Articulo articulo)
        {
            return Ok();
        }

    }

}

