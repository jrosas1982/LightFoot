using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Dominio.AggregatesModel;

namespace Core.Aplicacion.Interfaces
{
    public interface IOrdenProduccionService
    {
        public Task<bool> AvanzarEtapa(int idOrdenProduccion);
        public Task<bool> PausarEtapa(int idOrdenProduccion);
        public Task<bool> ReanudarEtapa(int idOrdenProduccion);
        public Task<bool> RetrabajarEtapa(int idOrdenProduccion);
        public Task<bool> FinalizarOrden(int idOrdenProduccion);
        public Task<bool> CancelarOrden(int idOrdenProduccion);


        public Task<OrdenProduccion> BuscarPorId(int idOrdenProduccion);
        public Task<IEnumerable<OrdenProduccion>> GetOrdenes();
        public Task<IEnumerable<OrdenProduccionEvento>> GetOrdenEventos(int idOrdenProduccion);
        public Task<IEnumerable<EstadoOrdenProduccion>> GetEstadosOrden();
        public Task<IEnumerable<EtapaOrdenProduccion>> GetEtapasOrden();
        public Task<IEnumerable<EstadoEtapaOrdenProduccion>> GetEstadoEtapasOrden();
        public Task<int> GetProgreso(int IdOrdenProduccion);
    }
}
