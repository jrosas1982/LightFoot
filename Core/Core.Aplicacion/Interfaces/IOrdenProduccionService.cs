using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Dominio.AggregatesModel;

namespace Core.Aplicacion.Interfaces
{
    public interface IOrdenProduccionService
    {
        public Task<IEnumerable<OrdenProduccion>> GetOrdenes();
        public Task<OrdenProduccion> BuscarPorId(int idOrdenProduccion);
        public Task<IEnumerable<OrdenProduccionEvento>> GetOrdenEventos(int idOrdenProduccion);
        public Task<IEnumerable<EstadoOrdenProduccion>> GetEstadosOrden();
        public Task<IEnumerable<EtapaOrdenProduccion>> GetEtapasOrden();
        public Task<IEnumerable<EstadoEtapaOrdenProduccion>> GetEstadosEtapaOrden();
        public Task<bool> SetEtapaOrdenProduccion(int idOrdenProduccion, int idEtapaOrdenProduccion);
        public Task<bool> SetEtapaOrdenProduccion(int idOrdenProduccion, EtapaOrdenProduccion etapaOrdenProduccion);
        public Task<bool> SetEstadoEtapaOrdenProduccion(int IdOrdenProduccion, EstadoEtapaOrdenProduccion estadoEtapaOrdenProduccion);
    }
}
