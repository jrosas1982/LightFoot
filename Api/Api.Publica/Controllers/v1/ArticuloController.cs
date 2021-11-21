using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Publica.Dtos;
using Core.Aplicacion.Interfaces;
using Core.Dominio.AggregatesModel;
using Core.Dominio.CoreModelHelpers;
using Core.Infraestructura;
using LightFoot.Api.Publica.Dtos.CompraVirtual;
using LightFoot.Api.Publica.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace LightFoot.Api.Publica.Controllers.v1
{
    [ApiController]
    [Route("v1/[Controller]")]
    [ServiceFilter(typeof(ApiKeyAuth))]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    public class ArticuloController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly ILogger<ArticuloController> _logger;
        private readonly IArticuloService _articuloService;

        public ArticuloController(ILogger<ArticuloController> logger, AppDbContext db, IArticuloService articuloService)
        {
            _logger = logger;
            _db = db;
            _articuloService = articuloService;
        }
        

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ArticuloResponse>), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [SwaggerOperation("Retorna todos los articulos")]
        public async Task<IActionResult> Get()
        {
            var articulos = await _articuloService.GetArticulos();
         
            if (articulos == null) return NotFound();
            if (!articulos.Any()) return NoContent();
            
            var response = articulos.Select(x => new ArticuloResponse()
            {
                IdArticulo = x.Id,
                Nombre = x.Nombre,
                CodigoArticulo = x.CodigoArticulo,
                Color = x.Color,
                TalleArticulo = x.TalleArticulo,
                Descripcion = x.Descripcion,
                ArticuloCategoria = x.ArticuloCategoria.Descripcion
            });          

            return Ok(response);
        }
    }
}
