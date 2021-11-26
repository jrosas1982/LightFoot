using System.Linq;
using System.Threading.Tasks;
using Core.Aplicacion.FIlters;
using Core.Aplicacion.Interfaces;
using Core.Dominio.AggregatesModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Site.Dtos;
using Web.Site.Helpers;
using System;
using Core.Dominio.CoreModelHelpers;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Core.Aplicacion.Auth;

namespace Web.Site.Areas
{
    [Area("reporte")]
    [Authorize]
    [Route("[area]/[controller]/[action]")]
    public class VentasPorSucursalController : CustomController
    {
        public IReporteriaService _reporteriaService;

        public VentasPorSucursalController(IReporteriaService reporteriaService)
        {
            _reporteriaService = reporteriaService;
        }

        public async Task<IActionResult> Index()
        {
            PeriodoModel pediodo = new PeriodoModel();

            var ranking = await _reporteriaService.GetRankingVentasPorSucursal(pediodo);

            return View(ranking);
        }

    }
}