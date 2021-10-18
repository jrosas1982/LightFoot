using Microsoft.AspNetCore.Mvc;

namespace LightFoot.Web.Site.Controllers
{
    public class LandingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}