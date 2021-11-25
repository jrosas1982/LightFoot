﻿using System.Linq;
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

namespace Web.Site.Areas
{
    [Area("reporte")]
    //[Authorize(Policy = Policies.IsSupervisor)]
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
            PeriodoModel pediodo = new PeriodoModel()
            {
                Año=2021,
                Mes=5
            };

            var ranking = await _reporteriaService.GetRankingVentasPorSucursal(pediodo);
            var rankingList = ranking.ToList();

            return View(rankingList);
        }

    }
}