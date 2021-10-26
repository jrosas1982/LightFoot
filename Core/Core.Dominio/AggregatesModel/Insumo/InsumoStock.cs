using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Dominio.SeedWork;

namespace Core.Dominio.AggregatesModel
{
    public class InsumoStock : EntityBase
    {
        [Key]
        public int Id { get; set; }
        public int IdInsumo { get; set; }
        public int? IdProveedorPreferido { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public double StockTotal { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public double StockReservado { get; set; }

        [ForeignKey("IdInsumo")]
        public virtual Insumo Insumo { get; set; }

        [ForeignKey("IdProveedorPreferido")]
        public virtual Proveedor Proveedor { get; set; }


    }
    // InsumoStock(Id, IdInsumo, IdProveedorPreferido, CantidadStockTotal, CantidadStockReservada)
}
