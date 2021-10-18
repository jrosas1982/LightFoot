using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Dominio.SeedWork;

namespace Core.Dominio.AggregatesModel
{
    public class Sucursal : EntityBase
    {
        public Sucursal()
        {
            Activo = true;
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Descripcion { get; set; }
        public string Direccion { get; set; }
        public Boolean Activo { get; set; }

        public virtual ICollection<ArticuloStock> ArticulosStock { get; set; }
        //public virtual ICollection<MovimientoStock> MovimientoStockOrigen { get; set; }
        //public virtual ICollection<MovimientoStock> MovimientoStockDestino { get; set; }
        [NotMapped]
        public virtual ICollection<Solicitud> Solicitudes { get; set; }
        public virtual ICollection<CajaSucursal> CajaSucursal { get; set; }
        public virtual ICollection<Compra> Compras { get; set; }
        public virtual ICollection<Venta> Ventas { get; set; }
    }
}
