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
        public IMapper _mapper;
        public DashboardFabricaController(ISolicitudService solicitudService, IInsumoService insumoService,
           IOrdenProduccionService ordenProduccionService, IMapper mapper)
        {
            _solicitudService = solicitudService;
            _insumoService = insumoService;
            _ordenProduccionService = ordenProduccionService;
            _mapper = mapper;

        }

        public async Task<IActionResult> Index()
        {
            var solicitudes = await _solicitudService.GetSolicitudes();

            var CantSolHoy = solicitudes.Where(x => x.FechaCreacion >= DateTime.Now.AddDays(-1)).Count();
            var CantSolsemana = solicitudes.Where(x => x.FechaCreacion >= DateTime.Now.AddDays(-8)).Count();

            ViewBag.SolicitudesHoy = CantSolHoy;
            ViewBag.SolicitudesSemana = CantSolsemana;

            return View();
        }

    }
}
