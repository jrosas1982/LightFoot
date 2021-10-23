using System.Collections.Generic;
using Core.Dominio.AggregatesModel;

namespace Web.Site.Areas
{
    public class SolicitudDetalleModel
    {
        public IEnumerable<Solicitud> Solicitudes { get; set; }
        public IEnumerable<Sucursal> Sucursales { get; set; }
        public IEnumerable<EstadoSolicitud> EstadosSolicitud { get; set; }

    }
}