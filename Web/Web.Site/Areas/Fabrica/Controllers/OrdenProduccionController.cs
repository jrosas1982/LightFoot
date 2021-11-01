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
                ordenesList = ordenesList.Where(x => x.Articulo.Nombre.ToLower().Contains(filter.Articulo.ToLower()));
            if (filter.EstadosOrden != null)
                ordenesList = ordenesList.Where(x => filter.EstadosOrden.Contains(x.EstadoOrdenProduccion));
            if (filter.IdEtapasOrden != null)
                ordenesList = ordenesList.Where(x => filter.IdEtapasOrden.Contains(x.EtapaOrdenProduccion.Id));
            if (filter.EstadosEtapa != null)
                ordenesList = ordenesList.Where(x => filter.EstadosEtapa.Contains(x.EstadoEtapaOrdenProduccion));

            //Filtro por fecha
            ordenesList = ordenesList.Where(x => x.FechaCreacion > filter.FechaDesde && x.FechaCreacion < filter.FechaHasta);

            return PartialView("_IndexTable", ordenesList);
        }

        public async Task<IActionResult> Index()
        {
            var ordenesList = await _ordenProduccionService.GetOrdenes();

            ViewBag.EstadoOrdenProducciones = await _ordenProduccionService.GetEstadosOrden();
            ViewBag.EtapaOrdenProducciones = await _ordenProduccionService.GetEtapasOrden();
            ViewBag.EstadoEtapaOrdenProducciones = await _ordenProduccionService.GetEstadoEtapasOrden();
            ViewBag.FechaDesde = DateTime.Today - TimeSpan.FromDays(30);
            ViewBag.FechaHasta = DateTime.Today;

            return View(ordenesList);
        }

        public async Task<IActionResult> DetallesOrdenProduccion(int idOrdenProduccion)
        {
            var ordenesList = await _ordenProduccionService.GetOrdenes();

            var model = new OrdenProduccionDetalleModel()
            {
                OrdenProduccion = ordenesList.Where(x => x.Id == idOrdenProduccion).First(),
                EstadosOrdenProduccion = await _ordenProduccionService.GetEstadosOrden(),
                EtapasOrdenProduccion = await _ordenProduccionService.GetEtapasOrden(),
                EstadosEtapaOrdenProduccion = await _ordenProduccionService.GetEstadoEtapasOrden(),
                OrdenProduccionEventos = await _ordenProduccionService.GetOrdenEventos(idOrdenProduccion),
                PorcentajeCompletado = await _ordenProduccionService.GetProgreso(idOrdenProduccion)
            };

            return View(model);
        }


        public async Task<IActionResult> IniciarEtapa(int idOrdenProduccion)
        {
            try
            {
                await _ordenProduccionService.IniciarEtapa(idOrdenProduccion);
                return RedirectToAction("DetallesOrdenProduccion", new { idOrdenProduccion = idOrdenProduccion });
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<IActionResult> PausarEtapa(int idOrdenProduccion)
        {
            try
            {
                await _ordenProduccionService.PausarEtapa(idOrdenProduccion, "");

                return RedirectToAction("DetallesOrdenProduccion", new { idOrdenProduccion = idOrdenProduccion });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IActionResult> FinalizarEtapa(int idOrdenProduccion)
        {
            try
            {
                await _ordenProduccionService.FinalizarEtapa(idOrdenProduccion);
                return RedirectToAction("DetallesOrdenProduccion", new { idOrdenProduccion = idOrdenProduccion });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IActionResult> ReanudarEtapa(int idOrdenProduccion)
        {
            await _ordenProduccionService.ReanudarEtapa(idOrdenProduccion);
            return RedirectToAction("DetallesOrdenProduccion", new { idOrdenProduccion = idOrdenProduccion });
        }

        public async Task<IActionResult> RetrabajarEtapa(int idOrdenProduccion)
        {
            await _ordenProduccionService.RetrabajarEtapa(idOrdenProduccion, "");
            return RedirectToAction("DetallesOrdenProduccion", new { idOrdenProduccion = idOrdenProduccion });
        }

        public async Task<IActionResult> AvanzarSiguienteEtapa(int idOrdenProduccion)
        {
            await _ordenProduccionService.AvanzarSiguienteEtapa(idOrdenProduccion);
            return RedirectToAction("DetallesOrdenProduccion", new { idOrdenProduccion = idOrdenProduccion });
        }

        public async Task<IActionResult> FinalizarOrden(int idOrdenProduccion)
        {
            await _ordenProduccionService.FinalizarOrden(idOrdenProduccion);
            return RedirectToAction("DetallesOrdenProduccion", new { idOrdenProduccion = idOrdenProduccion });
        }        
        
        public async Task<IActionResult> CancelarOrden(int idOrdenProduccion)
        {
            await _ordenProduccionService.CancelarOrden(idOrdenProduccion, "");
            return RedirectToAction("DetallesOrdenProduccion", new { idOrdenProduccion = idOrdenProduccion });
        }

    }
}
