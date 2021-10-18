using Core.Dominio.AggregatesModel;

namespace Web.Site.Areas
{
    public class InsumoModel : Insumo
    {
        public InsumoModel(Insumo insumos)
        {
            Id = insumos.Id;
            Nombre = insumos.Nombre;
            Descripcion = insumos.Descripcion;
            Unidad = insumos.Unidad;
        }
    }
}
