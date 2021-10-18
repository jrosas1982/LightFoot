using Microsoft.AspNetCore.Mvc;

namespace LightFoot.Web.Site.Controllers
{
    public class DashboardsController : Controller
    {

        public IActionResult Dashboard_1()
        {
            return View();
        }
        //[Authorize(Roles = "admin")]
        public IActionResult Dashboard_2()
        {
            return View();
        }
        public IActionResult Dashboard_3()
        {
            return View();
        }
        // [Authorize(Roles = "Admin")]
        public IActionResult Dashboard_4()
        {
            return View();
        }
        //[Authorize(Roles = "Admin")]
        public IActionResult Dashboard_4_1()
        {
            return View();
        }
        //[Authorize(Roles = "Admin")]
        public IActionResult Dashboard_5()
        {
            return View();
        }
    }
}