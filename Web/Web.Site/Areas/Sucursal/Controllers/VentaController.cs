using System.Linq;
using System.Threading.Tasks;
using Core.Aplicacion.FIlters;
using Core.Aplicacion.Interfaces;
using Core.Dominio.AggregatesModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Site.Dtos;
using Web.Site.Helpers;
using System;
using Core.Dominio.CoreModelHelpers;
using System.Collections.Generic;

namespace Web.Site.Areas
{
    [Area("sucursal")]
    [Route("[area]/[controller]/[action]")]
    public class VentaController : CustomController
    {
        private IVentaService _ventaService;
        
        public VentaController(IVentaService ventaService)
        {
            _ventaService = ventaService;
        }
        public async Task<IActionResult> Index()
        {
            var ventasList = await _ventaService.GetVentas(); 

            return View(ventasList);
        }

        public async Task<IActionResult> VentaArticulo( /*int idCliente*/ )
        {
            //var proveedoresListTask = _proveedorService.GetProveedores();
            //var articulosListTask = _articuloService.GetArticulos();
            //var clienteListTask = _clienteService.BuscarPorId(idCliente)

            //await Task.WhenAll(proveedoresListTask, articulosListTask, clienteListTask);

            //var proveedoresList = proveedoresListTask.Result;
            //var articulosList = articulosListTask.Result;
            //var clienteList = clienteListTask.Result;

            //ViewBag.Cliente = clienteList.Select(x => new SelectListItem() { Text = $"{x.Nombre}", Value = $"{x.Id}" });

            VentaModel ventaModel = new VentaModel();

            //compraArticuloModel.Articulos = articulosList.Select(x => new SelectListItem() { Text = $"{x.ArticuloCategoria.Descripcion} - {x.CodigoArticulo} - {x.Nombre} - {x.Color} - Talle {x.TalleArticulo} ", Value = $"{x.Id}" });
            //compraArticuloModel.Proveedores = proveedoresList.Select(x => new SelectListItem() { Text = $"{x.Nombre}", Value = $"{x.Id}" });

            return View();
        }
    }
}
