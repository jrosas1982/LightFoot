using System;

namespace LightFoot.Api.Publica.Dtos.CompraVirtual
{
    public class VentaResponse
    {
        public int IdItem { get; set; }
        public int IdCliente { get; set; }
        public int Cantidad { get; set; }
        public DateTime Fecha { get; set; }
        public double Precio { get; set; }
    }
}
