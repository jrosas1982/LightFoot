using System.Collections.Generic;
using Core.Dominio.AggregatesModel;
using Web.Site.Dtos;

namespace Web.Site.Areas
{
    public class ProveedorInsumoModel
    {
        public Proveedor Proveedor { get; set; }
        public IEnumerable<ProveedorInsumo> ProveedoresInsumo { get; set; }
        public IEnumerable<Insumo> Insumos { get; set; }

    }
}
