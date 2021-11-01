using System;
using System.Collections.Generic;
using Core.Dominio.AggregatesModel;

namespace Web.Site.Areas
{
    public class OrdenProduccionFilter
    {
        public string Articulo { get; set; }
        public EstadoOrdenProduccion[] EstadosOrden { get; set; }
        public int[] IdEtapasOrden { get; set; }
        public EstadoEtapaOrdenProduccion[] EstadosEtapa { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
    }
}
