using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Site.Areas.Fabrica.Models
{
    public class InsumoModel
    {
        public int Id { get; set; }

        [Display(Name = "NombreInsumo")]
        public string Nombre { get; set; }
        [Display(Name = "Descripción Insumo")]
        public string Descripcion { get; set; }

        [Display(Name = "Unidad de medida")]
        public string Unidad { get; set; }
        public int? IdProveedorPreferido { get; set; }
    
        public double StockTotal { get; set; }
   
        public double StockReservado { get; set; }
    }
}
