using System.Collections.Generic;
using Core.Dominio.AggregatesModel;
using Web.Site.Dtos;

namespace Web.Site.Areas.Abm
{
    public class ArticuloModel
    {
        public Articulo Articulo { get; set; }
        public IEnumerable<DesplegableModel> ArticuloEstados { get; set; }
        public IEnumerable<DesplegableModel> ArticuloCategorias { get; set; }
    }
}
