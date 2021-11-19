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
        public IMapper _mapper;
        public CuentaCorrienteController(ICuentaCorrienteService cuentaCorrienteService, IMapper mapper)
        {
            _cuentaCorrienteService = cuentaCorrienteService;
            _mapper = mapper;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var cuentaCorriente = await _cuentaCorrienteService.GetCuentaCorrientes();
            var cuentasCorrientes = new List<CuentaCorrienteModel>();
            foreach (var item in cuentaCorriente)
            {
                var clienteCuentasCorriente = new CuentaCorrienteModel();
                cuentasCorrientes.Add(_mapper.Map<CuentaCorrienteModel>(item));
            }

            return View(cuentasCorrientes);
        }


    }
}
