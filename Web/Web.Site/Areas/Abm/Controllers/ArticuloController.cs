using System.Linq;
using System.Threading.Tasks;
using Core.Aplicacion.Interfaces;
using Core.Dominio.AggregatesModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Site.Dtos;
using Web.Site.Helpers;
using Web.Site.Areas.Abm;
using System.Collections.Generic;
using Web.Site.Areas.Abm.Models;
using System;

namespace Web.Site.Areas
{
    [Authorize]
    [Area("abm")]
    [Route("[area]/[controller]/[action]")]
    public class ArticuloController : CustomController
    {
        private IArticuloService _articuloService;
        private IArticuloCategoriaService _articuloCategoriaService;

        public ArticuloController(IArticuloService articuloService, IArticuloCategoriaService articuloCategoriaService)
        {
            _articuloService = articuloService;
            _articuloCategoriaService = articuloCategoriaService;
        }

        public async Task<IActionResult> FiltrarArticulos(string nombreArticulo)
        {
            var articulosList = await _articuloService.GetArticulos();

            if (!string.IsNullOrWhiteSpace(nombreArticulo))
                articulosList = articulosList.Where(x => x.Nombre.ToLower().Contains(nombreArticulo.ToLower())
                                                      || x.ArticuloCategoria.Descripcion.ToLower().Contains(nombreArticulo.ToLower())
                                                      || x.CodigoArticulo.ToLower().Contains(nombreArticulo.ToLower())
                                                      || x.Color.ToLower().Contains(nombreArticulo.ToLower())
                                                      || x.TalleArticulo.ToLower().Contains(nombreArticulo.ToLower()));

            return PartialView("_ArticuloIndexTable", articulosList);
        }

        public void LlenarViewBagsFiltro(IEnumerable<Articulo> articulosList)
        {
            ViewBag.TypeaheadCodArticulo = articulosList.Select(x => x.CodigoArticulo).Distinct();
            ViewBag.TypeaheadCategoria = articulosList.Select(x => x.ArticuloCategoria.Descripcion).Distinct();
            ViewBag.TypeaheadArticulo = articulosList.Select(x => x.Nombre).Distinct();
            ViewBag.TypeaheadColor = articulosList.Select(x => x.Color).Distinct();
            ViewBag.TypeaheadTalle = articulosList.Select(x => x.TalleArticulo).Distinct();
        }

        public async Task<IActionResult> Index()
        {
            var articulosList = await _articuloService.GetArticulos();
            LlenarViewBagsFiltro(articulosList);
            return View(articulosList);
        }

        public async Task<IActionResult> CrearEditarArticulo(int IdArticulo)
        {
            Articulo articulo;
            var articuloCategoriaListt = await _articuloCategoriaService.GetCategorias();
            var articuloEstadosList = await _articuloService.GetArticuloEstados();

            if (IdArticulo != 0) // 0 = crear
                articulo = await _articuloService.BuscarPorId(IdArticulo);

            else
                articulo = new Articulo();

            ArticuloModel articuloModel = new ArticuloModel()
            {
                Articulo = articulo,
                ArticuloCategorias = articuloCategoriaListt.Select(x => new DesplegableModel() { Id = x.Id, Descripcion = $"{x.Id} - {x.Descripcion}" }),
                ArticuloEstados = articuloEstadosList.Select(x => new DesplegableModel() { Id = (int)x, Descripcion = x.ToString() }),
            };
            return View(articuloModel);
        }

        public async Task<IActionResult> Crear(Articulo articulo)
        {
            await _articuloService.CrearArticulo(articulo);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Editar(Articulo articulo)
        {
            await _articuloService.EditarArticulo(articulo);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Eliminar(Articulo articulo)
        {
            var result = await _articuloService.EliminarArticulo(articulo);
            return Ok(result);
        }

        public async Task<IActionResult> CambiarPrecio(CambioPrecioModel cambioPrecio)
        {
            var nombreArticulo = cambioPrecio.ArticulosAfectados;
            var articulosList = await _articuloService.GetArticulos();

            if (!string.IsNullOrWhiteSpace(nombreArticulo))
                articulosList = articulosList.Where(x => x.Nombre.ToLower().Contains(nombreArticulo.ToLower())
                                                      || x.ArticuloCategoria.Descripcion.ToLower().Contains(nombreArticulo.ToLower())
                                                      || x.CodigoArticulo.ToLower().Contains(nombreArticulo.ToLower())
                                                      || x.Color.ToLower().Contains(nombreArticulo.ToLower())
                                                      || x.TalleArticulo.ToLower().Contains(nombreArticulo.ToLower()));
            
            // hacer refactor código feo solo para test
            foreach (var item in articulosList)
            {
                //usar reflection?
                if ("PrecioMayorista" == cambioPrecio.TipoPrecioAfectado)
                {
                    if (cambioPrecio.TipCambio == -1)
                    {
                        item.PrecioMayorista = item.PrecioMayorista - ActualizarPrecio(item.PrecioMayorista, cambioPrecio.Porcentaje);
                    }
                    else
                    {
                        item.PrecioMayorista = item.PrecioMayorista + ActualizarPrecio(item.PrecioMayorista, cambioPrecio.Porcentaje);
                    }
                }
                else {
                    if (cambioPrecio.TipCambio == -1)
                    {
                        item.PrecioMinorista = item.PrecioMinorista - ActualizarPrecio(item.PrecioMinorista, cambioPrecio.Porcentaje);
                    }
                    else
                    {
                        item.PrecioMinorista = item.PrecioMinorista + ActualizarPrecio(item.PrecioMinorista, cambioPrecio.Porcentaje);
                    }
                }
            }

            await _articuloService.CambioPrecio(articulosList);
            return PartialView("_ArticuloIndexTable", articulosList);
        }

        private decimal ActualizarPrecio(decimal actual, decimal porcentaje)
        {
            return actual * porcentaje / 100;
        }
    }
    
}
