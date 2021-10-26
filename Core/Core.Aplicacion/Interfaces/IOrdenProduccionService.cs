using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Dominio.AggregatesModel;

namespace Core.Aplicacion.Interfaces
{
    public interface IOrdenProduccionService
    {
        public Task<bool> IniciarEtapa(int idOrdenProduccion, string comentario = "");
        public Task<bool> PausarEtapa(int idOrdenProduccion, string comentario);
        public Task<bool> ReanudarEtapa(int idOrdenProduccion, string comentario = "");
        public Task<bool> FinalizarEtapa(int idOrdenProduccion, string comentario = "");
        public Task<bool> RetrabajarEtapa(int idOrdenProduccion, string comentario);
        public Task<bool> AvanzarSiguienteEtapa(int idOrdenProduccion, string comentario = "");
        public Task<bool> FinalizarOrden(int idOrdenProduccion, string comentario = "");
        public Task<bool> CancelarOrden(int idOrdenProduccion, string comentario);


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

