﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Dominio.AggregatesModel;
using Core.Dominio.CoreModelHelpers;

namespace Core.Aplicacion.Interfaces
{

    public interface ICompraInsumoService
    {
        public Task<IEnumerable<CompraInsumo>> GetCompras();
        public Task<IEnumerable<TipoPago>> GetTiposPago();
        public Task<IEnumerable<CompraInsumoDetalle>> GetCompraDetalles(int IdCompra);
        public Task<CompraInsumo> BuscarPorId(int IdCompra);
        public Task CrearCompra(IEnumerable<NuevaCompraInsumoModel> detalles);
        public Task RecibirCompra(int idCompra, long nroRemito, int tiempoCalificacion, int distanciaCalificacion, int precioCalificacion, int calidadCalificacion);
        public Task AgregarPagoCompra(int idCompra, TipoPago tipoPago, decimal montoPagado);
        public Task EnviarMailCompra(int IdCompra);
        public Task<IEnumerable<ProveedorInsumoCuentaCorriente>> GetPagosRealizados(int IdCompra);
    }
}
