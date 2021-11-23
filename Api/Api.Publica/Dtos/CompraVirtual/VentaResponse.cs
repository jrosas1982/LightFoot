using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Core.Dominio.AggregatesModel;

namespace LightFoot.Api.Publica.Dtos.CompraVirtual
{
    public class VentaResponse
    {
        public int IdVenta { get; set; }
        public string NombreSucursal { get; set; }
        public string NombreCliente { get; set; }
        public string UsuarioVendedor { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public VentaTipo VentaTipo { get; set; }
        public decimal MontoTotal { get; set; }
        public decimal DescuentoRealizado { get; set; }
        public DateTime Fecha { get; set; }
        public bool Pagado { get; set; }
        public DateTime? FechaPagado { get; set; }
        public IEnumerable<VentaDetalleResponse> Detalles { get; set; }
        public IEnumerable<VentaPagoResponse> Pagos { get; set; }
    }

    public class VentaDetalleResponse
    {
        public string Articulo { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal PrecioTotal { get; set; }
    }

    public class VentaPagoResponse
    {
        public decimal MontoPercibido { get; set; }
        public TipoPago TipoPago { get; set; }
    }
}
