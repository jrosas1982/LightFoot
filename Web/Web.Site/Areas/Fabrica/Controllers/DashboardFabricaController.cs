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
           IOrdenProduccionService ordenProduccionService, IMapper mapper, IDashboardFabricaService dashboardFabricaService)
        {
            _solicitudService = solicitudService;
            _insumoService = insumoService;
            _ordenProduccionService = ordenProduccionService;
            _dashboardFabricaService = dashboardFabricaService;
            _mapper = mapper;

        }

        public async Task<IActionResult> Index()
        {
            var avanceProduccion = await _dashboardFabricaService.GetAvanceProduccion(DateTime.Now, TimeSpan.FromDays(7));

            DashboardFabricaModel dashboard = new DashboardFabricaModel()
            {
                DashbardFabricaInfoGeneralModel = new DashbardFabricaInfoGeneralModel()
                {
                    SolicitudesRecibidasFecha = await _dashboardFabricaService.GetSolicitudesRecibidas(DateTime.Now),
                    SolicitudesRecibidasPlazo = await _dashboardFabricaService.GetSolicitudesRecibidas(DateTime.Now, TimeSpan.FromDays(7)),
                    OrdenesProduccionFinalizadasFecha = await _dashboardFabricaService.GetOrdenesProduccionFinalizadas(DateTime.Now),
                    OrdenesProduccionFinalizadasPlazo = await _dashboardFabricaService.GetOrdenesProduccionFinalizadas(DateTime.Now, TimeSpan.FromDays(7)),
                    OrdenesProduccionEnExpedicionFecha = await _dashboardFabricaService.GetOrdenesProduccionCanceladas(DateTime.Now),
                    OrdenesProduccionEnExpedicionPlazo = await _dashboardFabricaService.GetOrdenesProduccionCanceladas(DateTime.Now, TimeSpan.FromDays(7))
                },
                TopSucursalesSolicitudes = await _dashboardFabricaService.GetTopSucursalesSolicitudes(5),
                //AvanceProduccion = avanceProduccion,
                InsumosBajoStock = await _dashboardFabricaService.GetInsumosBajoStock(5),
                Etapas = avanceProduccion.Select(x => x.Item1.Descripcion).ToArray(),
                Cantidad = avanceProduccion.Select(x => x.Item2).ToArray()
            };

            return View(dashboard);
        }

        public async Task<IActionResult> ActualizarInfoGeneral()
        {
            var dashbardFabricaInfoGeneralModel = new DashbardFabricaInfoGeneralModel()
            {
                SolicitudesRecibidasFecha = await _dashboardFabricaService.GetSolicitudesRecibidas(DateTime.Now),
                SolicitudesRecibidasPlazo = await _dashboardFabricaService.GetSolicitudesRecibidas(DateTime.Now, TimeSpan.FromDays(7)),
                OrdenesProduccionFinalizadasFecha = await _dashboardFabricaService.GetOrdenesProduccionFinalizadas(DateTime.Now),
                OrdenesProduccionFinalizadasPlazo = await _dashboardFabricaService.GetOrdenesProduccionFinalizadas(DateTime.Now, TimeSpan.FromDays(7)),
                OrdenesProduccionEnExpedicionFecha = await _dashboardFabricaService.GetOrdenesProduccionCanceladas(DateTime.Now),
                OrdenesProduccionEnExpedicionPlazo = await _dashboardFabricaService.GetOrdenesProduccionCanceladas(DateTime.Now, TimeSpan.FromDays(7))
            };
            return PartialView("_InfoGeneralSolicitudes", dashbardFabricaInfoGeneralModel);
        }

        public async Task<IActionResult> ActualizarTopSucursalesSolicitudes()
        {
            var topSucursalesSolicitudes = await _dashboardFabricaService.GetTopSucursalesSolicitudes(5);
            return PartialView("_RankingSucursalesTable", topSucursalesSolicitudes);
        }

        public async Task<IActionResult> ActualizarInsumosBajoStock()
        {
            var insumosBajoStock = await _dashboardFabricaService.GetInsumosBajoStock(5);
            return PartialView("_InsumoStockTable", insumosBajoStock);
        }

        public async Task<IActionResult> ActualizarGrafico()
        {
            var avanceProduccion = await _dashboardFabricaService.GetAvanceProduccion(DateTime.Now, TimeSpan.FromDays(7));
            var etapas = avanceProduccion.Select(x => x.Item1.Descripcion).ToArray();
            var cantidad = avanceProduccion.Select(x => x.Item2).ToArray();

            ViewBag.Etapas = etapas;
            ViewBag.Cantidad = cantidad;
            return PartialView("_GraficoEtapas", null);
        }


    }
}