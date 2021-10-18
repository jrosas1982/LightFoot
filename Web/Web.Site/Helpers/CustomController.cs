using Microsoft.AspNetCore.Mvc;

namespace Web.Site.Helpers
{
    public abstract class CustomController : Controller
    {
        public void MensajeError(string mensaje)
        {
            //ViewBag.JsFunction = $"MensajeError('Ocurrio un error', '{mensaje}')";
            var func = $"MensajeError('Ocurrio un error', '{mensaje}')";
            TempData["JsFunction"] = func;
        }
    }
}
