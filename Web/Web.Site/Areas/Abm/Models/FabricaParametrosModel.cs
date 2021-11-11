using Core.Dominio.AggregatesModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Site.Areas.Abm.Models
{
    public class FabricaParametrosModel
    {
        public int Id { get; set; }
        public string Modulo { get; set; }
        public Parametro Parametro { get; set; }
        public string Descripcion { get; set; }
        public int Valor { get; set; }
    }
}
