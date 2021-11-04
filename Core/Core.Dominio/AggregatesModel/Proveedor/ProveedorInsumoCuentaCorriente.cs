using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Dominio.SeedWork;

namespace Core.Dominio.AggregatesModel
{
    public class ProveedorInsumoCuentaCorriente : EntityBase
    {
        [Key]
        public int Id { get; set; }
        public int IdProveedor { get; set; }
        public int IdCompraInsumo { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal MontoPagado { get; set; }
        public TipoPago TipoPago { get; set; }


        [ForeignKey("IdProveedor")]
        public virtual Proveedor Proveedor { get; set; }

        [ForeignKey("IdCompraInsumo")]
        public virtual CompraInsumo CompraInsumo { get; set; }
    }
}
