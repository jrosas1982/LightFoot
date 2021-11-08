using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Aplicacion.Helpers;
using Core.Aplicacion.Interfaces;
using Core.Dominio.AggregatesModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Site.Dtos;
using Web.Site.Helpers;

namespace Web.Site.Areas
{
    [AllowAnonymous]
    [Area("Auth")]
    [Route("[controller]/[action]")]


    public class AuthController : CustomController
    {
        private ISucursalService _sucursalService;
        private IUserLoginService _userLoginService;
        public AuthController(ISucursalService sucursalService, IUserLoginService userLoginService)
        {
            _sucursalService = sucursalService;
            _userLoginService = userLoginService;
        }

        public async Task<IActionResult> LogIn(string returnurl)
        {
            try
            {
                var sucursalesList = await _sucursalService.GetSucursales();

                var model = new UserLoginModel()
                {
                    Sucursales = sucursalesList.Select(x => new DesplegableModel() { Id = x.Id, Descripcion = $"{x.Nombre} - {x.Descripcion}" }).ToList(),
                };

                ViewBag.ReturnUrl = returnurl;
                return View(model);
            }
            catch (Exception ex)
            {
                MensajeError(ex.Message);
            }
            return View();
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginUser(UserLoginModel userLoginModel, string returnurl)
        {
            try
            {
                var sucursalesList = await _sucursalService.GetSucursales();

                userLoginModel.Sucursales = sucursalesList.Select(x => new DesplegableModel() { Id = x.Id, Descripcion = $"{x.Nombre} - {x.Descripcion}" }).ToList();

                var user = await _userLoginService.ValidarUsuario(userLoginModel.UserLoginDTO);

                var claims = await GenerarClaimsAsync(user, userLoginModel);

                var authenticationProperties = GenerarAuthenticationProperties(userLoginModel.UserLoginDTO.Recuerdame);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claims, authenticationProperties);

                if (string.IsNullOrEmpty(returnurl))
                    return Redirect("/");
                    //return RedirectToAction("Index", "Dashboard", new { area = "Fabrica" });

                return Redirect(returnurl);
            }
            catch (Exception ex)
            {
                MensajeError(ex.Message);
                return View("LogIn", userLoginModel);
            }
        }

        public async Task<IActionResult> LogOutUser()
        {
            try
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction("LogIn", "Auth");
            }
            catch (Exception ex)
            {
                MensajeError(ex.Message);
            }
            return RedirectToAction("LogIn", "Auth");
        }

        private async Task<ClaimsPrincipal> GenerarClaimsAsync(Usuario usuario, UserLoginModel userLoginModel)
        {
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);

            var sucursal = await _sucursalService.BuscarPorId(userLoginModel.UserLoginDTO.IdSucursal);

            identity.AddClaim(new Claim("NombreApellido", $"{usuario.Nombre} {usuario.Apellido}"));
            identity.AddClaim(new Claim("IdSucursal", userLoginModel.UserLoginDTO.IdSucursal.ToString()));
            identity.AddClaim(new Claim("NombreSucursal", sucursal.Nombre));
            identity.AddClaim(new Claim(ClaimTypes.Name, userLoginModel.UserLoginDTO.NombreUsuario));
            identity.AddClaim(new Claim(ClaimTypes.Email, usuario.Email));
            identity.AddClaim(new Claim(ClaimTypes.Role, usuario.UsuarioRol.ToString()));
            identity.AddClaim(new Claim("GrupoRol", usuario.UsuarioRol.GetGroupName()));

            var principal = new ClaimsPrincipal(identity);

            return principal;
        }

        private AuthenticationProperties GenerarAuthenticationProperties(bool esPermanente)
        {
            return new AuthenticationProperties()
            {
                ExpiresUtc = DateTime.Now.AddDays(esPermanente ? 10 : 1),
                IsPersistent = esPermanente,
                IssuedUtc = DateTime.UtcNow
            };
        }


    }
}
