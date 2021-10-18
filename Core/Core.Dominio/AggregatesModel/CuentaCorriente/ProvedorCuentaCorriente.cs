using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Dominio.SeedWork;

namespace Core.Dominio.AggregatesModel
{
    public class ProvedorCuentaCorriente : EntityBase
    {
        [Key]
        public int Id { get; set; }
        public int IdProveedor { get; set; }
        public int IdCompra { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal MontoPagado { get; set; }
        public TipoPagoCuentaCorriente TipoPago { get; set; }


        [ForeignKey("IdProveedor")]
        public virtual Proveedor Proveedor { get; set; }

        [ForeignKey("IdCompra")]
        public virtual Compra Compra { get; set; }


    }
    //ProvedorCuentaCorriente (Id, IdProveedor, IdPedido, MontoPagado, TipoPago)
}
