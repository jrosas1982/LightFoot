using Core.Dominio.AggregatesModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Site.Areas
{
    public class DashboardFabricaModel
    {
        public DashbardFabricaInfoGeneralModel DashbardFabricaInfoGeneralModel { get; set; }
        public IEnumerable<Tuple<Sucursal, int>> TopSucursalesSolicitudes { get; set; }
        public IEnumerable<Insumo> InsumosBajoStock { get; set; }   
        //public IEnumerable<Tuple<EtapaOrdenProduccion, int>> AvanceProduccion { get; set; }
        public string[] Etapas { get; set; }
        public int[] Cantidad { get; set; }
    }

}
