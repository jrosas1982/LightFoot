using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Site.Areas.Fabrica
{
    public class ArticuloModel
    {
        public int Id { get; set; }

        [Display(Name = "Receta Artículo")]
        public int? IdReceta { get; set; }
        [Display(Name = "Categoria Artículo")]
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
  
        public decimal PrecioMinorista { get; set; }

        public decimal PrecioMayorista { get; set; }
    }
}
