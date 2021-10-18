using System;
using System.ComponentModel.DataAnnotations;
using Core.Dominio.SeedWork;

namespace Core.Dominio.AggregatesModel
{
    public class UsuarioApi : EntityBase
    {
        public UsuarioApi()
        {
            Activo = true;
            ApiKey = Guid.NewGuid().ToString("N");
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string ApiKey { get; set; }
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }
        public Boolean Activo { get; set; }

    }
}
