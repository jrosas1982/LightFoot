using Core.Dominio.AggregatesModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Site.Dtos;

namespace Web.Site.Areas
{
    public class VentaModel
    {
        public Cliente Cliente { get; set; }
        public VentaTipo VentaTipo { get; set; }
        public decimal DescuentoRealizado { get; set; }
        public IEnumerable<VentaTipo> VentaTipos { get; set; }
        public IEnumerable<VentaDetalleModel> VentaDetalleModels { get; set; }
        public IEnumerable<SelectListItem> Articulos { get; set; }

    }
}
