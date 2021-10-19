using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Core.Dominio.SeedWork;

namespace Core.Dominio.AggregatesModel
{
    public class Receta : EntityBase
    {
        [Key]
        public int Id { get; set; }
        public int IdArticulo { get; set; }
        public int IdInsumo { get; set; }
        public Boolean Activo { get; set; }

        [ForeignKey("IdArticulo")]
        public virtual Articulo Articulo { get; set; }

        [ForeignKey("IdInsumo")]
        public virtual Insumo Insumo { get; set; }
        public virtual ICollection<RecetaDetalle> RecetaDetalles { get; set; } = new HashSet<RecetaDetalle>();
    }
}
