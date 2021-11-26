using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Dominio.AggregatesModel;

namespace Core.Aplicacion.Interfaces
{
    public interface IOrdenProduccionService
    {
        public Task IniciarEtapa(int idOrdenProduccion, string comentario = "");
        public Task PausarEtapa(int idOrdenProduccion, string comentario);
        public Task ReanudarEtapa(int idOrdenProduccion, string comentario = "");
        public Task FinalizarEtapa(int idOrdenProduccion, string comentario = "");
        public Task RetrabajarEtapa(int idOrdenProduccion, string comentario);
        public Task AvanzarSiguienteEtapa(int idOrdenProduccion, string comentario = "");
        public Task FinalizarOrden(int idOrdenProduccion, string comentario = "");
        public Task CancelarOrden(int idOrdenProduccion, string comentario);


        public Task<OrdenProduccion> BuscarPorId(int idOrdenProduccion);
        public Task<IEnumerable<OrdenProduccion>> GetOrdenes();
        public Task<IEnumerable<OrdenProduccionEvento>> GetOrdenEventos(int idOrdenProduccion);
        public Task<IEnumerable<EstadoOrdenProduccion>> GetEstadosOrden();
        public Task<IEnumerable<EtapaOrdenProduccion>> GetEtapasOrden();
        public Task<IEnumerable<EstadoEtapaOrdenProduccion>> GetEstadoEtapasOrden();

        public Task<int> GetProgreso(int IdOrdenProduccion);
        public Task<TimeSpan> GetTiempoTranscurrido(int IdOrdenProduccion);
    }
}

