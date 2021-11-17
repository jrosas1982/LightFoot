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
        private IClienteService _clienteService;
        
        public VentaController(IVentaService ventaService, IClienteService clienteService)
        {
            _ventaService = ventaService;
            _clienteService = clienteService;
        }
        public async Task<IActionResult> Index()
        {
            var ventasList = await _ventaService.GetVentas();

            var clienteListTask = _clienteService.GetClientes();
            await Task.WhenAll(clienteListTask);
            var clienteList = clienteListTask.Result;
            ViewBag.Clientes = clienteList.Select(x => new SelectListItem() { Text = $"{x.Nombre}", Value = $"{x.Id}" });

            return View(ventasList);
        }

        public async Task<IActionResult> VentaArticulo(int IdCliente)
        {
            //var proveedoresListTask = _proveedorService.GetProveedores();
            //var articulosListTask = _articuloService.GetArticulos();

            //await Task.WhenAll(proveedoresListTask, articulosListTask);

            //var proveedoresList = proveedoresListTask.Result;
            //var articulosList = articulosListTask.Result;
            Cliente cliente = await _clienteService.BuscarPorId(IdCliente);
            VentaModel ventaModel = new VentaModel()
            {
                Cliente = cliente
            };

            //compraArticuloModel.Articulos = articulosList.Select(x => new SelectListItem() { Text = $"{x.ArticuloCategoria.Descripcion} - {x.CodigoArticulo} - {x.Nombre} - {x.Color} - Talle {x.TalleArticulo} ", Value = $"{x.Id}" });
            //compraArticuloModel.Proveedores = proveedoresList.Select(x => new SelectListItem() { Text = $"{x.Nombre}", Value = $"{x.Id}" });

            return View(ventaModel);
        }
    }
}
