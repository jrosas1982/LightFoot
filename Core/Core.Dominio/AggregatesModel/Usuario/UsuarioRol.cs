using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Core.Dominio.AggregatesModel
{
    public enum UsuarioRol : int
    {
        [Category("Admin")]
        [Display(GroupName = "Admin")]
        Administrador = 40,

        [Category("Fabrica")]
        [Display(GroupName = "Fabrica")]
        Gerente = 30,
        [Category("Fabrica")]
        [Display(GroupName = "Fabrica")]
        Supervisor = 20,
        [Category("Fabrica")]
        [Display(GroupName = "Fabrica")]
        Controlador = 80,
        [Category("Fabrica")]
        [Display(GroupName = "Fabrica")]
        Operario = 50,

        [Category("Sucursal")]
        [Display(GroupName = "Sucursal")]
        Encargado = 60,
        [Category("Sucursal")]
        [Display(GroupName = "Sucursal")]
        Cajero = 70,
        [Category("Sucursal")]
        [Display(GroupName = "Sucursal")]
        Vendedor = 10,
    }
}
