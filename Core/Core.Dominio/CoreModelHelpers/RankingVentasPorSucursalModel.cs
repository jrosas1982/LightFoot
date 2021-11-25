using System;
using System.Collections.Generic;
using System.Text;
using Core.Dominio.AggregatesModel;

namespace Core.Dominio.CoreModelHelpers
{
    public class RankingVentasPorSucursalModel
    {
        public Sucursal Sucursal { get; set; }
        public IEnumerable<Venta> Ventas { get; set; }
    }
    public class RankingVentasPorUsuarioModel
    {
        public Usuario Usuario { get; set; }
        public IEnumerable<Venta> Ventas { get; set; }
    }
}
