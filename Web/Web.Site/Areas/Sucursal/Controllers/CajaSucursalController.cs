using System.Linq;
using System.Threading.Tasks;
using Core.Aplicacion.FIlters;
using Core.Aplicacion.Interfaces;
using Core.Dominio.AggregatesModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Site.Dtos;
using Web.Site.Helpers;
using System;
using Core.Dominio.CoreModelHelpers;
using System.Collections.Generic;
namespace Web.Site.Areas
{
    [Area("sucursal")]
    [Route("[area]/[controller]/[action]")]
    public class CajaSucursalController : CustomController
    {
        private IAdministracionDeCajaService _cajaService;

        public CajaSucursalController(IAdministracionDeCajaService cajaSucursal)
        {
            _cajaService = cajaSucursal;
        }

        public async Task<IActionResult> Index()
        {
            var cajaListTask = _cajaService.GetMovimientos();

            await Task.WhenAll(cajaListTask);

            var cajaList = cajaListTask.Result;

            return View(cajaList);
        }

        public async Task<IActionResult> Crear(int monto, string comentario)
        {
            try
            {
                var cajaSucursal = new CajaSucursal()
                {
                    Monto = monto,
                    Comentario = comentario,
                };
                await _cajaService.AgregarMovimiento(cajaSucursal);

                var caja = await _cajaService.GetMovimientos();
                return PartialView("_CajaSucursalIndexTable", caja);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
