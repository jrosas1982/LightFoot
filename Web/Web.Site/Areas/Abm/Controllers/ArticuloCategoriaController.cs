using System.Linq;
using System.Threading.Tasks;
using Core.Aplicacion.Interfaces;
using Core.Dominio.AggregatesModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Site.Areas.Abm.Controllers
{
    [Authorize]
    [Area("abm")]
    [Route("[area]/[controller]/[action]")]
    public class ArticuloCategoriaController : Controller
    {
        private IArticuloCategoriaService _articuloCategoriaService;

        public ArticuloCategoriaController(IArticuloCategoriaService articuloCategoriaService)
        {
            _articuloCategoriaService = articuloCategoriaService;
        }

        public async Task<IActionResult> Index()
        {
            var articuloCategoriasList = await _articuloCategoriaService.GetCategorias();
            return View(articuloCategoriasList.ToList());
        }

        public async Task<IActionResult> CrearEditarCategoria(int IdCategoria)
        {
            ArticuloCategoria articuloCategoria;

            if (IdCategoria != 0) // 0 = crear
                articuloCategoria = await _articuloCategoriaService.BuscarPorId(IdCategoria);
            else
                articuloCategoria = new ArticuloCategoria();

            return View(articuloCategoria);
        }

        public async Task<IActionResult> Crear(ArticuloCategoria articuloCategoria)
        {
            await _articuloCategoriaService.CrearCategoria(articuloCategoria);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Editar(ArticuloCategoria articuloCategoria)
        {
            await _articuloCategoriaService.EditarCategoria(articuloCategoria);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Eliminar(ArticuloCategoria articuloCategoria)
        {
            var result = await _articuloCategoriaService.EliminarCategoria(articuloCategoria);
            return Ok(result);
        }

    }
}
