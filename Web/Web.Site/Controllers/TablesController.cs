using Microsoft.AspNetCore.Mvc;

namespace LightFoot.Web.Site.Controllers
{
    public class TablesController : Controller
    {
        public IActionResult StaticTables()
        {
            return View();
        }

        public IActionResult DataTables()
        {
            return View();
        }

        public IActionResult FooTables()
        {
            return View();
        }

        public IActionResult jqGrid()
        {
            return View();
        }
    }
}