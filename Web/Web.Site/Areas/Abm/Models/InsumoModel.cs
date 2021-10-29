using System.Collections.Generic;
using Core.Dominio.AggregatesModel;

namespace Web.Site.Areas
{
    public class InsumoModel
    {
        public Insumo Insumo { get; set; }
        public IEnumerable<Proveedor> Proveedores { get; set; }
    }
}
