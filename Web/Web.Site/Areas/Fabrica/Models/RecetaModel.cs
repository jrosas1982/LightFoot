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
    
        public Boolean Activo { get; set; }
       public  RecetaDetalleModel RecetaDetalle { get; set; }

        public virtual IEnumerable<Receta> Recetas { get; set; }
        public virtual IEnumerable<RecetaDetalleModel> RecetaDetalles { get; set; }
        
        public RecetaModel()
        {
           // RecetaDetalles = new IEnumerable<RecetaDetalleModel>();
          // Recetas = new List<Receta>();
        }
        public RecetaModel(Receta receta , ICollection<RecetaDetalleModel> recetaDetalle)
        {
            Id = receta.Id;
            IdArticulo = receta.IdArticulo;
            Activo = receta.Activo;
            RecetaDetalles = recetaDetalle;
        }
    }
}
