using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Dominio.SeedWork;

namespace Core.Dominio.AggregatesModel
{
    public class Proveedor : EntityBase
    {
        [Key]
        public int Id { get; set; }
        public string CUIT { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }

        public virtual ICollection<ProvedorCuentaCorriente> ProvedorCuentaCorriente { get; set; }
        public virtual ICollection<ProveedorArticulo> ProveedorArticulos { get; set; }
        public virtual ICollection<ProveedorInsumo> ProveedorInsumos { get; set; }
        //Proveedor(Id, Nombre, Direccion, Telefono, CUIT, Email)

    }

}
