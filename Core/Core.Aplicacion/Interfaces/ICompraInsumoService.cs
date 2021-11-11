using System.Collections.Generic;
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
        public Task<bool> RecibirCompra(int idCompra, long nroRemito, double calificacionProveedor);
        public Task<bool> AgregarPagoCompra(int idCompra, TipoPago tipoPago, decimal montoPagado);
        public Task EnviarMailCompra(int IdCompra);
    }
}
