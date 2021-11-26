using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Dominio.SeedWork;

namespace Core.Dominio.AggregatesModel
{
    public class OrdenProduccion : EntityBase
    {
        [Key]
        public int Id { get; set; }
        public int IdSolicitudDetalle { get; set; }
        public int IdArticulo { get; set; }
        public int IdEtapaOrdenProduccion { get; set; }
        public EstadoOrdenProduccion EstadoOrdenProduccion { get; set; }
        public EstadoEtapaOrdenProduccion EstadoEtapaOrdenProduccion { get; set; }
        public int CantidadTotal { get; set; }
        public int CantidadFabricada { get; set; }


        [ForeignKey("IdSolicitudDetalle")]
        public virtual SolicitudDetalle SolicitudDetalle { get; set; }

        [ForeignKey("IdArticulo")]
        public virtual Articulo Articulo { get; set; }

        [ForeignKey("IdEtapaOrdenProduccion")]
        public virtual EtapaOrdenProduccion EtapaOrdenProduccion { get; set; }

        public virtual ICollection<OrdenProduccionEvento> OrdenProduccionEventos { get; set; } = new HashSet<OrdenProduccionEvento>();
    }
}
