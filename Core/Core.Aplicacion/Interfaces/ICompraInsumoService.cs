using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Dominio.AggregatesModel;

namespace Core.Aplicacion.Interfaces
{

        public interface ICompraInsumoService
        {
            public Task<IEnumerable<CompraInsumo>> GetCompras();
            public Task<IEnumerable<CompraInsumoDetalle>> GetCompraDetalles(int IdCompra);
            public Task<CompraInsumo> BuscarPorId(int IdCompra);
            public Task CrearCompra(CompraInsumo compra, IEnumerable<CompraInsumoDetalle> detalles);
            public Task<bool> RecibirCompra(int idCompra, long nroRemito);
            public Task<bool> AgregarPagoCompra(int idCompra, TipoPago tipoPago, decimal montoPagado);
        }
}
