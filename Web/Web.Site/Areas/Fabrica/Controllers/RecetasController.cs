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
using Web.Site.Helpers;
using Web.Site.Areas.Fabrica;

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
        private IRecetaDetalleService _recetaDetalleService;
        private IMapper _mapper ;


        public RecetasController(ISolicitudService solicitudService,  IArticuloService articuloService , IInsumoService InsumoService
            , IOrdenProduccionService ordenProduccionService, IRecetaService recetaService, IRecetaDetalleService recetaDetalleService, 
            IMapper mapper)
        {
            _solicitudService = solicitudService;
            _articuloService = articuloService;
            _insumoService = InsumoService;
            _ordenesProduccionService = ordenProduccionService;
            _recetaService = recetaService;
            _recetaDetalleService = recetaDetalleService;
            _mapper = mapper;
        }
 
        public async Task<IActionResult> IndexAsync()
        { 
            var recetasTask = _recetaService.GetRecetas();
            var insumosTask = _insumoService.GetInsumos();
            var ordenesTask = _ordenesProduccionService.GetEtapasOrden();

            await Task.WhenAll(recetasTask, insumosTask, ordenesTask);

            var recetas = recetasTask.Result;
            var ordenes = ordenesTask.Result;
            var insumos = insumosTask.Result;

            var modeloReceta = _mapper.Map<IEnumerable<RecetaModel>>(recetas);

            Parallel.ForEach(modeloReceta, receta =>
            {
                Parallel.ForEach(receta.RecetaDetalles, detalle =>
                {
                    detalle.NombreEtapaOrdenProduccion = ordenes.Where(x => x.Id == detalle.IdEtapaOrdenProduccion).Select(d => d.Descripcion).Single();
                    detalle.NombreInsumo = insumos.Where(x => x.Id == detalle.IdInsumo).Select(d => d.Nombre).Single();
                    detalle.UnidadDeMedida = insumos.Where(x => x.Id == detalle.IdInsumo).Select(d => d.Unidad).Single();
                });
            });

            //foreach (var receta in modeloReceta)
            //{
            //    foreach (var detalle in receta.RecetaDetalles)
            //    {
            //        detalle.NombreEtapaOrdenProduccion = ordenes.Where(x => x.Id == detalle.IdEtapaOrdenProduccion).Select(d => d.Descripcion).Single();
            //        detalle.NombreInsumo = insumos.Where(x => x.Id == detalle.IdInsumo).Select(d => d.Nombre).Single();
            //        detalle.UnidadDeMedida = insumos.Where(x => x.Id == detalle.IdInsumo).Select(d => d.Unidad).Single();
            //    }
            //}
            return View(modeloReceta);
        }

        /// <summary>
        /// El Id que recibe como parametro define si la receta existe o no 
        /// </summary>
        /// <param name="IdReceta"></param>
        /// <returns></returns>
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
                    recetaNueva.CreadoPor = User.GetUserIdSucursal();
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

        [HttpPost]
        public ActionResult ActivarDesactivar(int id)
        {
            // TODO: definir si debemos registrar el usuartio que activa desactiva
            // User.GetUserIdSucursal();
            return Ok(_recetaService.ActivarDesactivarReceta(id).Result);
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
            return Ok(_recetaService.EliminarReceta(id).Result);
        }

        [HttpPost]
        public async Task<IActionResult> EliminarDetalle(int id)
        {
            return Ok(await _recetaDetalleService.EliminarInsumoAReceta(id));
        }

        public async Task<IActionResult> AgregarDetalle(RecetaDetalleModel data)
        {
            if (data.IdReceta != 0 )
            {
                var agregarDetalle = _mapper.Map<RecetaDetalle>(data);
                agregarDetalle.ModificadoPor = User.GetUserIdSucursal();
                var nuevoLineaReceta = _recetaDetalleService.BuscarInsumoDeRecetaPorId(await _recetaDetalleService.AgregarInsumoAReceta(agregarDetalle));
                var recetaDetalleModelo = _mapper.Map<RecetaDetalleModel>(nuevoLineaReceta);
                recetaDetalleModelo.NombreInsumo = data.NombreInsumo;
                recetaDetalleModelo.NombreEtapaOrdenProduccion = data.NombreEtapaOrdenProduccion;

                return PartialView("_RecetaDetalle", recetaDetalleModelo);
            }
            else
                return PartialView("_RecetaDetalle", data);
        }

        //TODO: Idea para refactor 
        //private async Task<RecetaDetalleModel> AgregarDetalleInsumoYOrdenesAsync(RecetaDetalleModel recetaDetalleModel)     
        //{
        //    var insumos = await _insumoService.GetInsumos();
        //    var ordenes = await _ordenesProduccionService.GetEtapasOrden();

        //    recetaDetalleModel.NombreEtapaOrdenProduccion = ordenes.Where(x => x.Orden == recetaDetalleModel.IdEtapaOrdenProduccion).Select(d => d.Descripcion).FirstOrDefault();
        //    recetaDetalleModel.NombreInsumo = insumos.Where(x => x.Id == recetaDetalleModel.IdInsumo).Select(d => d.Nombre).FirstOrDefault();
         
        //    return recetaDetalleModel;
        //}
    }
}
