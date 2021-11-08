using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Core.Dominio.SeedWork;

namespace Core.Dominio.AggregatesModel
{
    public class Parametro : EntityBase
    {
        [Key]
        public int Id { get; set; }
        public string Modulo { get; set; }
        public string Variable { get; set; }
        public string Descripcion { get; set; }
        public string ValorPorDefecto { get; set; }
    }
}
