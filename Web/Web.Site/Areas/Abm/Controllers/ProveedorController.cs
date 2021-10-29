using System.Linq;
using System.Threading.Tasks;
using Core.Aplicacion.Interfaces;
using Core.Dominio.AggregatesModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public IInsumoService _insumoService { get; }

        public ProveedorController(IProveedorService proveedorService, IInsumoService insumoService)
        {
            _proveedorService = proveedorService;
            _insumoService = insumoService;
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

        public async Task<IActionResult> AsignarInsumoProveedor(int idProveedor)
        {
            ProveedorInsumoModel proveedorInsumo = new ProveedorInsumoModel();
            var proveedor = await _proveedorService.GetProveedores();
            var insumos = await _insumoService.GetInsumos();

            ViewBag.Proveedores = proveedor.Select(x => new SelectListItem() { Text = $"{x.Nombre}", Value = $"{x.Id}" }).GroupBy(p => new { p.Text }).Select(g => g.First()).ToList();
            ViewBag.Insumos = insumos.Select(x => new SelectListItem() { Text = $"{x.Nombre}", Value = $"{x.Id}" }).GroupBy(p => new { p.Text }).Select(g => g.First()).ToList();

            proveedorInsumo.IdProveedor = idProveedor;

            return View(proveedorInsumo);

        }
        [HttpPost]
        public async Task<IActionResult> EliminarDetalle(int id)
        {
            return Ok(await _insumoService.EliminarInsumo(await _insumoService.BuscarPorId(id)));
        }

        public async Task<IActionResult> AgregarDetalle(ProveedorInsumoModel data)
        {
            if (data.Id != 0)
            {
             
                return PartialView("_InsumoProveedor", data);
            }
            else
                return PartialView("_InsumoProveedor", data);
        }

    }

}

