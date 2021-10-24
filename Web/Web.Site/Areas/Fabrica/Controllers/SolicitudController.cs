using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Aplicacion.Interfaces;
using Core.Dominio.AggregatesModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Site.Dtos;

namespace Web.Site.Areas
{
    //[Authorize (Policy = Policies.IsControlador)]
    [Area("fabrica")]
    [Route("[area]/[controller]/[action]")]
    public class SolicitudController : Controller
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
                Articulos = solicitud.SolicitudDetalles.Select(x => new DesplegableModel() { Id = x.Id, Descripcion = $"{x.Articulo.Id} - {x.Articulo.Nombre}" }),

                Sucursales = await _sucursalService.GetSucursales(),
                EstadosSolicitud = await _solicitudService.GetEstadosSolicitud()
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

        public async Task<IActionResult> Crear(Solicitud solicitud)
        {
            var detallesSolicitud = new List<SolicitudDetalle>()
            {
                new SolicitudDetalle()
                {
                    IdArticulo = 11,
                    CantidadSolicitada = 15,
                    Motivo = "pq si",
                },
                new SolicitudDetalle()
                {
                    IdArticulo = 8,
                    CantidadSolicitada = 15,
                    Motivo = "pq si 2",
                },
                 new SolicitudDetalle()
                {
                    IdArticulo = 4,
                    CantidadSolicitada = 15,
                    Motivo = "pq si 3",
                }
            };

            await _solicitudService.CrearSolicitud(solicitud, detallesSolicitud);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AprobarSolicitud(int idSolicitud)
        {
            await _solicitudService.AprobarSolicitud(idSolicitud);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Eliminar(Solicitud solicitud)
        {
            //var result = await _solicitudService.EliminarSolicitud(solicitud);
            return Ok(/*result*/);
        }

        public IActionResult AgregarDetalle(SolicitudDetalle data)
        {
            return PartialView("_SolicitudDetalle", data);
        }

    }

}