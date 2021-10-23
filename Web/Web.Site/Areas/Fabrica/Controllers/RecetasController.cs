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
    [Authorize]
    [Area("fabrica")]
    [Route("[area]/[controller]/[action]")]
    public class RecetasController : CustomController
    {
        private ISolicitudService _solicitudService;
        private IArticuloService _articuloService;
        private IInsumoService _insumoService;
        private IOrdenProduccionService _ordenesProduccionService;
        private IRecetaService _recetaService;


        public RecetasController(ISolicitudService solicitudService,  IArticuloService articuloService , IInsumoService InsumoService
            , IOrdenProduccionService ordenProduccionService, IRecetaService recetaService)
        {
            _solicitudService = solicitudService;
            _articuloService = articuloService;
            _insumoService = InsumoService;
            _ordenesProduccionService = ordenProduccionService;
            _recetaService = recetaService;
        }
 

        // GET: RecetasController
        public async Task<ActionResult> IndexAsync()
        {
            var recetas = await _recetaService.GetRecetas();


            var recetaModelList = new RecetaModel()
            {
                Recetas = recetas
            }; 

            return View(recetaModelList);
        }

        // GET: RecetasController/Details/5
        public ActionResult Details(int id)
        {
            
            return View();
        }

        // GET: RecetasController/Create
        public async Task<IActionResult> CrearEditarReceta(int IdReceta)
        {
            RecetaModel recetamodel;
            if (IdReceta != 0)
            {
                var receta = await _recetaService.BuscarPorId(IdReceta);
                var detalle = await _recetaService.GetRecetaDetalles(IdReceta);
                recetamodel = new RecetaModel(receta, detalle);
            }
            else
            {
                recetamodel = new RecetaModel();
            }

            var articulos= await _articuloService.GetArticulos();
            var insumos = await _insumoService.GetInsumos();
            var ordenes = await _ordenesProduccionService.GetEtapasOrden();

            ViewBag.Articulos = articulos.Select(x => new SelectListItem() { Text = $"{x.Nombre}", Value = $"{x.Id}" }).GroupBy(p => new { p.Text }).Select(g => g.First()).ToList(); 
            ViewBag.Insumos = insumos.Select(x => new SelectListItem() { Text = $"{x.Nombre}", Value = $"{x.Id}" }).GroupBy(p => new { p.Text }).Select(g => g.First()).ToList(); 
            ViewBag.EtapasOrden =  ordenes.Select(x => new SelectListItem() { Text = $"{x.Descripcion}", Value = $"{x.Orden}" }).GroupBy(p => new { p.Text }).Select(g => g.First()).ToList();
          
            return View(recetamodel);
        }

        // POST: RecetasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearEditarReceta(RecetaModel collection)
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

                    return View(collection);
                }

                return RedirectToAction(nameof(IndexAsync));

            }
            catch(Exception ex)
            {
             MensajeError(ex.Message);
             return RedirectToAction(nameof(CrearEditarReceta));
            }
        }

        // GET: RecetasController/Edit/5
        [HttpPost]
        public ActionResult Desactivar(int id)
        {
            User.GetUserIdSucursal();

            return RedirectToAction(nameof(IndexAsync));
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

        // GET: RecetasController/Delete/5
        [HttpPost]
        public ActionResult Eliminar(int id)
        {
            var i = id;
            return View();
        }

        // POST: RecetasController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(IndexAsync));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
        public IActionResult AgregarDetalle(RecetaDetalle data)
        {
                return PartialView("_RecetaDetalle", data);
        }
    }
}
