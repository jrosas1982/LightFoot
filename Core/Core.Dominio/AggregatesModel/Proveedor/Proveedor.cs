using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Dominio.SeedWork;

namespace Core.Dominio.AggregatesModel
{
    public class Proveedor : EntityBase
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CUIT { get; set; }
        [Required]
        public string Nombre { get; set; }        
        [Required]
        public string Direccion { get; set; }
        [Required]
        public string Telefono { get; set; }
        [Required]
        public string Email { get; set; }
        public int Calificacion { get; set; }

        public virtual ICollection<ProveedorCuentaCorriente> ProvedorCuentaCorriente { get; set; } = new HashSet<ProveedorCuentaCorriente>();
        public virtual ICollection<ProveedorArticulo> ProveedorArticulos { get; set; } = new HashSet<ProveedorArticulo>();
        public virtual ICollection<ProveedorInsumo> ProveedorInsumos { get; set; } = new HashSet<ProveedorInsumo>();
        //Proveedor(Id, Nombre, Direccion, Telefono, CUIT, Email)

    }

}
