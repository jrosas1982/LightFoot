using Microsoft.AspNetCore.Mvc;

namespace LightFoot.Web.Site.Controllers
{
    public class WidgetsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}