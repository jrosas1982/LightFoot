using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Aplicacion.Interfaces;
using Core.Dominio.AggregatesModel;
using Microsoft.AspNetCore.Mvc;
using Web.Site.Helpers;

namespace Web.Site.Areas
{
    [Area("fabrica")]
    [Route("[area]/[controller]/[action]")]
    public class OrdenProduccionController : CustomController
    {
        private IOrdenProduccionService _ordenProduccionService;
        private ISucursalService _sucursalService;
        private IArticuloService _articuloService;

        public OrdenProduccionController(IOrdenProduccionService ordenProduccionService, ISucursalService sucursalService, IArticuloService articuloService)
        {
            _ordenProduccionService = ordenProduccionService;
            _sucursalService = sucursalService;
            _articuloService = articuloService;
        }

        public async Task<IActionResult> FiltrarOrdenes(OrdenProduccionFilter filter)
        {
            IEnumerable<OrdenProduccion> ordenesList = new List<OrdenProduccion>();

            //if (filter == null)
            //    ordenesList = await _ordenProduccionService.GetOrdenes();

            ordenesList = await _ordenProduccionService.GetOrdenes();

            if (filter.Articulo != null)
                ordenesList = ordenesList.Where(x => x.Articulo.Nombre.ToLower().Contains(filter.Articulo.ToLower())
                                                  || x.Articulo.CodigoArticulo.ToLower().Contains(filter.Articulo.ToLower())
                                                  || x.Articulo.Color.ToLower().Contains(filter.Articulo.ToLower())
                                                  || x.Articulo.TalleArticulo.ToLower().Contains(filter.Articulo.ToLower()));
            if (filter.EstadosOrden != null)
                ordenesList = ordenesList.Where(x => filter.EstadosOrden.Contains(x.EstadoOrdenProduccion));
            if (filter.IdEtapasOrden != null)
                ordenesList = ordenesList.Where(x => filter.IdEtapasOrden.Contains(x.EtapaOrdenProduccion.Id));
            if (filter.EstadosEtapa != null)
                ordenesList = ordenesList.Where(x => filter.EstadosEtapa.Contains(x.EstadoEtapaOrdenProduccion));
            ordenesList = ordenesList.Where(x => x.FechaCreacion > filter.FechaDesde && x.FechaCreacion < filter.FechaHasta);

            return PartialView("_IndexTable", ordenesList);
        }

        private async Task RellenarViewBags(int idOrdenProduccion)
        {
            ViewBag.OrdenProduccionEventos = await _ordenProduccionService.GetOrdenEventos(idOrdenProduccion);
            ViewBag.EtapasOrdenProduccion = await _ordenProduccionService.GetEtapasOrden();
            ViewBag.PorcentajeCompletado = await _ordenProduccionService.GetProgreso(idOrdenProduccion);
            //ViewBag.EstadosOrdenProduccion = await _ordenProduccionService.GetEstadosOrden();
            //ViewBag.EstadosEtapaOrdenProduccion = await _ordenProduccionService.GetEstadoEtapasOrden();
        }

        public async Task<IActionResult> Index()
        {
            var ordenesList = await _ordenProduccionService.GetOrdenes();

            ViewBag.EstadoOrdenProducciones = await _ordenProduccionService.GetEstadosOrden();
            ViewBag.EtapaOrdenProducciones = await _ordenProduccionService.GetEtapasOrden();
            ViewBag.EstadoEtapaOrdenProducciones = await _ordenProduccionService.GetEstadoEtapasOrden();
            ViewBag.FechaDesde = DateTime.Today - TimeSpan.FromDays(30);
            ViewBag.FechaHasta = DateTime.Today;
            ViewBag.TypeaheadCodArticulo = ordenesList.Select(x => x.Articulo.CodigoArticulo).Distinct();
            ViewBag.TypeaheadArticulo = ordenesList.Select(x => x.Articulo.Nombre).Distinct();
            ViewBag.TypeaheadColor = ordenesList.Select(x => x.Articulo.Color).Distinct();
            ViewBag.TypeaheadTalle = ordenesList.Select(x => x.Articulo.TalleArticulo).Distinct();

            return View(ordenesList);
        }

        public async Task<IActionResult> DetallesOrdenProduccion(int idOrdenProduccion)
        {
            var orden = await _ordenProduccionService.BuscarPorId(idOrdenProduccion);

            await RellenarViewBags(idOrdenProduccion);

            return View(orden);
        }

        public async Task<IActionResult> IniciarEtapa(int idOrdenProduccion)
        {
            try
            {
                await _ordenProduccionService.IniciarEtapa(idOrdenProduccion);

                var orden = await _ordenProduccionService.BuscarPorId(idOrdenProduccion);

                await RellenarViewBags(idOrdenProduccion);

                return PartialView("_DetallesOrdenProduccionPartial", orden);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IActionResult> PausarEtapa(int idOrdenProduccion)
        {
            try
            {
                await _ordenProduccionService.PausarEtapa(idOrdenProduccion, "");

                var orden = await _ordenProduccionService.BuscarPorId(idOrdenProduccion);

                await RellenarViewBags(idOrdenProduccion);

                return PartialView("_DetallesOrdenProduccionPartial", orden);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IActionResult> FinalizarEtapa(int idOrdenProduccion)
        {
            try
            {
                await _ordenProduccionService.FinalizarEtapa(idOrdenProduccion);

                var orden = await _ordenProduccionService.BuscarPorId(idOrdenProduccion);

                await RellenarViewBags(idOrdenProduccion);

                return PartialView("_DetallesOrdenProduccionPartial", orden);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IActionResult> ReanudarEtapa(int idOrdenProduccion)
        {
            try
            {
                await _ordenProduccionService.ReanudarEtapa(idOrdenProduccion);

                var orden = await _ordenProduccionService.BuscarPorId(idOrdenProduccion);

                await RellenarViewBags(idOrdenProduccion);

                return PartialView("_DetallesOrdenProduccionPartial", orden);
            }
            catch (Exception ex)
            {
                throw ex;
            }           
        }

        public async Task<IActionResult> RetrabajarEtapa(int idOrdenProduccion)
        {
            try
            {
                await _ordenProduccionService.RetrabajarEtapa(idOrdenProduccion, "");

                var orden = await _ordenProduccionService.BuscarPorId(idOrdenProduccion);

                await RellenarViewBags(idOrdenProduccion);

                return PartialView("_DetallesOrdenProduccionPartial", orden);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IActionResult> AvanzarSiguienteEtapa(int idOrdenProduccion)
        {
            try
            {
                await _ordenProduccionService.AvanzarSiguienteEtapa(idOrdenProduccion);

                var orden = await _ordenProduccionService.BuscarPorId(idOrdenProduccion);

                await RellenarViewBags(idOrdenProduccion);

                return PartialView("_DetallesOrdenProduccionPartial", orden);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IActionResult> FinalizarOrden(int idOrdenProduccion)
        {
            try
            {
                await _ordenProduccionService.FinalizarOrden(idOrdenProduccion);

                var orden = await _ordenProduccionService.BuscarPorId(idOrdenProduccion);

                await RellenarViewBags(idOrdenProduccion);

                return PartialView("_DetallesOrdenProduccionPartial", orden);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        
        
        public async Task<IActionResult> CancelarOrden(int idOrdenProduccion)
        {
            try
            {
                await _ordenProduccionService.CancelarOrden(idOrdenProduccion, "");

                var orden = await _ordenProduccionService.BuscarPorId(idOrdenProduccion);

                await RellenarViewBags(idOrdenProduccion);

                return PartialView("_DetallesOrdenProduccionPartial", orden);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
