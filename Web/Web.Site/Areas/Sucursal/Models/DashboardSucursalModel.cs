using Core.Dominio.AggregatesModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Site.Dtos;

namespace Web.Site.Areas
{
    public class DashboardSucursalModel
    {
        public Sucursal Sucursal { get; set; }
        public IEnumerable<CajaSucursal> Movimientos { get; set; }
        public IEnumerable<ArticuloStock> StockBajo { get; set; }
        public IEnumerable<Venta> UltimasVentas { get; set; }
        public IEnumerable<Tuple<Articulo, int>> MasVendidos { get; set; }
    }
}
