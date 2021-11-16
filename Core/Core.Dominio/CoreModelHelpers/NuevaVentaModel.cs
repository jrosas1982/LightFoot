using System;
using System.Collections.Generic;
using System.Text;
using Core.Dominio.AggregatesModel;

namespace Core.Dominio.CoreModelHelpers
{
    public class NuevaVentaModel
    {
        public VentaTipo VentaTipo { get; set; }
        public double DescuentoRealizado { get; set; }
        public int IdCliente { get; set; }
    }
}
