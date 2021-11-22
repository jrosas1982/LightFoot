using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Aplicacion.Auth;
using Core.Aplicacion.Helpers;
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
            var groupName = User.GetUserRole().GetValueOrDefault();

            if (Enum.IsDefined(typeof(UsuarioRol), groupName) && groupName.GetGroupName() == Policies.IsSucursal)
                return Redirect("/sucursal/DashboardSucursal/Index");
            else
                return Redirect("/fabrica/DashboardFabrica/Index");

            return View();
        }


    }
}
