using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Dominio.SeedWork;

namespace Core.Dominio.AggregatesModel
{
    public class CajaSucursal : EntityBase
    {
        [Key]
        public int Id { get; set; }
        public int IdSucursal { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Monto { get; set; }
        public string Comentario { get; set; }      

        [ForeignKey("IdSucursal")]
        public virtual Sucursal Sucursal { get; set; }

        [NotMapped]
        public string TipoMovimiento
        {
            get
            {
                if (Monto > 0)
                    return "Ingreso";
                else
                    return "Egreso";
            }
        }
    }
}
