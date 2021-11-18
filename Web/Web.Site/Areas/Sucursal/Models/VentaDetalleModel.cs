using Core.Dominio.AggregatesModel;

namespace Web.Site.Areas
{
    public class VentaDetalleModel
    {
        public int IdArticulo { get; set; }
        public string ArticuloCategoria { get; set; }
        public string CodigoArticulo { get; set; }
        public string NombreArticulo { get; set; }
        public string DescripcionArticulo { get; set; }
        public string ColorArticulo { get; set; }
        public string TalleArticulo { get; set; }
        public int Cantidad { get; set; }
        public double Precio { get; set; }
        public string Comentario { get; set; }
    }
}