using Core.Dominio.AggregatesModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Site.Dtos;

namespace Web.Site.Areas
{
    public class CompraArticuloModel
    {
        public IEnumerable<CompraArticuloDetalleModel> CompraArticuloDetalleModels { get; set; }
        //public CompraInsumo CompraInsumo { get; set; }
        public IEnumerable<SelectListItem> Insumos { get; set; }
        public IEnumerable<SelectListItem> Proveedores { get; set; }
        //public CompraInsumoDetalleModel CompraInsumoDetalleModel { get; set; }

    }
}
