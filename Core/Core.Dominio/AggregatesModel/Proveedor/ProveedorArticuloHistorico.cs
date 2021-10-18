using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Dominio.SeedWork;

namespace Core.Dominio.AggregatesModel
{
    public class ProveedorArticuloHistorico : EntityBase
    {
        [Key]
        public int Id { get; set; }
        public int IdProveedor { get; set; }
        public int IdArticulo { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Precio { get; set; }

        [ForeignKey("IdProveedor")]
        public virtual Proveedor Proveedor { get; set; }

        [ForeignKey("IdArticulo")]
        public virtual Articulo Articulo { get; set; }

    }

    //ProveedorArticuloHistorico(Id, IdProveedor, IdArticulo, Precio)
}
