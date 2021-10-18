using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Dominio.SeedWork;

namespace Core.Dominio.AggregatesModel
{
    public class CompraArticulo : EntityBase
    {
        [Key]
        public int Id { get; set; }
        public int IdCompra { get; set; }
        public int IdArticulo { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Monto { get; set; }
        public int Cantidad { get; set; }
        public bool Recibido { get; set; }

        [ForeignKey("IdCompra")]
        public virtual Compra Compra { get; set; }

        [ForeignKey("IdArticulo")]
        public virtual Articulo Articulo { get; set; }
    }
    //    CompraArticulo(Id, IdCompra, IdArticulo, Monto, Cantidad, Recibido)

}
