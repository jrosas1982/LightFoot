using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Site.Areas.Fabrica
{
    public class RecetaDetalleModel
    {
        public int Id { get; set; }
        [Display(Name ="Nombre Receta")]
        public int IdReceta { get; set; }

        [Display(Name = "Nombre Insumo")]
        public int IdInsumo { get; set; }
        [Display(Name = "Nombre Orden")]
        public int IdEtapaOrdenProduccion { get; set; }
        public double Cantidad { get; set; }
        public string Comentario { get; set; }
        [Display(Name = "Nombre Insumo")]
        public string NombreInsumo { get; set; }

        public string UnidadDeMedida { get; set; }
        public string NombreEtapaOrdenProduccion { get; set; }
    
    }
}
