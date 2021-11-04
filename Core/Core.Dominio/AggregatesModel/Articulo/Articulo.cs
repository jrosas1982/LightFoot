using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Dominio.SeedWork;

namespace Core.Dominio.AggregatesModel
{
    public class Articulo : EntityBase
    {
        [Key]
        public int Id { get; set; }        
        public ArticuloEstado ArticuloEstado { get; set; }
        [Display(Name = "Receta Artículo")]
        public int IdArticuloCategoria { get; set; }
        [Display(Name = "Código Artículo")]
        [Required]
        public string CodigoArticulo { get; set; }
        [Display(Name = "Talle Artículo")]
        [Required]
        public string TalleArticulo { get; set; }
        [Display(Name = "Nombre Artículo")]
        [Required]
        public string Nombre { get; set; }
        [Display(Name = "Descripción Artículo")]
        public string Descripcion { get; set; }
        [Display(Name = "Color Artículo")]
        [Required]
        public string Color { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        [Required]
        public decimal PrecioMinorista { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        [Required]
        public decimal PrecioMayorista { get; set; }

        public virtual ICollection<ProveedorArticuloHistorico> ArticuloHistorico { get; set; } = new HashSet<ProveedorArticuloHistorico>();
        public virtual ICollection<Receta> Recetas { get; set; } = new HashSet<Receta>();
        [ForeignKey("IdArticuloCategoria")]
        public virtual ArticuloCategoria ArticuloCategoria { get; set; }

        public string ToFullString()
        {
            return $"{CodigoArticulo} - {Nombre} - {Color} - {TalleArticulo}";
        }
    }

}