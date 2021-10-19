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
        public int CantidadStockTotal { get; set; }
        public int CantidadStockReserva { get; set; }

        [ForeignKey("IdInsumo")]
        public virtual Insumo Insumo { get; set; }

        [ForeignKey("IdProveedorPreferido")]
        public virtual Proveedor Proveedor { get; set; }


    }
    // InsumoStock(Id, IdInsumo, IdProveedorPreferido, CantidadStockTotal, CantidadStockReservada)
}
