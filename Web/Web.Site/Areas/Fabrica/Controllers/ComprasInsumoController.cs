using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Aplicacion.FIlters;
using Core.Aplicacion.Interfaces;
using Core.Dominio.AggregatesModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Site.Dtos;
using Web.Site.Helpers;

namespace Web.Site.Areas
{ 
//[Authorize (Policy = Policies.IsControlador)]
    [Area("fabrica")]
    [Route("[area]/[controller]/[action]")]
    public class ComprasInsumoController : CustomController
    {
        //private ICompraInsumoService _compraInsumoService;
        private IInsumoService _insumoService;

        public ComprasInsumoController(/*ICompraInsumoService compraInsumoService,*/ IInsumoService insumoService)
        {
            //_compraInsumoService = compraInsumoService;
            _insumoService = insumoService;
        }

        public async Task<IActionResult> Index()
        {
            //var comprasList = await _compraInsumoService.GetCompras();

            //return View(comprasList);
            return View();
        }

        public async Task<IActionResult> ComprarInsumos(int idCompraInsumo)
        {
            //var compra = await _compraInsumoService.BuscarPorId(idCompraInsumo);
            //ViewBag.Insumos = await _insumoService.GetInsumos();

            //return View(compra);
            return View();
        }

        //public async Task<IActionResult> RealizarCompra(CompraInsumo compra)
        //{
        //    await _compraInsumoService.CrearCompra(compra, compra.CompraInsumoDetalles);
        //    return RedirectToAction("Index");

        //}

        

    }

}