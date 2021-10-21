using System.Collections.Generic;
using Core.Dominio.AggregatesModel;

namespace Web.Site.Areas
{
    public class OrdenProduccionDetalleModel
    {
        public OrdenProduccion OrdenProduccion { get; set; }
        public IEnumerable<EstadoOrdenProduccion> EstadosOrdenProduccion { get; set; }
        public IEnumerable<EtapaOrdenProduccion> EtapasOrdenProduccion { get; set; }
        public IEnumerable<EstadoEtapaOrdenProduccion> EstadosEtapaOrdenProduccion { get; set; }
        public IEnumerable<OrdenProduccionEvento> OrdenProduccionEventos { get; set; }
        public int NumeroDeEtapaActual { get; set; }
        public int NumeroTotalDeEtapas { get; set; }
        public int PorcentajeCompletado { get; set; }
    }
}
