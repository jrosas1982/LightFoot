using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Core.Dominio.SeedWork;
namespace Core.Dominio.AggregatesModel
{
    public class FabricaParametro : EntityBase
    {
        [Key]
        public int Id { get; set; }
        public string Clave { get; set; }
        public string Valor { get; set; }

    }
}
