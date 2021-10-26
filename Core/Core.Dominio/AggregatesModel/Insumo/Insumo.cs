using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Dominio.SeedWork;

namespace Core.Dominio.AggregatesModel
{
    public class Insumo : EntityBase
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        [Required]
        public string Unidad { get; set; }
        public int? IdProveedorPreferido { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public double StockTotal { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public double StockReservado { get; set; }


        [ForeignKey("IdProveedorPreferido")]
        public virtual Proveedor Proveedor { get; set; }

        public virtual ICollection<ProveedorInsumoHistorico> InsumoHistorico { get; set; } = new HashSet<ProveedorInsumoHistorico>();
    }
}

