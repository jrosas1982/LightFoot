using Core.Aplicacion.Interfaces;
using Core.Dominio.AggregatesModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Web.Site.Helpers;

namespace Web.Site.Areas.Abm.Controllers
{
    //[Authorize]
    [Area("abm")]
    [Route("[area]/[controller]/[action]")]
    public class FabricaParametrosController : CustomController
    {
        public IParametroService _parametroService { get; }

        public FabricaParametrosController(IParametroService parametroService)
        {
            _parametroService = parametroService;
        }
        public async Task<IActionResult> IndexAsync() {

            IEnumerable<FabricaParametro> parame = await _parametroService.GetParametros();

            return View(parame);
        }
        public IActionResult EliminarParametro(int IdParametro) {


            var a = IdParametro;

            return View();
        } 
        public async Task<IActionResult> EditarParametro(FabricaParametro fabricaParametro) {

            if (_parametroService.EditarParamtro(fabricaParametro).Result) {
                IEnumerable<FabricaParametro> parame = await _parametroService.GetParametros();
                return PartialView("_listadoParametro", parame);
            }
            else {
                return RedirectToAction("Index");
            }
        }  

        public async Task<IActionResult> CrearEditarParametro() {

            //IEnumerable<FabricaParametro> parame = await _parametroService.GetParametros();
            //var b = parame.GroupBy(x => x.Parametro).ToList();
            //var c = ToListSelectListItem<Parametro>();
            ViewBag.ListParametros = ToListSelectListItem<Parametro>();
            return View();
        }

        [HttpPost]
        public IActionResult CrearEditarParametro(FabricaParametro fabricaParametro)
        {
            ViewBag.ListParametros = ToListSelectListItem<Parametro>();

            try
            {
                if (!_parametroService.CrearParamtro(fabricaParametro).Result) { 
                    return View(fabricaParametro); }
                else
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                return View(fabricaParametro);
            }
    
        }


        public List<SelectListItem> ToListSelectListItem<T>()
        {
            var t = typeof(T);

            if (!t.IsEnum) { throw new ApplicationException("Tipo debe ser enum"); }

            var members = t.GetFields(BindingFlags.Public | BindingFlags.Static);

            var result = new List<SelectListItem>();

            foreach (var member in members)
            {
                var attributeDescription = member.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute),
                    false);
                var descripcion = member.Name;

                if (attributeDescription.Any())
                {
                    descripcion = ((System.ComponentModel.DescriptionAttribute)attributeDescription[0]).Description;
                }

                var valor = ((int)Enum.Parse(t, member.Name));
                result.Add(new SelectListItem()
                {
                    Text = descripcion,
                    Value = valor.ToString()
                });
            }
            return result;
        }
    }
}
