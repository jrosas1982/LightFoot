using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Core.Dominio.SeedWork;

namespace Core.Dominio.AggregatesModel
{
    public class CompraArticuloDetalle : EntityBase
    {
        [Key]
        public int Id { get; set; }
        public int IdCompraArticulo { get; set; }
        public int IdArticulo { get; set; }
        public int IdProveedorSugerido { get; set; }
        [Column(TypeName = "decimal(18,2)")]        
        public decimal Monto { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public double Cantidad { get; set; }
        public string Comentario { get; set; }


        [ForeignKey("IdCompraArticulo")]
        public virtual CompraArticulo CompraArticulo { get; set; }

        [ForeignKey("IdArticulo")]
        public virtual Articulo Articulo { get; set; }

        [ForeignKey("IdProveedorSugerido")]
        public virtual Proveedor Proveedor { get; set; }
    }
}
