using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Dominio.AggregatesModel;

namespace Web.Site.Areas
{
    public class SolicitudFilter
    {
        public IEnumerable<int> IdSucursal { get; set; }
        public IEnumerable<EstadoSolicitud> EstadoSolicitud { get; set; }
        public DateTime FechaDesde { get; set; } = DateTime.Today - TimeSpan.FromDays(7);
        public DateTime FechaHasta { get; set; } = DateTime.Today;
    }
}
