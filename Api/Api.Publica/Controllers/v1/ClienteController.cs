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
    public class ClienteController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly ILogger<ClienteController> _logger;
        private readonly IClienteService _clienteService;

        public ClienteController(ILogger<ClienteController> logger, AppDbContext db, IClienteService clienteService)
        {
            _logger = logger;
            _db = db;
            _clienteService = clienteService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ClienteResponse>), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [SwaggerOperation("Retorna todos los clientes")]
        public async Task<IActionResult> Get()
        {
            var clientes = await _clienteService.GetClientes();

            if (clientes == null) return NotFound();
            if (!clientes.Any()) return NoContent();

            var response = clientes.Select(x => new ClienteResponse()
            {
                IdCliente = x.Id,
                Nombre = x.Nombre,
                Contacto = x.Contacto,
                Direccion = x.Direccion,
                Email = x.Email,
                Telefono = x.Telefono,
                CUIT = x.CUIT,
            });

            return Ok(response);
        }
    }
}
