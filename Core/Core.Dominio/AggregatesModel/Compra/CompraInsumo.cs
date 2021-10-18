using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Dominio.SeedWork;

namespace Core.Dominio.AggregatesModel
{
    public class CompraInsumo : EntityBase
    {
        [Key]
        public int Id { get; set; }
        public int IdCompra { get; set; }
        public int IdInsumo { get; set; }
        public int Cantidad { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal MontoTotal { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal MontoRecibido { get; set; }


        [ForeignKey("IdCompra")]
        public virtual Compra Compra { get; set; }

        [ForeignKey("IdArticulo")]
        public virtual Insumo Insumo { get; set; }

    }
    //   CompraInsumo(Id, IdPedido, IdInsumo, Monto, Cantidad, Recibido)
}
