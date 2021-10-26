using System.Collections.Generic;
using Core.Dominio.AggregatesModel;

namespace Web.Site.Areas
{
    public class OrdenProduccionIndexModel
    {
        public IEnumerable<OrdenProduccion> OrdenProducciones { get; set; }
        public IEnumerable<EstadoOrdenProduccion> EstadoOrdenProducciones { get; set; }
        public IEnumerable<EtapaOrdenProduccion> EtapaOrdenProducciones { get; set; }
        public IEnumerable<EstadoEtapaOrdenProduccion> EstadoEtapaOrdenProducciones { get; set; }


    }
}
