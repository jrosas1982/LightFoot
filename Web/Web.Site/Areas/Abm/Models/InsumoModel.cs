using System.Collections.Generic;
using Core.Dominio.AggregatesModel;

namespace Web.Site.Areas.Abm
{
    public class InsumoModel
    {
        public Insumo Insumo { get; set; }
        public IEnumerable<Proveedor> Proveedores { get; set; }
    }
}
