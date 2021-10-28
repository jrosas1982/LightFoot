using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Site.Helpers;

namespace Web.Site.Areas.Fabrica.Controllers
{
    [Area("Fabrica")]
    [Route("[area]/[controller]/[action]")]
    public class DashboardController : CustomController
    {

        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
