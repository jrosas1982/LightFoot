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
            return View(proveedores.ToList());
        }

        public async Task<IActionResult> CrearEditarProveedor(int IdProveedor)
        {
            Proveedor proveedor;

            if (IdProveedor != 0) // 0 crear
                proveedor = await _proveedorService.BuscarPorId(IdProveedor);
            else
                proveedor = new Proveedor();

            ProveedorModel proveedorModel = new ProveedorModel(proveedor);

            return View(proveedorModel);
        }

        public async Task<IActionResult> Crear(Proveedor proveedor)
        {
            await _proveedorService.CrearProveedor(proveedor);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Editar(Proveedor proveedor)
        {
            await _proveedorService.EditarProveedor(proveedor);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Eliminar(Proveedor proveedor)
        {
            var result = await _proveedorService.EliminarProveedor(proveedor);
            return Ok(result);
        }

    }

}

