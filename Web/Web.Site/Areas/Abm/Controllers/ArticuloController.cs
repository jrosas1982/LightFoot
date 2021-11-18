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
using Core.Dominio.CoreModelHelpers;

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
            try
            {
                var nombreArticulo = cambioPrecio.ArticulosAfectados;
                var articulosList = await _articuloService.GetArticulos();

                if (!string.IsNullOrWhiteSpace(nombreArticulo))
                    articulosList = articulosList.Where(x => x.Nombre.ToLower().Contains(nombreArticulo.ToLower())
                                                          || x.ArticuloCategoria.Descripcion.ToLower().Contains(nombreArticulo.ToLower())
                                                          || x.CodigoArticulo.ToLower().Contains(nombreArticulo.ToLower())
                                                          || x.Color.ToLower().Contains(nombreArticulo.ToLower())
                                                          || x.TalleArticulo.ToLower().Contains(nombreArticulo.ToLower()));


                NuevoCambioPrecioModel nuevoCambio = new NuevoCambioPrecioModel();
                nuevoCambio.CambioPrecioMayorista = cambioPrecio.TipoPrecioAfectado.Contains("PrecioMayorista");
                var esAumento = (cambioPrecio.TipCambio == 1);
                nuevoCambio.Comentario = cambioPrecio.Comentartio;

                foreach (var item in articulosList)
                {
                    var precio = ActualizarPrecio(item, nuevoCambio.CambioPrecioMayorista, esAumento, cambioPrecio.Porcentaje);
                    nuevoCambio.Detalle.Add(new NuevoCambioPrecioDetalleModel() { IdArticulo = item.Id, NuevoPrecio = precio });
                }

                await _articuloService.CambioPrecio(nuevoCambio);
                return PartialView("_ArticuloIndexTable", articulosList);
            }
            catch (Exception ex)
            {
                var v = ex.Message;
                throw;
            }
        }

        private decimal ActualizarPrecio(decimal actual, decimal porcentaje)
        {
            return actual * porcentaje / 100;
        }   
        private decimal ActualizarPrecio(Articulo articulo, bool mayorista , bool aumento , decimal porcentaje)
        {
            if (mayorista)
            {
                return aumento ? (articulo.PrecioMayorista + articulo.PrecioMayorista * porcentaje / 100) : (articulo.PrecioMayorista - articulo.PrecioMayorista * porcentaje / 100);
            }
            else
            {
                return aumento ? (articulo.PrecioMinorista + articulo.PrecioMinorista * porcentaje / 100) : (articulo.PrecioMinorista - articulo.PrecioMinorista * porcentaje / 100);
            }
            //return actual * porcentaje / 100;
        }
    }
    
}
