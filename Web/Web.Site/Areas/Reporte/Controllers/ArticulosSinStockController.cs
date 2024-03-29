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
    public class ArticulosSinStockController : CustomController
    {
        public IArticulosSinStockService _articulosSinStockService;

        public ArticulosSinStockController(IArticulosSinStockService articulosSinStockService)
        {
            _articulosSinStockService = articulosSinStockService;
        }

        public async Task<IActionResult> Index()
        {
            var sinStock = await _articulosSinStockService.GetArticulosSinStock();
 
            return View(sinStock);
        }

    }
}
