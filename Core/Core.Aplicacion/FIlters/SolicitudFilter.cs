using System;
using System.Collections.Generic;
using System.Text;
using Core.Dominio.AggregatesModel;

namespace Core.Aplicacion.FIlters
{
    public class SolicitudFilter
    {
        public IEnumerable<int> IdSucursalList { get; set; } = new List<int>();
        public IEnumerable<EstadoSolicitud> EstadoSolicitudList { get; set; } = new List<EstadoSolicitud>();
        public DateTime FechaDesde { get; set; } = DateTime.Today - TimeSpan.FromDays(-30);
        public DateTime FechaHasta { get; set; } = DateTime.Today;
    }
}
