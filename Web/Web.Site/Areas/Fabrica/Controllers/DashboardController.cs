using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Aplicacion.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Site.Helpers;
using Web.Site.Areas.Fabrica;

namespace Web.Site.Areas.Fabrica.Controllers
{
    [Area("Fabrica")]
    [Authorize (Policy = Policies.IsSupervisor)]
    [Route("[area]/[controller]/[action]")]
    public class DashboardController : CustomController
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }

    }
}
