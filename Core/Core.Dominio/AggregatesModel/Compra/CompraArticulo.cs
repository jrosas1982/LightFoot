using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Core.Dominio.SeedWork;

namespace Core.Dominio.AggregatesModel
{
    public class CompraArticulo : EntityBase
    {
        [Key]
        public int Id { get; set; }
        public int IdSucursal { get; set; }
        public int IdProveedor { get; set; }
        public bool Recibido { get; set; }
        public DateTime? FechaRecibido { get; set; }
        public bool Pagado { get; set; }
        public DateTime? FechaPagado { get; set; }
        public long? NroRemito { get; set; }
        public decimal MontoTotal { get; set; }


        [ForeignKey("IdSucursal")]
        public virtual Sucursal Sucursal { get; set; }

        [ForeignKey("IdProveedor")]
        public virtual Proveedor Proveedor { get; set; }

        public virtual ICollection<CompraArticuloDetalle> CompraArticuloDetalles { get; set; } = new HashSet<CompraArticuloDetalle>();
    }
    //    CompraArticulo(Id, IdCompra, IdArticulo, Monto, Cantidad, Recibido)

}
