﻿using System.Linq;
using System.Threading.Tasks;
using Core.Aplicacion.FIlters;
using Core.Aplicacion.Interfaces;
using Core.Dominio.AggregatesModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Site.Dtos;
using Web.Site.Helpers;
using System;

namespace Web.Site.Areas
{
    //[Authorize (Policy = Policies.IsControlador)]
    [Area("fabrica")]
    [Route("[area]/[controller]/[action]")]
    public class CompraInsumoController : CustomController
    {
        private ICompraInsumoService _compraInsumoService;
        private IInsumoService _insumosService;
        private IProveedorService _proveedorService;

        public CompraInsumoController(ICompraInsumoService compraInsumoService, IInsumoService insumosService, IProveedorService proveedorService)
        {
            _compraInsumoService = compraInsumoService;
            _insumosService = insumosService;
            _proveedorService = proveedorService;
        }

        public async Task<IActionResult> Index()
        {
            var compraInsumoList = await _compraInsumoService.GetCompras();

            return View(compraInsumoList);
        }

        public async Task<IActionResult> ComprarInsumos()
        {
            CompraInsumo compraInsumo;
            
            var proveedoresListTask = _proveedorService.GetProveedores();
            var insumosListTask = _insumosService.GetInsumos();

            await Task.WhenAll(proveedoresListTask, insumosListTask);

            var proveedoresList = proveedoresListTask.Result;
            var insumosList = insumosListTask.Result;

            compraInsumo = new CompraInsumo();

            CompraInsumoModel compraInsumoModel = new CompraInsumoModel(compraInsumo);

            compraInsumoModel.Insumos = insumosList.Select(x => new SelectListItem() { Text = $"{x.Nombre}", Value = $"{x.Id}" }).GroupBy(p => new { p.Text }).Select(g => g.First()).ToList();
            compraInsumoModel.Proveedores = proveedoresList.Select(x => new SelectListItem() { Text = $"{x.Nombre}", Value = $"{x.Id}" }).GroupBy(p => new { p.Text }).Select(g => g.First()).ToList();

            return View(compraInsumoModel);
        }

        public async Task<IActionResult> precioCompra(string _IdInsumo, string _IdProveedor, string Cantidad)
        {
            //var articulosList = await _articuloService.GetArticulos();
            var IdInsumo = int.Parse(_IdInsumo);
            var IdProveedor = int.Parse(_IdProveedor);

            var proveedor = await _proveedorService.BuscarPorId(IdProveedor);


            string precio = "22";
            //solicitudModel.Colores = articulosList.Where(x => x.TalleArticulo == NumeroTalle).Select(c => new SelectListItem() { Text = $"{c.Color}", Value = $"{c.Color}" }).GroupBy(p => new { p.Text }).Select(g => g.First()).ToList(); ;
            return Json(precio);
        }

        public async Task<IActionResult> Crear(SolicitudModel solicitudModel)
        {
            var solicitud = new Solicitud()
            {
                IdSucursal = solicitudModel.IdSucursal,
                EstadoSolicitud = solicitudModel.EstadoSolicitud,
                Comentario = solicitudModel.Comentario
            };

            var solicitudDetalles = solicitudModel.SolicitudDetalle.Select(x => new SolicitudDetalle()
            {
                IdArticulo = x.IdArticulo,
                CantidadSolicitada = x.CantidadSolicitada,
                Motivo = x.Motivo
            });

            //await _solicitudService.CrearSolicitud(solicitud, solicitudDetalles);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AgregarDetalleAsync(CompraInsumoDetalle data)
        {
            var insumosList = await _insumosService.GetInsumos();
            var insumo = insumosList.FirstOrDefault(x => x.Nombre == data.Insumo.Nombre);

            var proveedoresList = await _proveedorService.GetProveedores();
            var proveedor = proveedoresList.FirstOrDefault(x => x.Nombre == data.Proveedor.Nombre);


            data.IdInsumo = insumo.Id;
            data.Insumo = insumo;
            data.Proveedor = proveedor;
                        
            return PartialView("_CompraInsumoDetalle", data);
        }

    }

}