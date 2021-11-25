using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Dominio.SeedWork;


namespace Core.Dominio.AggregatesModel
{
    public class ArticuloHistorico : EntityBase
    {
        [Key]
        public int Id { get; set; }
        public int IdArticulo { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecioMinorista { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecioMayorista { get; set; }

        [ForeignKey("IdArticulo")]
        public virtual Articulo Articulo { get; set; }
    }
}
