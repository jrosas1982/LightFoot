using System.Linq;
using System.Threading.Tasks;
using Core.Aplicacion.Interfaces;
using Core.Dominio.AggregatesModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Site.Dtos;
using Web.Site.Helpers;

namespace Web.Site.Areas
{
    [Authorize]
    [Area("abm")]
    [Route("[area]/[controller]/[action]")]
    public class ArticuloController : CustomController
    {
        private IArticuloService _articuloService;
        private IArticuloCategoriaService _articuloCategoriaService;

        public ArticuloController(IArticuloService articuloService, IArticuloCategoriaService articuloCategoriaService)
        {
            _articuloService = articuloService;
            _articuloCategoriaService = articuloCategoriaService;
        }

        public async Task<IActionResult> Index()
        {
            var articulosList = await _articuloService.GetArticulos();
            return View(articulosList.ToList());
        }

        public async Task<IActionResult> CrearEditarArticulo(int IdArticulo)
        {
            Articulo articulo;
            var articuloCategoriaListt = await _articuloCategoriaService.GetCategorias();
            var articuloEstadosList = await _articuloService.GetArticuloEstados();

            if (IdArticulo != 0) // 0 = crear
                articulo = await _articuloService.BuscarPorId(IdArticulo);

            else
                articulo = new Articulo();

            ArticuloModel articuloModel = new ArticuloModel()
            {
                Articulo = articulo,
                ArticuloCategorias = articuloCategoriaListt.Select(x => new DesplegableModel() { Id = x.Id, Descripcion = $"{x.Id} - {x.Descripcion}" }),
                ArticuloEstados = articuloEstadosList.Select(x => new DesplegableModel() { Id = (int)x, Descripcion = x.ToString() }),
            };
            return View(articuloModel);
        }

        public async Task<IActionResult> Crear(Articulo articulo)
        {
            await _articuloService.CrearArticulo(articulo);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Editar(Articulo articulo)
        {
            await _articuloService.EditarArticulo(articulo);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Eliminar(Articulo articulo)
        {
            var result = await _articuloService.EliminarArticulo(articulo);
            return Ok(result);
        }

    }

}
