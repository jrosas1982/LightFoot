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
using Web.Site.Areas.Fabrica;


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
        private IRecetaService _recetaService;

        public SolicitudController(ISolicitudService solicitudService, ISucursalService sucursalService, IArticuloService articuloService, IRecetaService recetaService)
        {
            _solicitudService = solicitudService;
            _sucursalService = sucursalService;
            _articuloService = articuloService;
        }

        public async Task<IActionResult> Index(bool error, string msj)
        {
            var solicitudListTask = _solicitudService.GetSolicitudes();
            var SucursalesTask = _sucursalService.GetSucursales();
            var EstadosSolicitudTask = _solicitudService.GetEstadosSolicitud();

            await Task.WhenAll(solicitudListTask, SucursalesTask, EstadosSolicitudTask);

            var solicitudList = solicitudListTask.Result;

            ViewBag.Sucursales = SucursalesTask.Result;
            ViewBag.EstadosSolicitud = EstadosSolicitudTask.Result;

            ViewBag.FechaDesde = DateTime.Today - TimeSpan.FromDays(30);
            ViewBag.FechaHasta = DateTime.Today;
            if (error)
            {
                ViewBag.CatchError = true;
                ViewBag.MensajeError = "Error inesperado con Mensaje: " + msj;
            }
            return View(solicitudList);
        }

        public async Task<IActionResult> SolicitudDetalle(int IdSolicitud)
        {
            var solicitud = await _solicitudService.BuscarPorId(IdSolicitud);
            //var solicitud = solicitudes.Where(x => x.Id == IdSolicitud).First();

            return View(solicitud);
        }

        public async Task<IActionResult> CrearEditarSolicitud(int IdSolicitud)
        {
            SolicitudModel solicitudModel = new SolicitudModel();
            Solicitud solicitud;
            try
            {
                var estadoSolicitudListTask = _solicitudService.GetEstadosSolicitud();
                var sucursalesListTask = _sucursalService.GetSucursales();
                var articulosListTask = _articuloService.GetArticulos();

                await Task.WhenAll(estadoSolicitudListTask, sucursalesListTask, articulosListTask);

                var estadoSolicitudList = estadoSolicitudListTask.Result;
                var sucursalesList = sucursalesListTask.Result;
                var articulosList = articulosListTask.Result;

                if (IdSolicitud != 0) // 0 = crear
                    solicitud = await _solicitudService.BuscarPorId(IdSolicitud);
                else
                    solicitud = new Solicitud();
                solicitudModel = new SolicitudModel(solicitud, estadoSolicitudList);

                solicitudModel.Sucursales = sucursalesList.Select(x => new DesplegableModel() { Id = x.Id, Descripcion = $"{x.Nombre} - {x.Descripcion}" });
                solicitudModel.Articulos = articulosList.Select(x => new SelectListItem() { Text = $"{x.Nombre}", Value = $"{x.Nombre}" }).GroupBy(p => new { p.Text }).Select(g => g.First()).ToList();
                solicitudModel.Colores = articulosList.Select(x => new SelectListItem() { Text = $"{x.Color}", Value = $"{x.Color}" });
                solicitudModel.Talles = articulosList.Select(x => new SelectListItem() { Text = $"{x.TalleArticulo}", Value = $"{x.TalleArticulo}" });
                return View(solicitudModel);
            }
            catch (Exception ex)
            {
                ViewBag.CatchError = true;
                ViewBag.MensajeError = $"Error al CrearEditarSolicitud Error: {ex.Message}";
                return View(solicitudModel);
            }
       
        }

        public async Task<IActionResult> ColoresPorArticulo(string NumeroTalle , string NombreArticulo) 
        {
                var articulosList = await _articuloService.GetArticulos();
                SolicitudModel solicitudModel = new SolicitudModel();
                solicitudModel.Colores = articulosList.Where(x => x.TalleArticulo == NumeroTalle && x.Nombre == NombreArticulo).Select(c => new SelectListItem() { Text = $"{c.Color}", Value = $"{c.Color}" }).GroupBy(p => new { p.Text }).Select(g => g.First()).ToList(); ;
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
            try
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
            catch (Exception ex)
            {
                return Redirect($"/fabrica/Solicitud/Index?error=true&msj= {ex.Message}");
            }

        }

        public async Task<IActionResult> AprobarSolicitud(int idSolicitud)
        {
            try
            {
                await _solicitudService.AprobarSolicitud(idSolicitud);
                return RedirectToAction("Index");
            }
             catch (Exception ex)
            {
                return Redirect($"/fabrica/Solicitud/Index?error=true&msj= {ex.Message}");
            }
        }

        public async Task<IActionResult> Rechazar(int idSolicitud)
        {
            try
            {
                await _solicitudService.RechazarSolicitud(idSolicitud);
                var result = await _solicitudService.GetSolicitudes();
                return PartialView("_SolicitudTable", result);
            }
            catch (Exception ex)
            {
                return Redirect($"/fabrica/Solicitud/Index?error=true&msj= {ex.Message}");
            }
        }
        

        public async Task<IActionResult> Filtrar(SolicitudFilter solicitudFilter)
        {
        
             var result = await _solicitudService.GetSolicitudes(solicitudFilter);
            return PartialView("_SolicitudTable", result);

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