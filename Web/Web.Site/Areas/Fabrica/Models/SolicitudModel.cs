using Core.Dominio.AggregatesModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Site.Dtos;

namespace Web.Site.Areas
{
    public class SolicitudModel : Solicitud
    {
        public IEnumerable<EstadoSolicitud> EstadoSolicitudes { get; set; }      
        public IEnumerable<DesplegableModel> Sucursales { get; set; }
        public IEnumerable<SolicitudDetalle> SolicitudDetalle { get; set; } = new List<SolicitudDetalle>();

        public IEnumerable<SelectListItem> Articulos { get; set; }
        public IEnumerable<SelectListItem> Talles { get; set; }
        public IEnumerable<SelectListItem> Colores { get; set; }
        public SolicitudDetalle Detalle { get; set; }

        public SolicitudModel()
        {

        }

        public SolicitudModel(Solicitud solicitud, IEnumerable<EstadoSolicitud> estadoSolicitudes)
        {
            EstadoSolicitudes = estadoSolicitudes;

            Id = solicitud.Id;
            IdSucursal = solicitud.IdSucursal;
            Comentario = solicitud.Comentario;
        }
    }
}
