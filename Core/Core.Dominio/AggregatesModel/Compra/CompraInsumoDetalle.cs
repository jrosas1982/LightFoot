using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Core.Dominio.SeedWork;

namespace Core.Dominio.AggregatesModel
{
    public class CompraInsumoDetalle : EntityBase
    {
        [Key]
        public int Id { get; set; }
        public int IdCompraInsumo { get; set; }
        public int IdInsumo { get; set; }
        public int IdProveedorSugerido { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Monto { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public double Cantidad { get; set; }
        public string Comentario { get; set; }


        [ForeignKey("IdCompraInsumo")]
        public virtual CompraInsumo CompraInsumo { get; set; }

        [ForeignKey("IdInsumo")]
        public virtual Insumo Insumo { get; set; }

        [ForeignKey("IdProveedorSugerido")]
        public virtual Proveedor Proveedor { get; set; }
    }
}
