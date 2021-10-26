using System;
using System.Collections.Generic;
using System.Text;
using Core.Dominio.AggregatesModel;

namespace Core.Aplicacion.Helpers
{
    public class CantidadInsumoNecesarioStock
    {
        public Insumo Insumo { get; set; }
        public double CantidadNecesaria { get; set; }
        public double CantidadDisponible { get; set; }
    }
}
