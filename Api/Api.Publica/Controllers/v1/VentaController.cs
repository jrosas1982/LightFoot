using System.Threading.Tasks;
using LightFoot.Api.Publica.Dtos.CompraVirtual;
using LightFoot.Api.Publica.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace LightFoot.Api.Publica.Controllers.v1
{
    [ApiController]
    [Route("v1/[Controller]")]
    [ServiceFilter(typeof(ApiKeyAuth))]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    public class VentaController : ControllerBase
    {
        private readonly ILogger<VentaController> _logger;

        public VentaController(ILogger<VentaController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(typeof(NuevaVentaRequest), 201)]
        [SwaggerOperation("Registra una nueva venta en el sistema")]
        public async Task<IActionResult> Post(NuevaVentaRequest request)
        {
            var response = 1;
            _logger.LogInformation($"Se registro la venta {response} ");

            return CreatedAtAction(nameof(Get), new { idVenta = request }, request);
        }


        [HttpGet("{idVenta}")]
        [ProducesResponseType(typeof(VentaResponse), 200)]
        [ProducesResponseType(404)]
        [SwaggerOperation("Retorna una venta a partir de su id")]
        public async Task<IActionResult> Get(string idVenta)
        {
            var venta = new VentaResponse(); // Busco la venta en la db
            if (venta == null) return NotFound($"No se encontro la venta {idVenta}");

            var response = new VentaResponse()
            {
                Cantidad = venta.Cantidad,
                Fecha = venta.Fecha,
                IdCliente = venta.IdCliente,
                IdItem = venta.IdItem,
                Precio = venta.Precio
            };

            _logger.LogInformation($"Se busco la venta {idVenta}");

            return Ok(response);
        }
    }
}
