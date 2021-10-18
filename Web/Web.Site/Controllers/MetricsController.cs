using Microsoft.AspNetCore.Mvc;

namespace LightFoot.Web.Site.Controllers
{
    public class MetricsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}