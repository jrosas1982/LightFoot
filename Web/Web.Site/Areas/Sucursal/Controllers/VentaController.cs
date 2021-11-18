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
        private IArticuloService _articuloService;
        private IArticuloCategoriaService _articuloCategoriaService;
        
        public VentaController(IVentaService ventaService, IClienteService clienteService, IArticuloService articuloService, IArticuloCategoriaService articuloCategoriaService)
        {
            _ventaService = ventaService;
            _clienteService = clienteService;
            _articuloService = articuloService;
            _articuloCategoriaService = articuloCategoriaService;
        }
        public async Task<IActionResult> Index()
        {
            var ventasList = await _ventaService.GetVentas();
            var ventaTiposListTask = _ventaService.GetTiposVenta();
            var clienteListTask = _clienteService.GetClientes();

            await Task.WhenAll(clienteListTask, ventaTiposListTask, clienteListTask);

            var clienteList = clienteListTask.Result;
            var ventasTipoList = ventaTiposListTask.Result;

            ViewBag.Clientes = clienteList.Select(x => new SelectListItem() { Text = $"{x.Nombre}", Value = $"{x.Id}" });
            ViewBag.VentaTipos = ventasTipoList.ToArray();

            return View(ventasList);
        }

        public async Task<IActionResult> VentaArticulo(int IdCliente, VentaTipo ventaTipo)
        {
            var articulosListTask = _articuloService.GetArticulos();
            var ventaTiposListTask = _ventaService.GetTiposVenta();

            await Task.WhenAll(articulosListTask, ventaTiposListTask);

            var articulosList = articulosListTask.Result;
            var ventasTipoList = ventaTiposListTask.Result;

            Cliente cliente = await _clienteService.BuscarPorId(IdCliente);
            VentaModel ventaModel = new VentaModel()
            {
                Cliente = cliente,
                VentaTipo = ventaTipo
            };
            ventaModel.Articulos = articulosList.Select(x => new SelectListItem() { Text = $"{x.ArticuloCategoria.Descripcion} - {x.CodigoArticulo} - {x.Nombre} - {x.Color} - Talle {x.TalleArticulo} ", Value = $"{x.Id}" });
            ventaModel.VentaTipos = ventasTipoList.ToArray();

            return View(ventaModel);
        }

        public async Task<IActionResult> ActualizarPrecio(int idArticulo, VentaTipo ventatipo, decimal cantidad)
        {
            if (idArticulo == 0)
                return Json(0);

            var Articulo = await _articuloService.BuscarPorId(idArticulo);

            var ArticuloPrecio= new decimal(1);
            if (ventatipo == VentaTipo.Mayorista)
            {
                ArticuloPrecio = Articulo.PrecioMayorista;
            }
            else {
                ArticuloPrecio = Articulo.PrecioMinorista;
            }

            return Json(ArticuloPrecio * cantidad);
        }

        public async Task<IActionResult> AgregarDetalle(VentaDetalleModel data)
        {
            int idArticulo = data.IdArticulo;
            var articulo = await _articuloService.BuscarPorId(idArticulo);
            var categoria = await _articuloCategoriaService.BuscarPorId(articulo.IdArticuloCategoria);
            data.CodigoArticulo = articulo.CodigoArticulo;
            data.NombreArticulo = articulo.Nombre;
            data.DescripcionArticulo = articulo.Descripcion;
            data.ArticuloCategoria = categoria.Descripcion;
            data.ColorArticulo = articulo.Color;
            data.TalleArticulo = articulo.TalleArticulo;

            return PartialView("_VentaArticuloDetalle", data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(VentaModel ventaModel)
        {
            try
            {
                IEnumerable<NuevaVentaDetalleModel> venta = new List<NuevaVentaDetalleModel>();
                foreach (var detalle in ventaModel.VentaDetalleModels)
                {
                    venta.Append(new NuevaVentaDetalleModel()
                    {
                        IdArticulo = detalle.IdArticulo,
                        Cantidad = detalle.Cantidad
                    });
                }
                await _ventaService.CrearVenta(ventaModel.Cliente.Id, ventaModel.VentaTipo, ventaModel.DescuentoRealizado, venta);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
