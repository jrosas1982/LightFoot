using Core.Aplicacion.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Site.Helpers;

namespace Web.Site.Areas.Reporte.Controllers
{
    [Authorize]
    [Area("reporte")]
    [Route("[area]/[controller]/[action]")]
    public class CajaController : CustomController
    {
        public IAdministracionDeCajaService _administracionDeCajaService;
        public CajaController(IAdministracionDeCajaService administracionDeCajaService)
        {
            _administracionDeCajaService = administracionDeCajaService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var movimientos = await _administracionDeCajaService.GetMovimientos();
            ViewBag.FechaDesde = DateTime.Today - TimeSpan.FromDays(1);
            ViewBag.FechaHasta = DateTime.Today;
            return View(movimientos);
        }

        public async Task<IActionResult> Filtrar(DateTime FechaDesde , DateTime FechaHasta)
        {
            var movimientos = await _administracionDeCajaService.GetMovimientos();
            movimientos = movimientos.Where(x => x.FechaCreacion.Date >= FechaDesde && x.FechaCreacion <= FechaHasta.AddDays(1));

            return PartialView("_CajaIndexTable", movimientos);
        }

    }
}
