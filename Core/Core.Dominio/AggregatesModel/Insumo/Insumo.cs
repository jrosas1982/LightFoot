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
        [Display (Name = "Nombre Insumo")]
        public string Nombre { get; set; }
        [Display(Name = "Descripción Insumo")]
        public string Descripcion { get; set; }
        [Required]
        [Display(Name = "Unidad de medida")]
        public string Unidad { get; set; }
        [Display(Name = "Proveedor Preferido")]
        public int? IdProveedorPreferido { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Stock")]
        public double StockTotal { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public double StockReservado { get; set; }

         
        [ForeignKey("IdProveedorPreferido")]
        public virtual Proveedor Proveedor { get; set; }
        public virtual ICollection<ProveedorInsumo> ProveedoresInsumo { get; set; } = new HashSet<ProveedorInsumo>();

        public virtual ICollection<ProveedorInsumoHistorico> ProveedoresInsumoHistorico { get; set; } = new HashSet<ProveedorInsumoHistorico>();
    }
}

