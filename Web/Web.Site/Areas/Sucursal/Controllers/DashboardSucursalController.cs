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
            var sucursal = await _dashboardSucursalService.GetSucursal();

            var stockbajo = await _dashboardSucursalService.GetArticulosBajoStock();

            DashboardSucursalModel dashboard = new DashboardSucursalModel()
            {
                Sucursal = sucursal,
                StockBajo = stockbajo,
                MasVendidos = null,
                Movimientos = null,
                UltimasVentas = null
            };

            return View(dashboard);
        }

    }
}
