using Core.Dominio.AggregatesModel;

namespace Web.Site.Areas.Fabrica
{
    public class CompraInsumoProveedorModel
    {
        public Proveedor ProveedorSugerido { get; set; }
        public Proveedor ProveedorPreferido { get; set; }
    }
}
