using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Dominio.SeedWork;

namespace Core.Dominio.AggregatesModel
{
    public class MovimientoStock : EntityBase
    {
        [Key]
        public int Id { get; set; }
        public int IdSucursalOrigen { get; set; }
        public int IdSucursalDestino { get; set; }
        public int IdArticulo { get; set; }
        public int Cantidad { get; set; }
        public string Comentario { get; set; }

        [ForeignKey("IdSucursalOrigen")]
        public virtual Sucursal SucursalOrigen { get; set; }
        [ForeignKey("IdSucursalDestino")]
        public virtual Sucursal SucursalDestino { get; set; }
        [ForeignKey("IdArticulo")]
        public virtual Articulo Articulo { get; set; }
    }
}
