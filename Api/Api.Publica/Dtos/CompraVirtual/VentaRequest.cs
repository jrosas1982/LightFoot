using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Core.Dominio.AggregatesModel;

namespace LightFoot.Api.Publica.Dtos.CompraVirtual
{
    public class VentaRequest
    {
        public int IdCliente { get; set; }
        public int IdSucursal { get; set; }
        public decimal DescuentoRealizado { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public VentaTipo VentaTipo { get; set; }
        public IEnumerable<VentaDetalleRequest> Detalles { get; set; }
    }

    public class VentaDetalleRequest
    {
        public int IdArticulo { get; set; }
        public int Cantidad { get; set; }
    }
}
