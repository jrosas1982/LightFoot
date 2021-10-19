using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Dominio.SeedWork;

namespace Core.Dominio.AggregatesModel
{
    public class Cliente : EntityBase
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CUIT { get; set; }
        [Required]
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Contacto { get; set; }
        [Required]
        public string Email { get; set; }
        public Boolean Activo { get; set; }

        public virtual ICollection<ClienteCuentaCorriente> ClienteCuentaCorriente { get; set; } = new HashSet<ClienteCuentaCorriente>();

    }
    //Cliente(Id, CUIT, Nombre, Direccion, Telefono, Contacto, Email, Activo)
}
