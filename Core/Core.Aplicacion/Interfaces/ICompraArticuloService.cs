using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Dominio.AggregatesModel;

namespace Core.Aplicacion.Interfaces
{
    public interface ICompraArticuloService
    {
        public Task<IEnumerable<CompraArticulo>> GetCompras();
        public Task<IEnumerable<CompraArticuloDetalle>> GetCompraDetalles(int IdCompra);
        public Task<CompraArticulo> BuscarPorId(int IdCompra);
        public Task CrearCompra(CompraArticulo compra, IEnumerable<CompraArticuloDetalle> detalles);
        public Task<bool> RecibirCompra(int idCompra, long nroRemito);
        public Task<bool> AgregarPagoCompra(int idCompra, TipoPago tipoPago, decimal montoPagado);        
    }
}
