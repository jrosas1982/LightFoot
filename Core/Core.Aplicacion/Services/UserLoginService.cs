using System;
using System.Threading.Tasks;
using Core.Aplicacion.Auth;
using Core.Aplicacion.Interfaces;
using Core.Dominio.AggregatesModel;
using Core.Infraestructura;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Core.Aplicacion.Services
{
    public class UserLoginService : IUserLoginService
    {
        private readonly AppDbContext _db;
        private readonly ILogger<UserLoginService> _logger;
        private readonly PasswordHasher _PasswordHasher;

        public UserLoginService(AppDbContext db, ILogger<UserLoginService> logger, PasswordHasher passwordHasher)
        {
            _db = db;
            _logger = logger;
            _PasswordHasher = passwordHasher;
        }

        public async Task<Usuario> ValidarUsuario(UserLoginDTO usuario)
        {
            try
            {
                Usuario userDb = await _db.Usuarios.SingleOrDefaultAsync(x => x.NombreUsuario == usuario.NombreUsuario);

                if (userDb == null)
                    throw new Exception("Problemas al ingresar al sistema.");

                if (usuario.Contraseña.Equals(userDb.Contraseña))
                {
                    userDb.Contraseña = _PasswordHasher.HashPassword(userDb.Contraseña);
                    _db.Usuarios.Update(userDb);
                    await _db.SaveChangesAsync();
                }
                else if (!_PasswordHasher.VerifyHashedPassword(userDb.Contraseña, usuario.Contraseña).Equals(PasswordVerificationResult.Success))
                {
                    _logger.LogInformation($"El usuario {userDb.NombreUsuario}, Id: {userDb.Id} ingreso su contraseña incorrectamente.");
                    throw new Exception("Problemas al ingresar al sistema.");
                }

                if (!userDb.Activo)
                    throw new Exception($"La cuenta {userDb.NombreUsuario} se encuentra desactivada. Pida a un Administrador que la reactive o comuniquese con sistemas");

                Sucursal sucursal = await _db.Sucursales.FindAsync(usuario.IdSucursal);

                if (!sucursal.Activo)
                    throw new Exception($"La sucursal {sucursal.Nombre} encuentra desactivada. Pida a un Administrador que la reactive o comuniquese con sistemas");

                return userDb;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{ex.Message}, Usuario: {usuario.NombreUsuario}.");
                throw ex;
            }
        }
    }
}
