using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Dominio.SeedWork;

namespace Core.Dominio.AggregatesModel
{
    public class ClienteCuentaCorriente : EntityBase
    {
        [Key]
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public int IdVenta { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal MontoPercibido { get; set; }
        public TipoPago TipoPago { get; set; }
        public bool PagoAcreditado { get; set; }


        [ForeignKey("IdCliente")]
        public virtual Cliente Cliente { get; set; }

        [ForeignKey("IdVenta")]
        public virtual Venta Venta { get; set; }
    }

    //ClienteCuentaCorriente(Id, IdCliente, IdVenta, MontoPercibido, TipoPago, PagoAcreditado)
}
