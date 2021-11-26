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
using Web.Site.Areas.Abm;

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
        private IArticuloService _articuloService;
        private IProveedorArticuloService _proveedorArticuloService;
        private IInsumoService _insumoService;

        public ProveedorController(IProveedorService proveedorService, IProveedorInsumoService proveedorInsumoService, IMapper mapper, IArticuloService articuloService, IProveedorArticuloService proveedorArticuloService, IInsumoService insumoService)
        {
            _proveedorService = proveedorService;
            _proveedorInsumoService = proveedorInsumoService;
            _mapper = mapper;
            _articuloService = articuloService;
            _proveedorArticuloService = proveedorArticuloService;
            _insumoService = insumoService;
        }

        public async Task<IActionResult> FiltrarProveedores(string nombreProveedor)
        {
            var proveedoresList = await _proveedorService.GetProveedores();

            if (!string.IsNullOrWhiteSpace(nombreProveedor))
            {
                proveedoresList = proveedoresList.Where(x => x.Nombre.ToLower().Equals(nombreProveedor.ToLower())
                                                        || x.CUIT.ToString().Equals(nombreProveedor.ToLower())
                                                        || x.Direccion.ToLower().Equals(nombreProveedor.ToLower())
                                                        || x.Email.ToLower().Equals(nombreProveedor.ToLower())
                                                        || x.Calificacion.ToString().Equals(nombreProveedor.ToLower()));
                if (!proveedoresList.Any())
                    proveedoresList = proveedoresList.Where(x => x.Nombre.ToLower().Contains(nombreProveedor.ToLower())
                                                            || x.CUIT.ToString().Contains(nombreProveedor.ToLower())
                                                            || x.Direccion.ToLower().Contains(nombreProveedor.ToLower())
                                                            || x.Email.ToLower().Contains(nombreProveedor.ToLower())
                                                            || x.Calificacion.ToString().Contains(nombreProveedor.ToLower()));
            }

            return PartialView("_ProveedorIndexTable", proveedoresList);
        }

        public async Task<IActionResult> Index()
        {
            var proveedores = await _proveedorService.GetProveedores();

            ViewBag.TypeaheadNombre = proveedores.Select(x => x.Nombre).Distinct();
            ViewBag.TypeaheadCUIT = proveedores.Select(x => x.CUIT.ToString()).Distinct();
            ViewBag.TypeaheadDireccion = proveedores.Select(x => x.Direccion).Distinct();
            ViewBag.TypeaheadEmail = proveedores.Select(x => x.Email).Distinct();
            ViewBag.TypeaheadCalificacion = proveedores.Select(x => x.Calificacion.ToString()).Distinct();

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
            return Json(new { redirectToUrl = @Url.Action("Index", "Proveedor", new { area = "abm" }) });
            //return RedirectToAction("Index");
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Proveedor proveedor)
        {
            if (!ModelState.IsValid)
                return View("CrearEditarProveedor", proveedor);

            await _proveedorService.EditarProveedor(proveedor);
            return Json(new { redirectToUrl = @Url.Action("Index", "Proveedor", new { area = "abm" }) });
            //return RedirectToAction("Index");
        }


        public async Task<IActionResult> Eliminar(Proveedor proveedor)
        {
            await _proveedorService.EliminarProveedor(proveedor);

            var proveedores = await _proveedorService.GetProveedores();

            return PartialView("_ProveedorIndexTable", proveedores);
        }

        public async Task<IActionResult> AsignarProveedorArticulo(int idProveedor)
        {
            var proveedor = await _proveedorService.BuscarPorId(idProveedor);
            ViewBag.ProveedorSeleccionado = proveedor;


            var articulos = await _articuloService.GetArticulos();
            ViewBag.ArticulosDesplegable = articulos.Select(x => new DesplegableModel()
            {
                Id = x.Id,
                Descripcion = x.Descripcion
            });

            ViewBag.Articulos = articulos.Select(x => new SelectListItem() { Text = $"{x.ArticuloCategoria.Descripcion} - {x.Nombre} - {x.Color} - Talle {x.TalleArticulo} ", Value = $"{x.Id}" }).GroupBy(p => new { p.Text }).Select(g => g.First()).ToList();

            //ProveedorInsumoModel proveedorInsumo = new ProveedorInsumoModel();
            //var proveedor = await _proveedorService.GetProveedores();
            //var insumos = await _insumoService.GetInsumos();

            //ViewBag.Proveedores = proveedor.Select(x => new SelectListItem() { Text = $"{x.Nombre}", Value = $"{x.Id}" }).GroupBy(p => new { p.Text }).Select(g => g.First()).ToList();

            //proveedorInsumo.IdProveedor = idProveedor;

            return View();
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
        public async Task<IActionResult> EliminarDetalleInsumo(int id)
        {
            return Ok(await _proveedorInsumoService.EliminarInsumoDeProveedor(id));
        }

        public async Task<IActionResult> AgregarDetalleInsumo(ProveedorInsumo data)
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
        public async Task<IActionResult> AgregarDetalleArticulo(ProveedorArticulo data)
        {
            var nuevoLineaReceta = await _proveedorArticuloService.BuscarProveedorArticuloPorId(await _proveedorArticuloService.AgregarArticuloAProveedor(data));

            var ret = new List<ProveedorArticulo>();

            ret.Add(nuevoLineaReceta);
            //var proveedorInsumoDb = _mapper.Map<ProveedorInsumoModel>(nuevoLineaReceta);
            //proveedorInsumoDb.Proveedor = data.Proveedor;
            //proveedorInsumoDb.ProveedoresInsumos = data.ProveedoresInsumos;
            //proveedorInsumoDb.Insumos = data.Insumos;
            //proveedorInsumoDb.Insumo = data.Insumo;

            return PartialView("_ArticuloProveedor", nuevoLineaReceta);
        }

        [HttpPost]
        public async Task<IActionResult> EliminarDetalleArticulo(int id)
        {
            return Ok(await _proveedorArticuloService.EliminarArticuloDeProveedor(id));
        }

        public async Task<IActionResult> AgregarDetalle(ProveedorArticulo data)
        {
            var nuevoLineaReceta = await _proveedorArticuloService.BuscarProveedorArticuloPorId(await _proveedorArticuloService.AgregarArticuloAProveedor(data));

            var ret = new List<ProveedorArticulo>();

            ret.Add(nuevoLineaReceta);
            //var proveedorInsumoDb = _mapper.Map<ProveedorInsumoModel>(nuevoLineaReceta);
            //proveedorInsumoDb.Proveedor = data.Proveedor;
            //proveedorInsumoDb.ProveedoresInsumos = data.ProveedoresInsumos;
            //proveedorInsumoDb.Insumos = data.Insumos;
            //proveedorInsumoDb.Insumo = data.Insumo;

            return PartialView("_ArticuloProveedor", nuevoLineaReceta);
        }

        public async Task<IActionResult> ModificarPrecioInsumo(int id, decimal precio)
        {
            await _proveedorInsumoService.ModificarPrecioInsumo(id, precio);
            var proveedores = await _proveedorService.GetProveedores();
            return PartialView("_ProveedorIndexTable", proveedores);
        }

        public async Task<IActionResult> ModificarPrecioArticulo(int id, decimal precio)
        {
            await _proveedorArticuloService.ModificarPrecioArticulo(id, precio);
            var proveedores = await _proveedorService.GetProveedores();
            return PartialView("_ProveedorIndexTable", proveedores);
        }

    }

}

