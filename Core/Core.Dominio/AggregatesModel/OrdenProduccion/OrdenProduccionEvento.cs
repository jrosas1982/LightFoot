using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Dominio.SeedWork;

namespace Core.Dominio.AggregatesModel
{
    public class OrdenProduccionEvento : EntityBase
    {
        [Key]
        public int Id { get; set; }
        public int IdOrdenProduccion { get; set; }
        public int IdEtapaOrdenProduccion { get; set; }
        public EstadoOrdenProduccion EstadoOrdenProduccion { get; set; }
        public EstadoEtapaOrdenProduccion EstadoEtapaOrdenProduccion { get; set; }
        public int CantidadFabricada { get; set; }
        public string Comentario { get; set; }

        [ForeignKey("IdOrdenProduccion")]
        public virtual OrdenProduccion OrdenProduccion { get; set; }

        [ForeignKey("IdEtapaOrdenProduccion")]
        public virtual EtapaOrdenProduccion EtapaOrdenProduccion { get; set; }

    }
}
