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
        public int? IdReceta { get; set; }
        [Display (Name = "Categoria Artículo")]
        public int IdArticuloCategoria { get; set; }
        [Display(Name = "Código Artículo")]
        public string CodigoArticulo { get; set; }
        [Display(Name = "Talle Artículo")]
        public string TalleArticulo { get; set; }
        [Display(Name = "Nombre Artículo")]
        public string Nombre { get; set; }
        [Display(Name = "Descripción Artículo")]
        public string Descripcion { get; set; }
        [Display(Name = "Color Artículo")]
        public string Color { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecioMinorista { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecioMayorista { get; set; }

        public virtual ICollection<ProveedorArticuloHistorico> ArticuloHistorico { get; set; }
        [ForeignKey("IdArticuloCategoria")]
        public virtual ArticuloCategoria ArticuloCategoria { get; set; }
        [ForeignKey("IdReceta")]
        public virtual Receta Receta { get; set; }

    }
}