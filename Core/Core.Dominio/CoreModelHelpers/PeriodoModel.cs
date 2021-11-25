using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dominio.CoreModelHelpers
{
    public class PeriodoModel
    {
        public int Año { get; set; }
        public int Mes { get; set; }

        public PeriodoModel()
        {
            Año = DateTime.Now.Year;
            //mes = "5";
            Mes = DateTime.Now.Month;
        }
    }
}
