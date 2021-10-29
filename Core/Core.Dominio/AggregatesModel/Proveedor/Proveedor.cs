using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [Column(TypeName = "decimal(18,2)")]
        public double Calificacion { get; set; }

        public virtual ICollection<ProveedorArticuloCuentaCorriente> ProvedorCuentaCorriente { get; set; } = new HashSet<ProveedorArticuloCuentaCorriente>();
        public virtual ICollection<ProveedorArticulo> ProveedorArticulos { get; set; } = new HashSet<ProveedorArticulo>();
        public virtual ICollection<ProveedorInsumo> ProveedorInsumos { get; set; } = new HashSet<ProveedorInsumo>();
        //Proveedor(Id, Nombre, Direccion, Telefono, CUIT, Email)

    }

}
