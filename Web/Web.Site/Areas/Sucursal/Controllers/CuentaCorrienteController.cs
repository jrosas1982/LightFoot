using AutoMapper;
using Core.Aplicacion.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Site.Helpers;

namespace Web.Site.Areas
{
    //[Authorize (Policy = Policies.IsControlador)]
    [Area("sucursal")]
    [Route("[area]/[controller]/[action]")]
    public class CuentaCorrienteController : CustomController
    {
        public ICuentaCorrienteService _cuentaCorrienteService;
        public IVentaService _ventaService;
        public IClienteService _clienteService;
        public IMapper _mapper;
        public CuentaCorrienteController(ICuentaCorrienteService cuentaCorrienteService, IMapper mapper, IVentaService ventaService, IClienteService clienteService)
        {
            _cuentaCorrienteService = cuentaCorrienteService;
            _mapper = mapper;
            _ventaService = ventaService;
            _clienteService = clienteService;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var cuentaCorriente = await _cuentaCorrienteService.GetCuentaCorrientes();

            var ventas = await _ventaService.GetVentas();


            var cuentaAgrupada = cuentaCorriente.GroupBy(x => x.IdCliente).Select(b => new {
                IdCliente = b.Key,
                TotalRecibido = b.Sum(x => x.MontoPercibido)
            }).ToList();

            var agrupaVenta = ventas.GroupBy(x => x.IdCliente).Select(sp => new {
                IdCliente = sp.Key,
                MontoTotalCompras = sp.Sum(m => m.MontoTotal)
            }).ToList();
            
            var cuentasModelo = cuentaAgrupada.Join(agrupaVenta, c => c.IdCliente, v => v.IdCliente, (c, v) => new CuentaCorrienteModel
            {
                IdCliente = v.IdCliente,
                //NombreCliente = cuentaCorriente.Where(x => x.Cliente.Id == v.IdCliente).Select(x => x.Cliente.Nombre).FirstOrDefault().ToString(),
                NombreCliente = cuentaCorriente.FirstOrDefault(x => x.Cliente.Id == v.IdCliente).Cliente.Nombre,
                Cliente = cuentaCorriente.FirstOrDefault(x => x.Cliente.Id == v.IdCliente).Cliente,
                TotalFacturado = v.MontoTotalCompras,              
                TotalCobrado = c.TotalRecibido,
                DeudaTotal = v.MontoTotalCompras - c.TotalRecibido
            }).ToList();

            return View(cuentasModelo);
        }

        public async Task<IActionResult> CargarPagosRecibidos(int IdCliente)
        {
            var cuentaCorrienteCliente = await _cuentaCorrienteService.GetCuentaCorrientesPorCliente(IdCliente);
            return PartialView("_PagosClienteDetalle", cuentaCorrienteCliente);          
        }

        public async Task<IActionResult> CargarComprasCliente(int IdCliente)
        {
            var cuentaCorrienteCliente = await _ventaService.GetVentasPorCliente(IdCliente);
            return PartialView("_ComprasClienteDetalle", cuentaCorrienteCliente);
        }

        public async Task<IActionResult> CargarInfoCLiente(int IdCliente)
        {
            var response = await _clienteService.BuscarPorId(IdCliente);
            return PartialView("_ComprasClienteCabeceraDetalle", response);
        }


    }
}
