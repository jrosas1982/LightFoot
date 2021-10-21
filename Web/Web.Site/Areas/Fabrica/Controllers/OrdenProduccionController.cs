using System.Linq;
using System.Threading.Tasks;
using Core.Aplicacion.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Web.Site.Helpers;

namespace Web.Site.Areas
{
    [Area("fabrica")]
    [Route("[area]/[controller]/[action]")]
    public class OrdenProduccionController : CustomController
    {
        private IOrdenProduccionService _ordenProduccionService;
        private ISucursalService _sucursalService;
        private IArticuloService _articuloService;

        public OrdenProduccionController(IOrdenProduccionService ordenProduccionService, ISucursalService sucursalService, IArticuloService articuloService)
        {
            _ordenProduccionService = ordenProduccionService;
            _sucursalService = sucursalService;
            _articuloService = articuloService;
        }

        public async Task<IActionResult> Index()
        {
            var ordenesList = await _ordenProduccionService.GetOrdenes();

            //var model = new SolicitudIndexModel()
            //{
            //    Solicitudes = solicitudList.ToList(),
            //    Sucursales = sucursalesList.ToList(),
            //};

            return View(ordenesList);
        }

        public async Task<IActionResult> DetallesOrdenProduccion(int idOrdenProduccion)
        {
            var ordenesList = await _ordenProduccionService.GetOrdenes();

            var model = new OrdenProduccionDetalleModel()
            {
                OrdenProduccion = ordenesList.Where(x => x.Id == idOrdenProduccion).First(),
                EstadosOrdenProduccion = await _ordenProduccionService.GetEstadosOrden(),
                EtapasOrdenProduccion = await _ordenProduccionService.GetEtapasOrden(),
                EstadosEtapaOrdenProduccion = await _ordenProduccionService.GetEstadoEtapasOrden(),
                OrdenProduccionEventos = await _ordenProduccionService.GetOrdenEventos(idOrdenProduccion)
            };

            return View(model);
        }
    }
}
