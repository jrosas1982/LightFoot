using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Aplicacion.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Site.Helpers;
using Web.Site.Areas.Fabrica;
using Core.Aplicacion.Interfaces;
using AutoMapper;
using Core.Dominio.AggregatesModel;

namespace Web.Site.Areas
{
    [Area("fabrica")]
    [Authorize(Policy = Policies.IsSupervisor)]
    [Route("[area]/[controller]/[action]")]
    public class DashboardFabricaController : CustomController
    {

        public ISolicitudService _solicitudService;
        public IInsumoService _insumoService;
        public IOrdenProduccionService _ordenProduccionService;
        public IDashboardFabricaService _dashboardFabricaService;
        public IMapper _mapper;
        public DashboardFabricaController(ISolicitudService solicitudService, IInsumoService insumoService,
           IOrdenProduccionService ordenProduccionService, IMapper mapper , IDashboardFabricaService dashboardFabricaService)
        {
            _solicitudService = solicitudService;
            _insumoService = insumoService;
            _ordenProduccionService = ordenProduccionService;
            _dashboardFabricaService = dashboardFabricaService;
            _mapper = mapper;

        }

        public async Task<IActionResult> Index()
        {

            DashboardFabricaModel dashboard = new DashboardFabricaModel()
            {
                Solicitudes = await _dashboardFabricaService.GetSolicitudes(),
                Ordenes = await _dashboardFabricaService.GetOrdenes(),            
                InsumosBajoStock = await _dashboardFabricaService.GetInsumosBajoStock(5)
            //    UltimosMovimientos = await _dashboardSucursalService.GetUltimosMovimientos(5),
            //    UltimasVentas = await _dashboardSucursalService.GetUltimasVentas(5)
            };

            var solicitudes = await _dashboardFabricaService.GetSolicitudes();
            ViewBag.SolicitudesHoy = solicitudes.Where(x => x.FechaCreacion >= DateTime.Now.AddDays(-1)).Count();
            ViewBag.SolicitudesSemana = solicitudes.Where(x => x.FechaCreacion >= DateTime.Now.AddDays(-7)).Count();

            var ordenes = await _dashboardFabricaService.GetOrdenes();

            // buscar el enum
            ViewBag.cantTerminadasHoy = ordenes.Where(x => x.IdEtapaOrdenProduccion == (int)EstadoOrdenProduccion.Finalizada  && x.FechaModificacion >= DateTime.Now.AddDays(-1)).Count();
            ViewBag.cantTerminadasSemana = ordenes.Where(x => x.IdEtapaOrdenProduccion == (int)EstadoOrdenProduccion.Finalizada && x.FechaModificacion >= DateTime.Now.AddDays(-7)).Count();

            // buscar el enum
            ViewBag.cantEnviadasHoy = ordenes.Where(x => x.IdEtapaOrdenProduccion == 12 &&  x.FechaModificacion >= DateTime.Now.AddDays(-1) ).Count();
            ViewBag.cantEnviadasSemana = ordenes.Where(x => x.IdEtapaOrdenProduccion == 12 &&  x.FechaModificacion >= DateTime.Now.AddDays(-7) ).Count();


            return View(dashboard);
        }

    }
}
