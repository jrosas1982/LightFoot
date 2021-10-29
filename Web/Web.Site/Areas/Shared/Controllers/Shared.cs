using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Aplicacion.Interfaces;
using Core.Dominio.AggregatesModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Site.Dtos;
using Web.Site.Helpers;

namespace Web.Site.Areas.Shared.Controllers
{
    [Authorize]
    [Area("Shared")]
    [Route("[action]")]
    public class Shared : CustomController
    {
        [Route("/")]

        public IActionResult Welcome()
        {
            return View();
        }


    }
}
