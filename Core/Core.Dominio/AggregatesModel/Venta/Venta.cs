﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Dominio.SeedWork;

namespace Core.Dominio.AggregatesModel
{
    public class Venta : EntityBase
    {
        [Key]
        public int Id { get; set; }
        public int IdSucursal { get; set; }
        public int IdCliente { get; set; }
        public string UsuarioVenta { get; set; }
        public VentaTipo VentaTipo { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal MontoTotal { get; set; }
        public decimal Descuento { get; set; }
        public bool Pagado { get; set; }
        public DateTime? FechaUltimoPago { get; set; }

        [ForeignKey("IdSucursal")]
        public virtual Sucursal Sucursal { get; set; }

        [ForeignKey("IdCliente")]
        public virtual Cliente Cliente { get; set; }

        public virtual ICollection<VentaDetalle> VentaDetalles { get; set; } = new HashSet<VentaDetalle>();
    }
    //Venta (Id, IdSucursal, IdCliente, IdVentaTipo, IdUsuario, MontoTotal)
}
