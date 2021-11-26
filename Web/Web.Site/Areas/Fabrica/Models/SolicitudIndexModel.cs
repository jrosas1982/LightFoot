using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Aplicacion.FIlters;
using Core.Dominio.AggregatesModel;

namespace Web.Site.Areas.Fabrica
{
    public class SolicitudIndexModel
    {
        public IEnumerable<Solicitud> Solicitudes { get; set; }
        [Display(Name = "Sucursal")]
        public IEnumerable<Sucursal> Sucursales { get; set; }
        public IEnumerable<EstadoSolicitud> EstadosSolicitud { get; set; }
        public SolicitudFilter SolicitudFilter { get; set; } = new SolicitudFilter();

    }
}
