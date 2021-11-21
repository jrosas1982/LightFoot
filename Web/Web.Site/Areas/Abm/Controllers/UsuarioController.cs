using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Aplicacion.Auth;
using Core.Aplicacion.Interfaces;
using Core.Dominio.AggregatesModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Site.Areas.Abm;
using Web.Site.Helpers;

namespace Web.Site.Areas
{
    [Authorize(Policy = Policies.IsVendedor)]
    [Area("abm")]
    [Route("[area]/[controller]/[action]")]

    public class UsuarioController : CustomController
    {
        private IUsuarioService _usuarioService;
        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public async Task<IActionResult> Index()
        {
            var usuariosList = await _usuarioService.GetUsuarios();
            return View(usuariosList.ToList());
        }

        public async Task<IActionResult> CrearEditar(int IdUsuario)
        {
            Usuario usuario;
            var usuarioRoles = await _usuarioService.GetUsuarioRoles();

            if (IdUsuario != 0) // 0 = crear
                usuario = await _usuarioService.BuscarPorId(IdUsuario);
            else
                usuario = new Usuario();

            usuario.Contraseña = string.Empty;

            UsuarioModel usuarioModel = new UsuarioModel()
            {
                Usuario = usuario,
                usuarioRoles = usuarioRoles.ToList(),
            };

            return View("CrearEditarUsuario", usuarioModel);
        }

        //public IActionResult CrearEditarUsuario(UsuarioModel usuarioModel)
        //{
        //    return View(usuarioModel);
        //}

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(UsuarioModel usuarioModel)
        {

            try
            {
                if (!ModelState.IsValid)
                    return View("CrearEditarUsuario", usuarioModel);
                await _usuarioService.CrearUsuario(usuarioModel.Usuario);
                return Json(new { redirectToUrl = @Url.Action("Index", "Usuario", new { area = "abm" }) });
                //return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
                //Response.StatusCode = 500;
                ////Response.TrySkipIisCustomErrors = true;
                //return Json(new { message = ex.Message });
            }
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(UsuarioModel usuarioModel)
        {
            if (!ModelState.IsValid)
                return View("CrearEditarUsuario", usuarioModel);

            await _usuarioService.EditarUsuario(usuarioModel.Usuario);
            return Json(new { redirectToUrl = @Url.Action("Index", "Usuario", new { area = "abm" }) });
            //return RedirectToAction("Index");
        }

        public async Task<IActionResult> Eliminar(int idUsuario)
        {
            var result = await _usuarioService.EliminarUsuario(idUsuario);
            return Ok(result);
        }
    }
}
