using System;
using System.Collections.Generic;
using System.Text;
using Core.Dominio.AggregatesModel;

namespace Core.Aplicacion.Helpers
{
    public class CantidadInsumoNecesarioStock
    {
        public Insumo Insumo { get; set; }
        public int CantidadNecesaria { get; set; }
        public int CantidadDisponible { get; set; }
    }
}
