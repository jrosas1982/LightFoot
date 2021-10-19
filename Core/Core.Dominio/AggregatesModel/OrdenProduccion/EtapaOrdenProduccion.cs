using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Dominio.SeedWork;

namespace Core.Dominio.AggregatesModel
{
    public class EtapaOrdenProduccion : EntityBase
    {
        public EtapaOrdenProduccion()
        {
            Activo = true;
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public int Orden { get; set; }
        public Boolean Activo { get; set; }

        public virtual ICollection<RecetaDetalle> RecetaDetalle { get; set; } = new HashSet<RecetaDetalle>();
    }
}
