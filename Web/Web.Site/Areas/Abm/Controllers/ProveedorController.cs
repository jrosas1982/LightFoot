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
        

        public ProveedorController()
        {
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> CrearEditarProveedor(int IdProveedor)
        {
            Proveedor proveedor;

            ProveedorModel proveedorModel = new ProveedorModel()
            {
                
            };
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

