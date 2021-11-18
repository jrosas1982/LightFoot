using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Aplicacion.Interfaces;
using Core.Dominio.AggregatesModel;
using Core.Dominio.CoreModelHelpers;
using Core.Infraestructura;
using LightFoot.Api.Publica.Dtos.CompraVirtual;
using LightFoot.Api.Publica.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace LightFoot.Api.Publica.Controllers.v1
{
    [ApiController]
    [Route("v1/[Controller]")]
    //[ServiceFilter(typeof(ApiKeyAuth))]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    public class VentaController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly ILogger<VentaController> _logger;
        private readonly IVentaService _ventaService;

        public VentaController(ILogger<VentaController> logger, AppDbContext db, IVentaService ventaService)
        {
            _logger = logger;
            _db = db;
            _ventaService = ventaService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(VentaResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("Registra una nueva venta en el sistema")]
        public async Task<IActionResult> Post(VentaRequest request)
        {
            return CreatedAtAction(nameof(Get), new { idVenta = 12 }, new VentaResponse());

            var cliente = _db.Clientes.SingleOrDefault(x => x.Id == request.IdCliente);
            if (cliente == null)
                return BadRequest("El cliente no existe");

            var sucursal = _db.Sucursales.SingleOrDefault(x => x.Id == request.IdSucursal);
            if (sucursal == null)
                return BadRequest("La sucursal no existe");

            if (!Enum.IsDefined(typeof(VentaTipo), request.VentaTipo))
                return BadRequest("El tipo de venta elegido no existe"); 
            
            if (request.Detalles.Any(x => !_db.Articulos.Any(a => a.Id == x.IdArticulo)))
                return BadRequest("Al menos un articulo no existe");

            if (request.Detalles.Any(x => x.Cantidad < 0))
                return BadRequest("Al menos un articulo posee cantidad menor a 0");

            var detalles = request.Detalles.Select(x => new NuevaVentaDetalleModel()
            {
                Cantidad = x.Cantidad,
                IdArticulo = x.IdArticulo
            });

            var venta = await _ventaService.CrearVenta(request.IdCliente, request.VentaTipo, request.DescuentoRealizado, detalles);

            _logger.LogInformation($"Se registro la venta {venta.Id} ");

            return CreatedAtAction(nameof(Get), new { idVenta = venta.Id }, venta);
        }


        [HttpGet("{idVenta}")]
        [ProducesResponseType(typeof(VentaResponse), 200)]
        [ProducesResponseType(404)]
        [SwaggerOperation("Retorna una venta a partir de su id")]
        public async Task<IActionResult> Get(int idVenta)
        {
            var venta = _db.Ventas.SingleOrDefault(x => x.Id == idVenta); // Busco la venta en la db
            if (venta == null) return NotFound($"No se encontro la venta {idVenta}");

            var response = new VentaResponse()
            {
                IdVenta = venta.Id,
                NombreSucursal = venta.Sucursal.Nombre,
                NombreCliente = venta.Cliente.Nombre,
                NombreUsuarioVendedor = venta.Usuario.NombreUsuario + venta.Usuario.Apellido,
                VentaTipo = venta.VentaTipo,
                MontoTotal = venta.MontoTotal,
                DescuentoRealizado = venta.Descuento,
                Fecha = venta.FechaCreacion,
                FechaPagado = venta.FechaPagado,
                Detalles = venta.VentaDetalles.Select(x => new VentaDetalleResponse()
                {
                    Articulo = x.Articulo.Nombre,
                    Cantidad = x.Cantidad,
                    PrecioUnitario = x.PrecioUnitario,
                    PrecioTotal = x.Cantidad * x.PrecioUnitario
                })
            };

            _logger.LogInformation($"Se busco la venta {idVenta}");

            return Ok(response);
        }
    }
}
