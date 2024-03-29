﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Aplicacion.Auth;
using Core.Aplicacion.Helpers;
using Core.Aplicacion.Hubs;
using Core.Aplicacion.Interfaces;
using Core.Dominio;
using Core.Dominio.AggregatesModel;
using Core.Infraestructura;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Core.Aplicacion.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly AppDbContext _db;
        private readonly ILogger<UsuarioService> _logger;
        private readonly PasswordHasher _PasswordHasher; 
        private readonly IHubContext<NotificationsHub> _hubContext;
        public UsuarioService(AppDbContext db, ILogger<UsuarioService> logger, PasswordHasher passwordHasher, IHubContext<NotificationsHub> hubContext)
        {
            _db = db;
            _logger = logger;
            _PasswordHasher = passwordHasher;
            _hubContext = hubContext;
        }

        public async Task<IEnumerable<Usuario>> GetUsuarios()
        {
            var usuariosList = _db.Usuarios
                .Where(x => !x.Eliminado)
                .OrderByDescending(x => x.NombreUsuario);

            var asd = usuariosList.ToString();
            await _hubContext.Clients.All.SendAsync($"GetUsuarios", $"Se solicito la lista de usuarios");
            return await usuariosList.ToListAsync();
        }

        public async Task<Usuario> BuscarPorId(int IdUsuario)
        {
            var usuario = await _db.Usuarios.FindAsync(IdUsuario);
            return usuario;
        }

        public async Task CrearUsuario(Usuario usuario)
        {
            if (_db.Usuarios.Any(x => x.Eliminado == false && x.NombreUsuario == usuario.NombreUsuario))
                throw new ExcepcionControlada($"El nombre de usuario {usuario.NombreUsuario} ya existe");

            if (string.IsNullOrEmpty(usuario.Contraseña))
                throw new ExcepcionControlada($"La contraeña no puede estar vacia");

            usuario.Contraseña = _PasswordHasher.HashPassword(usuario.Contraseña);

            _db.Add(usuario);
            await _db.SaveChangesAsync();

            await _hubContext.Clients.All.SendAsync("UsuarioCreado");

            _logger.LogInformation($"Se creó el usuario Username: {usuario.NombreUsuario}, Id: {usuario.Id}");
        }

        public async Task EditarUsuario(Usuario usuario)
        {
            var usuarioDb = await _db.Usuarios.FindAsync(usuario.Id);

            usuarioDb.Nombre = usuario.Nombre;

            if (!string.IsNullOrEmpty(usuario.Contraseña))
                usuarioDb.Contraseña = _PasswordHasher.HashPassword(usuario.Contraseña);

            usuarioDb.Apellido = usuario.Apellido;
            usuarioDb.DNI = usuario.DNI;
            usuarioDb.Email = usuario.Email;
            usuarioDb.Direccion = usuario.Direccion;
            usuarioDb.Activo = usuario.Activo;
            usuarioDb.UsuarioRol = usuario.UsuarioRol;

            _db.Update(usuarioDb);
            await _db.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync($"UsuarioEditado", $"El usuario {_db.GetUsername()} edito el usuario {usuarioDb.NombreUsuario}", usuario.Id);
        }

        public async Task<bool> EliminarUsuario(int idUsuario)
        {
            try
            {
                var usuarioDb = await _db.Usuarios.FindAsync(idUsuario);

                if (usuarioDb.NombreUsuario == "super")
                    throw new ExcepcionControlada("No se puede eliminar el usuario super");

                usuarioDb.Eliminado = true;

                _db.Usuarios.Update(usuarioDb);
                await _db.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<UsuarioRol>> GetUsuarioRoles()
        {
            return await Task.FromResult(EnumExtensions.GetValues<UsuarioRol>());
        }

    }
}
