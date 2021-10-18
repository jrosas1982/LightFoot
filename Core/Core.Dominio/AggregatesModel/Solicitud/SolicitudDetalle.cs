using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Dominio.SeedWork;

namespace Core.Dominio.AggregatesModel
{
    public class SolicitudDetalle : EntityBase
    {
        [Key]
        public int Id { get; set; }
        public int IdSolicitud { get; set; }
        public int IdArticulo { get; set; }
        [Display (Name ="Cantidad")]
        public int CantidadSolicitada { get; set; }
        public string Motivo { get; set; }


        [ForeignKey("IdSolicitud")]
        public virtual Solicitud Solicitud { get; set; }

        [ForeignKey("IdArticulo")]
        public virtual Articulo Articulo { get; set; }

        public virtual ICollection<OrdenProduccion> OrdenesProduccion { get; set; }
    }
}
