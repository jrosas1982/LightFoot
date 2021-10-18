using System.ComponentModel.DataAnnotations;

namespace Core.Aplicacion.Auth
{
    public class UserLoginDTO
    {
        [Display(Name = "Nombre de Usuario")]
        [Required(ErrorMessage = "El Nombre de Usuario es obligatorio")]
        public string NombreUsuario { get; set; }
        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "Campo Contraseña es obligatorio")]
        public string Contraseña { get; set; }
        [Display(Name = "Sucursal")]
        public int IdSucursal { get; set; }
        public bool Recuerdame { get; set; }
    }
}
