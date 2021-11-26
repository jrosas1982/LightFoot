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
        [Required]
        public string Direccion { get; set; }
        public Boolean Activo { get; set; }

        public virtual ICollection<ArticuloStock> ArticulosStock { get; set; } = new HashSet<ArticuloStock>();
        //public virtual ICollection<MovimientoStock> MovimientoStockOrigen { get; set; }
        //public virtual ICollection<MovimientoStock> MovimientoStockDestino { get; set; }
        public virtual ICollection<Solicitud> Solicitudes { get; set; } = new HashSet<Solicitud>();
        public virtual ICollection<CajaSucursal> CajaSucursal { get; set; } = new HashSet<CajaSucursal>();
        public virtual ICollection<CompraArticulo> CompraArticulos { get; set; } = new HashSet<CompraArticulo>();
        public virtual ICollection<Venta> Ventas { get; set; } = new HashSet<Venta>();
    }
}
