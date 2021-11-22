using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Aplicacion.Auth;
using Core.Aplicacion.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Site.Helpers;

namespace Web.Site.Areas
{
    [Area("sucursal")]
    //[Authorize(Policy = Policies.IsSupervisor)]
    [Route("[area]/[controller]/[action]")]
    public class DashboardSucursalController : CustomController
    {
        public ISolicitudService _solicitudService;
        public IInsumoService _insumoService;
        public IOrdenProduccionService _ordenProduccionService;
        public IMapper _mapper;
        public DashboardSucursalController(ISolicitudService solicitudService, IInsumoService insumoService ,
           IOrdenProduccionService ordenProduccionService , IMapper mapper )
        {
            _solicitudService = solicitudService;
            _insumoService = insumoService;
            _ordenProduccionService = ordenProduccionService;
            _mapper = mapper;

        }
        public async Task<IActionResult> Index()
        {
            var solicitudes = await _solicitudService.GetSolicitudes();

            var CantSolHoy = solicitudes.Where(x => x.FechaCreacion > DateTime.Now.AddDays(-1)).Count();

            ViewBag.SolicitudesHoy = CantSolHoy;

            return View();
        }

    }
}
