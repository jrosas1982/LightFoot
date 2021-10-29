using System.Collections.Generic;
using Core.Dominio.AggregatesModel;
using Web.Site.Dtos;

namespace Web.Site.Areas
{
    public class ProveedorInsumoModel
    {

        public int Id { get; set; }
        public int IdProveedor { get; set; }
        public int IdInsumo { get; set; }
        public decimal Precio { get; set; }
        public Proveedor Proveedor { get; set; }
        public Ilist<ProveedorInsumo> ProveedoresInsumos { get; set; }
        public IEnumerable<Insumo> Insumos { get; set; }
        public Insumo Insumo { get; set; }

    }
}
