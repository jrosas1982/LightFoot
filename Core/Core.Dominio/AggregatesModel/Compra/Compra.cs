using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Dominio.SeedWork;

namespace Core.Dominio.AggregatesModel
{
    public class Compra : EntityBase
    {
        [Key]
        public int Id { get; set; }
        public int IdSucursal { get; set; }
        public int IdProveedor { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal MontoTotal { get; set; }


        [ForeignKey("IdSucursal")]
        public virtual Sucursal Sucursal { get; set; }

        [ForeignKey("IdProveedor")]
        public virtual Proveedor Proveedor { get; set; }

        public virtual ICollection<CompraArticulo> CompraArticulos { get; set; }
        public virtual ICollection<CompraInsumo> CompraInsumos { get; set; }
    }
    //Compra(Id, IdSucursal, IdProveedor, MontoTotal)
}
