using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        private IProveedorInsumoService _proveedorInsumoService;
        private IMapper _mapper;

        public IInsumoService _insumoService { get; }

        public ProveedorController(IProveedorService proveedorService, IInsumoService insumoService, IMapper mapper, IProveedorInsumoService proveedorInsumoService)
        {
            _proveedorService = proveedorService;
            _insumoService = insumoService;
            _proveedorInsumoService = proveedorInsumoService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var proveedores = await _proveedorService.GetProveedores();
            return View(proveedores);
        }

        public async Task<IActionResult> CrearEditarProveedor(int idProveedor)
        {
            Proveedor proveedor;

            if (idProveedor != 0) // 0 = crear
                proveedor = await _proveedorService.BuscarPorId(idProveedor);
            else
                proveedor = new Proveedor();

            return View(proveedor);
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Proveedor proveedor)
        {
            if (!ModelState.IsValid)
                return View("CrearEditarProveedor", proveedor);

            await _proveedorService.CrearProveedor(proveedor);
            return RedirectToAction("Index");
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Proveedor proveedor)
        {
            if (!ModelState.IsValid)
                return View("CrearEditarProveedor", proveedor);

            await _proveedorService.EditarProveedor(proveedor);
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Eliminar(Proveedor proveedor)
        {
            var result = false;
            if (ModelState.IsValid)
            {
                result = await _proveedorService.EliminarProveedor(proveedor);
            }
            return Ok(result);
        }

        public async Task<IActionResult> AsignarProveedorInsumo(int idProveedor)
        {
            var proveedor = await _proveedorService.BuscarPorId(idProveedor);
            ViewBag.ProveedorSeleccionado = proveedor;


            var insumos = await _insumoService.GetInsumos();
            ViewBag.InsumosDesplegable = insumos.Select(x => new DesplegableModel()
            {
                Id = x.Id,
                Descripcion = x.Descripcion
            });

            ViewBag.Insumos = insumos.Select(x => new SelectListItem() { Text = $"{x.Nombre}", Value = $"{x.Id}" }).GroupBy(p => new { p.Text }).Select(g => g.First()).ToList();

            //ProveedorInsumoModel proveedorInsumo = new ProveedorInsumoModel();
            //var proveedor = await _proveedorService.GetProveedores();
            //var insumos = await _insumoService.GetInsumos();

            //ViewBag.Proveedores = proveedor.Select(x => new SelectListItem() { Text = $"{x.Nombre}", Value = $"{x.Id}" }).GroupBy(p => new { p.Text }).Select(g => g.First()).ToList();

            //proveedorInsumo.IdProveedor = idProveedor;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EliminarDetalle(int id)
        {
            return Ok(await _proveedorInsumoService.EliminarInsumoDeProveedor(id));
        }

        public async Task<IActionResult> AgregarDetalle(ProveedorInsumo data)
        {
            var nuevoLineaReceta = await _proveedorInsumoService.BuscarProveedorInsumoPorId(await _proveedorInsumoService.AgregarInsumoAProveedor(data));

            var ret = new List<ProveedorInsumo>();

            ret.Add(nuevoLineaReceta);
            //var proveedorInsumoDb = _mapper.Map<ProveedorInsumoModel>(nuevoLineaReceta);
            //proveedorInsumoDb.Proveedor = data.Proveedor;
            //proveedorInsumoDb.ProveedoresInsumos = data.ProveedoresInsumos;
            //proveedorInsumoDb.Insumos = data.Insumos;
            //proveedorInsumoDb.Insumo = data.Insumo;

            return PartialView("_InsumoProveedor", nuevoLineaReceta);
        }

    }

}

