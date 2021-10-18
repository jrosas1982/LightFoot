using Microsoft.AspNetCore.Mvc;

namespace LightFoot.Web.Site.Controllers
{
    public class MailboxController : Controller
    {
        public IActionResult Inbox()
        {
            return View();
        }

        public IActionResult EmailView()
        {
            return View();
        }

        public IActionResult ComposeEmail()
        {
            return View();
        }

        public IActionResult EmailTemplates()
        {
            return View();
        }

        public IActionResult BasicActionEmail()
        {
            return View();
        }

        public IActionResult AlertEmail()
        {
            return View();
        }

        public IActionResult BillingEmail()
        {
            return View();
        }
    }
}