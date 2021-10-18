using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Dominio.SeedWork;

namespace Core.Dominio.AggregatesModel
{
    public class VentaDetalle : EntityBase
    {
        [Key]
        public int Id { get; set; }
        public int IdVenta { get; set; }
        public int IdArticulo { get; set; }
        public int Cantidad { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal MontoTotal { get; set; }


        [ForeignKey("IdVenta")]
        public virtual Venta Venta { get; set; }

        [ForeignKey("IdArticulo")]
        public virtual Articulo Articulo { get; set; }
    }
    //VentaDescripcion (Id, IdVenta, IdArticulo, Monto, Cantidad)
}
