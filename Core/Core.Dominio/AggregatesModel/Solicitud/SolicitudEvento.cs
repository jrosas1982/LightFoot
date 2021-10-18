using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Dominio.SeedWork;

namespace Core.Dominio.AggregatesModel
{
    public class SolicitudEvento : EntityBase
    {
        [Key]
        public int Id { get; set; }
        public int IdSolicitud { get; set; }
        public EstadoSolicitud EstadoSolicitud { get; set; }
        public string Comentario { get; set; }


        [ForeignKey("IdSolicitud")]
        public virtual Solicitud Solicitud { get; set; }

    }
}
