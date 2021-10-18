using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Dominio.AggregatesModel
{
    public class ProveedorInsumo
    {
        [Key]
        public int Id { get; set; }
        public int IdProveedor { get; set; }
        public int IdInsumo { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Precio { get; set; }

        // ProveedorInsumo (Id, IdProveedor, IdInsumo, Precio)

        [ForeignKey("IdProveedor")]
        public virtual Proveedor Proveedor { get; set; }

        [ForeignKey("IdInsumo")]
        public virtual Insumo Insumo { get; set; }
    }
}
