using Core.Dominio.AggregatesModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Site.Areas
{
    public class DashboardFabricaModel
    {
        //public Sucursal Sucursal { get; set; }
        public IEnumerable<Solicitud> Solicitudes { get; set; }
        public IEnumerable<DashboardRankingSucursales> SolicitudRanking { get; set; }
        public IEnumerable<OrdenProduccion> Ordenes { get; set; }
        public IEnumerable<Venta> UltimasVentas { get; set; }
        public IEnumerable<Tuple<Articulo, int>> MasVendidos { get; set; }
        public IEnumerable<Insumo> InsumosBajoStock { get; set; }
        
    }
    public class DashboardRankingSucursales
    { 
        public Sucursal Sucursal { get; set; }
        public Solicitud Solicitud { get; set; }
        public int Cantidad { get; set; }


    }
}
