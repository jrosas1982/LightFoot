using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Dominio.SeedWork;

namespace Core.Dominio.AggregatesModel
{
    public class Solicitud : EntityBase
    {
        [Key]
        public int Id { get; set; }
        public int IdSucursal { get; set; }
        public EstadoSolicitud EstadoSolicitud { get; set; } = EstadoSolicitud.PendienteAprobacion;
        public string Comentario { get; set; }

        [ForeignKey("IdSucursal")]
        public virtual Sucursal Sucursal { get; set; }
        public virtual ICollection<SolicitudDetalle> SolicitudDetalles { get; set; } = new HashSet<SolicitudDetalle>();
        public virtual ICollection<SolicitudEvento> SolicitudEventos { get; set; } = new HashSet<SolicitudEvento>();
    }
}
