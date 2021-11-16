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
    [Route("[area]/[controller]/[action]")]
    public class VentaController : CustomController
    {
        public VentaController()
        {

        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
