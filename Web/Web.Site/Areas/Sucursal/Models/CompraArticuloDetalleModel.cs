using Core.Dominio.AggregatesModel;

namespace Web.Site.Areas
{
    public class CompraArticuloDetalleModel
    {
        public int IdArticulo { get; set; }
        public string ArticuloNombre { get; set; }
        public int IdProveedor { get; set; }
        public string ProveedorNombre { get; set; }
        public int Cantidad { get; set; }
        public double Precio { get; set; }
        public string Comentario { get; set; }
    }
}
