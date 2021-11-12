using Core.Dominio.AggregatesModel;

namespace Web.Site.Areas.Sucursal_
{
    public class CompraArticuloDetalleModel
    {
        public int IdInsumo { get; set; }
        public string InsumoNombre { get; set; }
        public int IdProveedor { get; set; }
        public string ProveedorNombre { get; set; }
        public int Cantidad { get; set; }
        public double Precio { get; set; }
        public string Comentario { get; set; }
    }
}
