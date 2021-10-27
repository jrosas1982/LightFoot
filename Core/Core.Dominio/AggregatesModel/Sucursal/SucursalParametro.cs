using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Core.Dominio.SeedWork;

namespace Core.Dominio.AggregatesModel
{
    public class SucursalParametro : EntityBase
    {
        [Key]
        public int Id { get; set; }
        public int IdSucursal { get; set; }
        public string Clave { get; set; }
        public string Valor { get; set; }

        [ForeignKey("IdSucursal")]
        public virtual Sucursal Sucursal { get; set; }
    }
}
