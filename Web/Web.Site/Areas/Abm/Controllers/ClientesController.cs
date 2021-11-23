using AutoMapper;
using Core.Aplicacion.Interfaces;
using Core.Dominio.AggregatesModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Site.Helpers;

namespace Web.Site.Areas.Abm.Controllers
{
    [Authorize]
    [Area("abm")]
    [Route("[area]/[controller]/[action]")]
    public class ClientesController : CustomController
    {
        public readonly IClienteService _clienteService;
        public readonly IMapper _mapper;
        public ClientesController(IClienteService clienteService, IMapper mapper)
        {
            _clienteService = clienteService;
            _mapper = mapper;
        }

        public async Task<IActionResult> IndexAsync() {

            var Clientes = await _clienteService.GetClientes();
            ViewBag.TypeaheadClientes = Clientes.Select(x =>x.Nombre).Distinct();
            ViewBag.TypeaheadIdClientes = Clientes.Select(x =>x.Id.ToString());

            return View(Clientes);
        }

        public async Task<IActionResult> CrearEditarClienteAsync(int IdCliente)
        {
            ClienteModel cliente = new ClienteModel();

            if (IdCliente != 0)
            {
                var editarCliente =  await _clienteService.BuscarPorId(IdCliente);
               cliente = _mapper.Map<ClienteModel>(editarCliente);
            }

            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearAsync(ClienteModel clientemodel)
        {
            Cliente cliente = new Cliente();

            if(!ModelState.IsValid)
                return View(clientemodel);

            try
            {
                cliente = _mapper.Map<Cliente>(clientemodel);
                await _clienteService.CrearCliente(cliente);
                return Json(new { redirectToUrl = @Url.Action("Index", "Clientes", new { area = "abm" }) });
                //return RedirectToAction("Index","Clientes");
            }
            catch (Exception ex)
            {
                ViewBag.CatchError = true;
                ViewBag.MensajeError = ex.Message;
                return View(clientemodel);
            }
        }

        public async Task<IActionResult> EditarAsync(ClienteModel clientemodel)
        {
            Cliente cliente = new Cliente();

            if (!ModelState.IsValid)
                return View(clientemodel);

            try
            {
                cliente = _mapper.Map<Cliente>(clientemodel);
                await _clienteService.EditarCliente(cliente);
                return Json(new { redirectToUrl = @Url.Action("Index", "Clientes", new { area = "abm" }) });
                //return RedirectToAction("Index", "Clientes");
            }
            catch (Exception ex)
            {
                ViewBag.CatchError = true;
                ViewBag.MensajeError = ex.Message;
                return View(clientemodel);
            }
        }
        [HttpPost]
        public async Task<IActionResult> EliminarAsync(ClienteModel clientemodel)
        {
            Cliente cliente = new Cliente();

            if (!ModelState.IsValid)
                return View(clientemodel);

            try
            {
                cliente = _mapper.Map<Cliente>(clientemodel);           
                return Ok(await _clienteService.EliminarCliente(cliente));
            }
            catch (Exception ex)
            {
                ViewBag.CatchError = true;
                ViewBag.MensajeError = ex.Message;
                return Ok(false);
            }
        }
        public async Task<IActionResult> FiltrarClientes(string NombreCliente)
        {
            var Clientes = await _clienteService.GetClientes();

            if (!string.IsNullOrWhiteSpace(NombreCliente))
            {
                Clientes = Clientes.Where(x => x.Id.ToString().Equals(NombreCliente.ToLower())
                                                            || x.Nombre.ToLower().Equals(NombreCliente.ToLower())
                                                            || x.Id.ToString().ToLower().Equals(NombreCliente.ToLower())).ToList();

                if (!Clientes.Any())
                    Clientes = Clientes.Where(x => x.Id.ToString().Contains(NombreCliente.ToLower())
                                                            || x.Nombre.ToLower().Contains(NombreCliente.ToLower())
                                                            || x.Id.ToString().ToLower().Contains(NombreCliente.ToLower())).ToList();

            }

            return PartialView("_ClientesIndexTable", Clientes);
        }
    }
}
