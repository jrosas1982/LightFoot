using Core.Dominio.AggregatesModel;

namespace Web.Site.Areas.Fabrica
{
    public class CompraInsumoDetalleModel
    {
        //<th>Insumo</th>
        //<th>Proveedor</th>
        //<th>Cantidad</th>
        //<th>Precio</th>
        //<th>Acciones</th>

        public int IdInsumo { get; set; }
        public string InsumoNombre { get; set; }
        public int IdProveedor { get; set; }
        public string ProveedorNombre { get; set; }
        public int Cantidad { get; set; }
        public double Precio { get; set; }
        public string Comentario { get; set; }
        //public virtual CompraInsumoDetalle CompraInsumoDetalle { get; set; }
        //public virtual ProveedorInsumo ProveedorInsumo { get; set; }
        //public virtual Insumo Insumo { get; set; }
        //public virtual Proveedor Proveedor { get; set; }

        //public int Id { get; set; }
        //public int IdCompraInsumo { get; set; }
        //public int IdInsumo { get; set; }
        //public int IdProveedorSugerido { get; set; }
        //public decimal Monto { get; set; }
        //public double Cantidad { get; set; }
        //public string Comentario { get; set; }

        //public virtual CompraInsumo CompraInsumo { get; set; }
        //public virtual Insumo Insumo { get; set; }
        //public virtual Proveedor Proveedor { get; set; }
    }
}
