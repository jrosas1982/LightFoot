using Core.Dominio.AggregatesModel;

namespace Web.Site.Areas
{
    public class InsumoStockModel : InsumoStock
    {
        public InsumoStockModel(InsumoStock insumoStock)
        {
            Id = insumoStock.Id;
            IdInsumo = insumoStock.IdInsumo;
            IdProveedorPreferido = insumoStock.IdProveedorPreferido;
            CantidadStockTotal = insumoStock.CantidadStockTotal;
            CantidadStockReserva = insumoStock.CantidadStockReserva;
        }
    }
}