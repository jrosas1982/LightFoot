﻿using Core.Aplicacion.Interfaces;
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

            return View();
        } 
        public async Task<IActionResult> EditarParametro(FabricaParametro fabricaParametro) {

            var result = await _parametroService.EditarParamtro(fabricaParametro);
            if (result) {
                IEnumerable<FabricaParametro> parame = await _parametroService.GetParametros();
                return PartialView("_listadoParametro", parame);
            }
            else {
                return Json(new { redirectToUrl = @Url.Action("Index", "FabricaParametros", new { area = "abm" }) });
                //return RedirectToAction("Index");
            }
        }  

        public async Task<IActionResult> CrearEditarParametro() {
            List<SelectListItem> valores = new List<SelectListItem>();
            for (int i = 0; i <= 5 ; i++)
            {
                SelectListItem valor = new SelectListItem();
                valor.Text = i.ToString();
                valor.Value = i.ToString();
                valores.Add(valor);
            }
           
            ViewBag.ListaValor = valores;
            ViewBag.ListParametros = ToListSelectListItem<Parametro>();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearEditarParametro(FabricaParametro fabricaParametro)
        {
            ViewBag.ListParametros = ToListSelectListItem<Parametro>();

            try
            {
                var result = await _parametroService.CrearParamtro(fabricaParametro);
                if (!result) { 
                    return View(fabricaParametro); }
                else
                    return Json(new { redirectToUrl = @Url.Action("Index", "FabricaParametros", new { area = "abm" }) });
                    //return RedirectToAction("Index");
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
