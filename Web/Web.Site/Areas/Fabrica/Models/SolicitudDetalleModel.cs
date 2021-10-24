using System.Collections.Generic;
using Core.Dominio.AggregatesModel;
using Web.Site.Dtos;

namespace Web.Site.Areas
{
    public class SolicitudDetalleModel
    {
        public Solicitud Solicitud { get; set; }
        public IEnumerable<Sucursal> Sucursales { get; set; }
        public IEnumerable<EstadoSolicitud> EstadosSolicitud { get; set; }
        public IEnumerable<DesplegableModel> Articulos{ get; set; }
    }
}