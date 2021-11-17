using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.Site.Helpers
{
    [Authorize]
    public abstract class CustomController : Controller
    {
        public void MensajeError(string mensaje)
        {
            //ViewBag.JsFunction = $"MensajeError('Ocurrio un error', '{mensaje}')";
            var func = $"MensajeError('Ocurrio un error', '{mensaje}')";
            TempData["JsFunction"] = func;

            //TempData["JsFunction"] = "";
        }

        //public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        //{
        //    TempData["JsFunction"] = "";
        //    await base.OnActionExecutionAsync(context, next);

        //}

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            var cultureInfo = CultureInfo.GetCultureInfo("es-AR");
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }

        protected override void Dispose(bool disposing)
        {
            ViewBag.JsFunction = $"";
            TempData["JsFunction"] = "";
            base.Dispose(disposing);
        }
    }
}
