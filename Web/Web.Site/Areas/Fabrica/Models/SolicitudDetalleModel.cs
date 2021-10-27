using System.Collections.Generic;
using Core.Dominio.AggregatesModel;
using Web.Site.Dtos;

namespace Web.Site.Areas
{
    public class SolicitudDetalleModel
    {
        public Solicitud Solicitud { get; set; }
        public Sucursal Sucursal { get; set; }
        public IEnumerable<SolicitudDetalle> SolicitudDetalle { get; set; }
    }
}