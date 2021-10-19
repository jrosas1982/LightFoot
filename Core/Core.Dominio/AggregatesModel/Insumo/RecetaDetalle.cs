using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Core.Dominio.SeedWork;

namespace Core.Dominio.AggregatesModel
{
   public class RecetaDetalle : EntityBase
    {
        [Key]
        public int Id { get; set; }
        public int IdReceta { get; set; }
        public int IdInsumo { get; set; }
        public int IdEtapaOrdenProduccion { get; set; }
        public int Cantidad { get; set; }
        public string Comentario { get; set; }

        [ForeignKey("IdReceta")]
        public virtual Receta Receta { get; set; }
        [ForeignKey("IdInsumo")]
        public virtual Insumo Insumo { get; set; }
        [ForeignKey("IdEtapaOrdenProduccion")]
        public virtual EtapaOrdenProduccion EtapaOrdenProduccion { get; set; }
    }
}
