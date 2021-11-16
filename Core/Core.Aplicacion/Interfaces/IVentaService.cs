using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Dominio.AggregatesModel;
using Core.Dominio.CoreModelHelpers;

namespace Core.Aplicacion.Interfaces
{
    public interface IVentaService
    {
        public Task<IEnumerable<Venta>> GetVentas();
        public Task<IEnumerable<TipoPago>> GetTiposPago();
        public Task<IEnumerable<VentaTipo>> GetTiposVenta();
        public Task<IEnumerable<VentaDetalle>> GetVentaDetalles(int idVenta);
        public Task<Venta> BuscarPorId(int idVenta);
        public Task CrearVenta(NuevaVentaModel cabecera, IEnumerable<NuevaVentaDetalleModel> detalles);
        public Task AgregarPagoVenta(int idVenta, TipoPago tipoPago, decimal montoPagado);
        //public Task<bool> AcreditarPago (...); TODO desafectar de ventas
    }
}
