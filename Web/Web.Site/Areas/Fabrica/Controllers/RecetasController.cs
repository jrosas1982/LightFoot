using AutoMapper;
using Core.Aplicacion.Interfaces;
using Core.Dominio.AggregatesModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Site.Areas.Fabrica.Models;
using Web.Site.Helpers;

namespace Web.Site.Areas.Fabrica.Controllers
{
    //[Authorize]
    [Area("fabrica")]
    [Route("[area]/[controller]/[action]")]
    public class RecetasController : CustomController
    {
        private ISolicitudService _solicitudService;
        private IArticuloService _articuloService;
        private IInsumoService _insumoService;
        private IOrdenProduccionService _ordenesProduccionService;
        private IRecetaService _recetaService;
        private IRecetaDetalleService _recetaDetalleService;
        private IMapper _mapper ;


        public RecetasController(ISolicitudService solicitudService,  IArticuloService articuloService , IInsumoService InsumoService
            , IOrdenProduccionService ordenProduccionService, IRecetaService recetaService, IRecetaDetalleService recetaDetalleService, IMapper mapper)
        {
            _solicitudService = solicitudService;
            _articuloService = articuloService;
            _insumoService = InsumoService;
            _ordenesProduccionService = ordenProduccionService;
            _recetaService = recetaService;
            _recetaDetalleService = recetaDetalleService;
            _mapper = mapper;
        }
 
        // GET: RecetasController
        public async Task<IActionResult> IndexAsync()
        { 
            var recetas = await _recetaService.GetRecetas();
            var insumos = await _insumoService.GetInsumos();
            var ordenes = await _ordenesProduccionService.GetEtapasOrden();

            var modeloReceta = _mapper.Map<IEnumerable<RecetaModel>>(recetas);
            foreach (var receta in modeloReceta)
            {
                foreach (var detalle in receta.RecetaDetalles)
                {
                    detalle.NombreEtapaOrdenProduccion = ordenes.Where(x => x.Orden == detalle.IdEtapaOrdenProduccion).Select(d => d.Descripcion).FirstOrDefault();
                    detalle.NombreInsumo = insumos.Where(x => x.Id == detalle.IdInsumo).Select(d => d.Nombre).FirstOrDefault();
                }
            }

            return View(modeloReceta);
        }

        // GET: RecetasController/Details/5
        public ActionResult Details(int id)
        {
            
            return View();
        }

        // GET: RecetasController/Create
        public async Task<IActionResult> CrearEditarReceta(int IdReceta)
        {
            RecetaModel recetamodel = new RecetaModel();
            var articulos = await _articuloService.GetArticulos();
            var insumos = await _insumoService.GetInsumos();
            var ordenes = await _ordenesProduccionService.GetEtapasOrden();

            if (IdReceta != 0)
            {
                var receta = await _recetaService.BuscarPorId(IdReceta);
                recetamodel = _mapper.Map<RecetaModel>(receta);
                foreach (var item in recetamodel.RecetaDetalles)
                {
                    item.NombreEtapaOrdenProduccion = ordenes.Where(x => x.Orden == item.IdEtapaOrdenProduccion).Select(d => d.Descripcion).FirstOrDefault();
                    item.NombreInsumo = insumos.Where(x => x.Id == item.IdInsumo).Select(d => d.Nombre).FirstOrDefault();
                }
            }
            ViewBag.Articulos = articulos.Select(x => new SelectListItem() { Text = $"{x.Nombre}", Value = $"{x.Id}" }).GroupBy(p => new { p.Text }).Select(g => g.First()).ToList(); 
            ViewBag.Insumos = insumos.Select(x => new SelectListItem() { Text = $"{x.Nombre}", Value = $"{x.Id}" }).GroupBy(p => new { p.Text }).Select(g => g.First()).ToList(); 
            ViewBag.EtapasOrden =  ordenes.Select(x => new SelectListItem() { Text = $"{x.Descripcion}", Value = $"{x.Orden}" }).GroupBy(p => new { p.Text }).Select(g => g.First()).ToList();
            
            return View(recetamodel);
        }

