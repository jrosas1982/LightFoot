using Microsoft.AspNetCore.Mvc;

namespace LightFoot.Web.Site.Controllers
{
    public class LayoutsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult OffCanvas()
        {
            return View();
        }
    }
}