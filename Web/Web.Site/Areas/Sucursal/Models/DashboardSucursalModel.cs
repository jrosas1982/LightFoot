using Core.Dominio.AggregatesModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Site.Dtos;

namespace Web.Site.Areas
{
    public class DashboardSucursalModel
    {
        public Sucursal Sucursal { get; }
        public IEnumerable<CajaSucursal> Movimientos { get; }
        public IEnumerable<ArticuloStock> StockBajo { get; }
        public IEnumerable<CompraArticulo> UltimasCompras { get; }
        public IEnumerable<Articulo> MasVendidos { get; }
    }
}
