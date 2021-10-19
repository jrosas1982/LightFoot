using System;
using System.ComponentModel.DataAnnotations;
using Core.Dominio.SeedWork;

namespace Core.Dominio.AggregatesModel
{
    public class Usuario : EntityBase
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Rol")]
        public UsuarioRol UsuarioRol { get; set; }
        [Required(ErrorMessage ="El nombre de usuario es un campo requerido")]
        [Display(Name = "Nombre de Usuario")]
        public string NombreUsuario { get; set; }
        [Required]
        public string Contraseña { get; set; }
        [Required(ErrorMessage = "hola")]
        public string DNI { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public Boolean Activo { get; set; }
    }
}
