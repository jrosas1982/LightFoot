using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.AplicacionTests.V1
{
    [TestClass()]
    public class LoginControllerTests
    {
        [TestMethod()]
        public async Task LoginTestAsync()
        {
            //// Arrange
            //var userDto = new UserLoginDTO()
            //{
            //    NombreUsuario = "Maxissc",
            //    Contraseña = "123123",
            //    IdSucursal = 1,
            //    Recuerdame = true
            //};

            //var sucursalService = new Mock<ISucursalService>();

            //var userLoginService = new Mock<IUserLoginService>();
            //userLoginService.Setup(repo => repo.ValidarUsuario(userDto)).ReturnsAsync(new Usuario() { NombreUsuario = "maxiUser", Email = "123123@asd.com", UsuarioRol = UsuarioRol.Administrador });

            //var controller = new LoginController(sucursalService.Object, userLoginService.Object);

            //var ret = await controller.Login();

            //Assert.IsNotNull(ret);
        }
    }
}