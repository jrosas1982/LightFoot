using Core.Dominio.AggregatesModel;

namespace Web.Site.Areas.Fabrica.Models
{
    public class CompraInsumoDetalleModel
    {
        public int Id { get; set; }
        public int IdCompraInsumo { get; set; }
        public int IdInsumo { get; set; }
        public int IdProveedorSugerido { get; set; }
        public decimal Monto { get; set; }
        public double Cantidad { get; set; }
        public string Comentario { get; set; }

        public virtual CompraInsumo CompraInsumo { get; set; }
        public virtual Insumo Insumo { get; set; }
        public virtual Proveedor Proveedor { get; set; }
    }
}
