using System.Linq;
using System.Threading.Tasks;
using Core.Aplicacion.Interfaces;
using Core.Dominio.AggregatesModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Site.Helpers;
using Web.Site.Areas.Abm;
using System;

namespace Web.Site.Areas.Abm.Controllers
{
    [Authorize]
    [Area("abm")]
    [Route("[area]/[controller]/[action]")]
    public class ArticuloCategoriaController : CustomController
    {
        private IArticuloCategoriaService _articuloCategoriaService;

        public ArticuloCategoriaController(IArticuloCategoriaService articuloCategoriaService)
        {
            _articuloCategoriaService = articuloCategoriaService;
        }


        public async Task<IActionResult> Index(bool error, string msj)
        {
            if (error) {
                ViewBag.CatchError = true;
                ViewBag.MensajeError = "Error inesperado con Mensaje: " + msj;
            }
            var articuloCategoriasList = await _articuloCategoriaService.GetCategorias();
            ViewBag.TypeaheadCategoria = articuloCategoriasList.Select(x => x.Descripcion).Distinct();
            return View(articuloCategoriasList.ToList());
        }

        public async Task<IActionResult> CrearEditarCategoria(int IdCategoria)
        {
            ArticuloCategoria articuloCategoria = new ArticuloCategoria();
            try
            {
                if (IdCategoria != 0) // 0 = crear
                    articuloCategoria = await _articuloCategoriaService.BuscarPorId(IdCategoria);
                // solo para test
                //var msj = "Error test";
                //ViewBag.CatchError = true;
                //ViewBag.MensajeError = $"Error al CrearEditarCategoria Error: {mjs} ";
                // solo para test
                //return Redirect("/abm/ArticuloCategoria/Index?error=true");
                return View(articuloCategoria);
            }
            catch (Exception ex)
            {
                ViewBag.CatchError = true;
                ViewBag.MensajeError = $"Error al CrearEditarCategoria Error: {ex.Message}" ;
                return View(articuloCategoria);
            }

        }

        public async Task<IActionResult> Crear(ArticuloCategoria articuloCategoria)
        {
            try
            {
                throw new NotImplementedException();
                await _articuloCategoriaService.CrearCategoria(articuloCategoria);
                return Json(new { redirectToUrl = @Url.Action("Index", "ArticuloCategoria", new { area = "abm" }) });
                //return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return Redirect($"/abm/ArticuloCategoria/Index?error=true&msj= {ex.Message}");
            }

        }

        public async Task<IActionResult> Editar(ArticuloCategoria articuloCategoria)
        {
       
            await _articuloCategoriaService.EditarCategoria(articuloCategoria);
            return Json(new { redirectToUrl = @Url.Action("Index", "ArticuloCategoria", new { area = "abm" }) });
            //return RedirectToAction("Index");
        }

        public async Task<IActionResult> Eliminar(ArticuloCategoria articuloCategoria)
        {
            var result = await _articuloCategoriaService.EliminarCategoria(articuloCategoria);
            return Ok(result);
        }    
  
        public async Task<IActionResult> FiltrarCategoriaArticulos(string NombreCategoriaArticulo)
        {
            var articuloCategorias = await _articuloCategoriaService.GetCategorias();

            if (!string.IsNullOrWhiteSpace(NombreCategoriaArticulo))
            {
                articuloCategorias = articuloCategorias.Where(x => x.Id.ToString().Equals(NombreCategoriaArticulo.ToLower())
                                                            || x.Descripcion.ToLower().Equals(NombreCategoriaArticulo.ToLower())).ToList();

                if (!articuloCategorias.Any())
                    articuloCategorias = articuloCategorias.Where(x => x.Id.ToString().Contains(NombreCategoriaArticulo.ToLower())
                                                            || x.Descripcion.ToLower().Contains(NombreCategoriaArticulo.ToLower())).ToList();
            }

            return PartialView("_ArticuloCategoriaIndexTable", articuloCategorias);
        }
    }
}
