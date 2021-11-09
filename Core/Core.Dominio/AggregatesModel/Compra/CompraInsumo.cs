using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Core.Dominio.SeedWork;


namespace Core.Dominio.AggregatesModel
{
    public class CompraInsumo : EntityBase
    {
       public CompraInsumo()
        {
            Recibido = false;
            Pagado = false;
        }

        [Key]
        public int Id { get; set; }
        public int IdProveedor { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public bool Recibido { get; set; }
        public DateTime FechaRecibido { get; set; }
        public bool Pagado { get; set; }
        public DateTime FechaPagado { get; set; }
        public long NroRemito { get; set; }
        public decimal MontoTotal { get; set; }


        [ForeignKey("IdProveedor")]
        public virtual Proveedor Proveedor { get; set; }

        public virtual ICollection<CompraInsumoDetalle> CompraInsumoDetalles { get; set; } = new HashSet<CompraInsumoDetalle>();
    }
    //   CompraInsumo(Id, IdPedido, IdInsumo, Monto, Cantidad, Recibido)
}
