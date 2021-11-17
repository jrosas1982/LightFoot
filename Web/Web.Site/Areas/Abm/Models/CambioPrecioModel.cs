using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Site.Areas.Abm.Models
{
    public class CambioPrecioModel
    {
        public decimal Porcentaje { get; set; }
        public int TipCambio { get; set; }
        public string TipoPrecioAfectado { get; set; }
        public string Comentartio { get; set; }
        public string ArticulosAfectados { get; set; }

    }
}
