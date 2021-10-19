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
        public int IdReceta { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Unidad { get; set; }

        [ForeignKey("IdReceta")]
        public virtual Receta Receta { get; set; }
        public virtual ICollection<ProveedorInsumoHistorico> InsumoHistorico { get; set; }
    }
}
