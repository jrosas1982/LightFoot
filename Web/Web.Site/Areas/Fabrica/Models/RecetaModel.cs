using Core.Dominio.AggregatesModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Site.Areas.Fabrica.Models
{
    public class RecetaModel 
    {
        public int Id { get; set; }
        [Display(Name = "Número Articulo")]
        [Required]
        public int IdArticulo { get; set; }
    
        public ArticuloModel Articulo { get; set; }

        public Boolean Activo { get; set; }
        public  RecetaDetalleModel RecetaDetalle { get; set; }

        public virtual IEnumerable<RecetaDetalleModel> RecetaDetalles { get; set; }

        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string CreadoPor { get; set; }
        public string ModificadoPor { get; set; }

        public RecetaModel()
        {
  
        }
        public RecetaModel(RecetaModel receta, IEnumerable<RecetaDetalleModel> recetaDetalle)
        {
            Id = receta.Id;
            IdArticulo = receta.IdArticulo;
            Activo = receta.Activo;
            RecetaDetalles = recetaDetalle;
        }
    }
}
