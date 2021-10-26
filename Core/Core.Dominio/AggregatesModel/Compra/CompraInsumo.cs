using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Core.Dominio.SeedWork;


namespace Core.Dominio.AggregatesModel
{
    public class CompraInsumo : EntityBase
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

        public virtual ICollection<CompraInsumoDetalle> CompraInsumoDetalles { get; set; } = new HashSet<CompraInsumoDetalle>();
    }
    //   CompraInsumo(Id, IdPedido, IdInsumo, Monto, Cantidad, Recibido)
}
