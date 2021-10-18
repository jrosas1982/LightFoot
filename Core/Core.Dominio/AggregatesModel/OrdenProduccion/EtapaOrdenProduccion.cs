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
        public string Descripcion { get; set; }
        public int Orden { get; set; }
        public Boolean Activo { get; set; }

        public virtual ICollection<RecetaDetalle> RecetaDetalle { get; set; }
    }
}
