﻿using System.Collections.Generic;
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
    public class SolicitudController : CustomController
    {
        private ISolicitudService _solicitudService;
        private ISucursalService _sucursalService;
        private IArticuloService _articuloService;

        public SolicitudController(ISolicitudService solicitudService, ISucursalService sucursalService, IArticuloService articuloService)
        {
            _solicitudService = solicitudService;
            _sucursalService = sucursalService;
            _articuloService = articuloService;
        }

        public async Task<IActionResult> Index()
        {
            var solicitudList = await _solicitudService.GetSolicitudes();

            var sucursalesList = await _sucursalService.GetSucursales();

            var model = new SolicitudIndexModel()
            {
                Solicitudes = solicitudList.ToList(),
                Sucursales = sucursalesList.ToList(),
                EstadosSolicitud = await _solicitudService.GetEstadosSolicitud()
            };

            return View(model);
        }

        public async Task<IActionResult> SolicitudDetalle(int IdSolicitud)
        {
            var solicitudes = await _solicitudService.GetSolicitudes();
            var solicitud = solicitudes.Where(x => x.Id == IdSolicitud).First();

            var model = new SolicitudDetalleModel()
            {
                Solicitud = solicitud,
                Sucursal = solicitud.Sucursal,
                SolicitudDetalle = solicitud.SolicitudDetalles
            };

            return View(model);
        }

        public async Task<IActionResult> CrearEditarSolicitud(int IdSolicitud)
        {
            Solicitud solicitud;
            var estadoSolicitudList = await _solicitudService.GetEstadosSolicitud(); 
            var sucursalesList = await _sucursalService.GetSucursales();
            var articulosList = await _articuloService.GetArticulos();

            if (IdSolicitud != 0) // 0 = crear
                solicitud = await _solicitudService.BuscarPorId(IdSolicitud);
            else
            solicitud = new Solicitud();
            SolicitudModel solicitudModel = new SolicitudModel(solicitud, estadoSolicitudList);
           
            solicitudModel.Sucursales = sucursalesList.Select(x => new DesplegableModel() { Id = x.Id, Descripcion = $"{x.Nombre} - {x.Descripcion}" });
            solicitudModel.Articulos = articulosList.Select(x => new SelectListItem() { Text = $"{x.Nombre}", Value = $"{x.Nombre}" }).GroupBy(p => new { p.Text }).Select(g => g.First()).ToList();
            solicitudModel.Colores = articulosList.Select(x => new SelectListItem() { Text = $"{x.Color}" , Value = $"{x.Color}" });
            solicitudModel.Talles = articulosList.Select(x => new SelectListItem() { Text = $"{x.TalleArticulo}" , Value = $"{x.TalleArticulo}" } );
            return View(solicitudModel);
        }

        public async Task<IActionResult> ColoresPorArticulo(string NumeroTalle) 
        {
                var articulosList = await _articuloService.GetArticulos();
                SolicitudModel solicitudModel = new SolicitudModel();
                solicitudModel.Colores = articulosList.Where(x => x.TalleArticulo == NumeroTalle).Select(c => new SelectListItem() { Text = $"{c.Color}", Value = $"{c.Color}" }).GroupBy(p => new { p.Text }).Select(g => g.First()).ToList(); ;
                return Json(solicitudModel.Colores);
        }
        
        public async Task<IActionResult> TallesPorArticulo(string NombreArticulo)
        {
            var articulosList = await _articuloService.GetArticulos();
            SolicitudModel solicitudModel = new SolicitudModel();
            solicitudModel.Talles = articulosList.Where(x => x.Nombre == NombreArticulo).Select(c => new SelectListItem() { Text = c.TalleArticulo , Value = c.TalleArticulo }).GroupBy(p => new { p.Text }).Select(g => g.First()).ToList(); 
            return Json(solicitudModel.Talles);
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

            await _solicitudService.CrearSolicitud(solicitud, solicitudDetalles);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AprobarSolicitud(int idSolicitud)
        {
            await _solicitudService.AprobarSolicitud(idSolicitud);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Rechazar(int idSolicitud)
        {
            await _solicitudService.RechazarSolicitud(idSolicitud,"comentario");
            return RedirectToAction("Index");
        }
        

        public async Task<IActionResult> Filtrar(SolicitudFilter solicitudFilter)
        {
            var result = await _solicitudService.GetSolicitudes(solicitudFilter);

            return Ok(result);
        }

        public async Task<IActionResult> AgregarDetalleAsync(SolicitudDetalle data)
        {
            var articulosList = await _articuloService.GetArticulos();
            var articulo = articulosList.FirstOrDefault(x => x.Nombre == data.Articulo.Nombre && x.Color == data.Articulo.Color && x.TalleArticulo == data.Articulo.TalleArticulo);

            data.IdArticulo = articulo.Id;
            return PartialView("_SolicitudDetalle", data );
        }

    }

}