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

namespace Web.Site.Areas
{
    [Area("sucursal")]
    //[Authorize(Policy = Policies.IsSupervisor)]
    [Route("[area]/[controller]/[action]")]
    public class DashboardSucursalController : CustomController
    {
        public IDashboardSucursalService _dashboardSucursalService;

        public DashboardSucursalController(IDashboardSucursalService dashboardSucursalService)
        {
            _dashboardSucursalService = dashboardSucursalService;
        }

        public async Task<IActionResult> Index()
        {

            DashboardSucursalModel dashboard = new DashboardSucursalModel()
            {
                Sucursal = await _dashboardSucursalService.GetSucursal(),
                StockBajo = await _dashboardSucursalService.GetArticulosBajoStock(5),
                MasVendidos = await _dashboardSucursalService.GetMasVendidos(5),
                UltimosMovimientos = await _dashboardSucursalService.GetUltimosMovimientos(5),
                UltimasVentas = await _dashboardSucursalService.GetUltimasVentas(5)
            };

            return View(dashboard);
        }

    }
}