        // POST: RecetasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearEditarReceta(RecetaModel recetaModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var articulos = await _articuloService.GetArticulos();
                    var insumos = await _insumoService.GetInsumos();
                    var ordenes = await _ordenesProduccionService.GetEtapasOrden();

                    ViewBag.Articulos = articulos.Select(x => new SelectListItem() { Text = $"{x.Nombre}", Value = $"{x.Id}" }).GroupBy(p => new { p.Text }).Select(g => g.First()).ToList();
                    ViewBag.Insumos = insumos.Select(x => new SelectListItem() { Text = $"{x.Nombre}", Value = $"{x.Id}" }).GroupBy(p => new { p.Text }).Select(g => g.First()).ToList();
                    ViewBag.EtapasOrden = ordenes.Select(x => new SelectListItem() { Text = $"{x.Descripcion}", Value = $"{x.Orden}" }).GroupBy(p => new { p.Text }).Select(g => g.First()).ToList();

                    return View(recetaModel);
                }
                else {
                    var recetaNueva = _mapper.Map<Receta>(recetaModel);
                    await _recetaService.CrearReceta(recetaNueva);
                    return RedirectToAction("Index", "Recetas",new { @area = "fabrica" });
                }
            }
            catch(Exception ex)
            {
             MensajeError(ex.Message);
                return RedirectToAction("CrearEditarReceta", "Recetas", new { @area = "fabrica" });
            }
        }

        // GET: RecetasController/Edit/5
        [HttpPost]
        public ActionResult ActivarDesactivar(int id)
        {
            User.GetUserIdSucursal();
            return Ok(_recetaService.ActivarDesactivarReceta(id).Result);
        }

        // POST: RecetasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }
        /// <summary>
        /// Recibe el Ide de receta para eliminar
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: RecetasController/Delete/5
        [HttpPost]
        public ActionResult Eliminar(int id)
        {
            var i = _recetaService.EliminarReceta(id);
            return Ok(_recetaService.EliminarReceta(id));
        }

        [HttpPost]
        public IActionResult EliminarDetalle(int id)
        {
            return Ok(_recetaDetalleService.EliminarInsumoAReceta(id).Result);
        }

        public IActionResult AgregarDetalle(RecetaDetalleModel data)
        {
            if (data.IdReceta != 0 ) {
                var agregarDetalle = _mapper.Map<RecetaDetalle>(data);
                var nuevoLineaReceta = _recetaDetalleService.BuscarInsumoDeRecetaPorId(_recetaDetalleService.AgregarInsumoAReceta(agregarDetalle).Result);
                var recetaDetalleModelo = _mapper.Map<RecetaDetalleModel>(nuevoLineaReceta);
                recetaDetalleModelo.NombreInsumo = data.NombreInsumo;
                recetaDetalleModelo.NombreEtapaOrdenProduccion = data.NombreEtapaOrdenProduccion;

                return PartialView("_RecetaDetalle", recetaDetalleModelo);
            }else
            return PartialView("_RecetaDetalle", data);

        }

        //TODO: Idea para refactor 
        private async Task<RecetaDetalleModel> AgregarDetalleInsumoYOrdenesAsync(RecetaDetalleModel recetaDetalleModel)     
        {
            var insumos = await _insumoService.GetInsumos();
            var ordenes = await _ordenesProduccionService.GetEtapasOrden();

            recetaDetalleModel.NombreEtapaOrdenProduccion = ordenes.Where(x => x.Orden == recetaDetalleModel.IdEtapaOrdenProduccion).Select(d => d.Descripcion).FirstOrDefault();
            recetaDetalleModel.NombreInsumo = insumos.Where(x => x.Id == recetaDetalleModel.IdInsumo).Select(d => d.Nombre).FirstOrDefault();
         
            return recetaDetalleModel;
        }
    }
}
