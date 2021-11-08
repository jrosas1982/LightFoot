using Core.Dominio.AggregatesModel;

namespace Web.Site.Areas.Abm
{
    public class ArticuloStockModel : ArticuloStock
    {
        public ArticuloStockModel(ArticuloStock articuloStock)
        {
            Id = articuloStock.Id;
            IdArticulo = articuloStock.IdArticulo;
            IdSucursal = articuloStock.IdSucursal;
            IdProveedorPreferido = articuloStock.IdProveedorPreferido;
            StockTotal = articuloStock.StockTotal;
            StockMinimo = articuloStock.StockMinimo;
            EsReposicionPorLote = articuloStock.EsReposicionPorLote;
            EsReposicionAutomatica = articuloStock.EsReposicionAutomatica;
            TamañoLote = articuloStock.TamañoLote;
        }
    }
}