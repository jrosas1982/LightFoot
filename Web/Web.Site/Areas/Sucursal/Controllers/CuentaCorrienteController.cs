using AutoMapper;
using Core.Aplicacion.Interfaces;
using Core.Dominio.AggregatesModel;
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
            var cuentasModelo = await GetCuentaCorrienteModelsAsync(cuentaCorriente);

            ViewBag.TypeaheadIdCliente = cuentasModelo.Select(r => r.IdCliente.ToString());
            ViewBag.TypeaheadNombreCliente = cuentasModelo.Select(r => r.NombreCliente);

            return View(cuentasModelo);
        }

        public async Task<IEnumerable<CuentaCorrienteModel>> GetCuentaCorrienteModelsAsync(IEnumerable<ClienteCuentaCorriente> ctmodel)
        {
            var ventas = await _ventaService.GetVentas();

            var cuentaAgrupada = ctmodel.GroupBy(x => x.IdCliente).Select(b => new {
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
                NombreCliente = ctmodel.FirstOrDefault(x => x.Cliente.Id == v.IdCliente).Cliente.Nombre,
                Cliente = ctmodel.FirstOrDefault(x => x.Cliente.Id == v.IdCliente).Cliente,
                TotalFacturado = v.MontoTotalCompras,              
                TotalCobrado = c.TotalRecibido,
                DeudaTotal = v.MontoTotalCompras - c.TotalRecibido
            }).ToList();
            return cuentasModelo;
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


        public async Task<IActionResult> FiltrarCuentas(string nombreCuenta)
        {
            var cuentaCorriente = await _cuentaCorrienteService.GetCuentaCorrientes();


            if (!string.IsNullOrWhiteSpace(nombreCuenta))
            {
                cuentaCorriente = cuentaCorriente.Where(x => x.Id.ToString().Equals(nombreCuenta.ToLower())                                                 
                                                            || x.Cliente.Nombre.ToLower().Equals(nombreCuenta.ToLower())).ToList();

                if (!cuentaCorriente.Any())
                    cuentaCorriente = cuentaCorriente.Where(x => x.Id.ToString().Contains(nombreCuenta.ToLower())
                                                            || x.Cliente.Nombre.ToLower().Contains(nombreCuenta.ToLower()));
            }
            var cta = await GetCuentaCorrienteModelsAsync(cuentaCorriente);


            return PartialView("_CuentaIndexTable", cta);
        }
    }
}
