using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Dominio.AggregatesModel;

namespace Core.Aplicacion.Interfaces
{
    public interface ISolicitudService
    {
        public Task<IEnumerable<Solicitud>> GetSolicitudes();
        public Task<Solicitud> BuscarPorId(int IdSolicitud);

        public Task CrearSolicitud(Solicitud solicitud, IEnumerable<SolicitudDetalle> solicitudDetalles);
        public Task EditarSolicitud(Solicitud solicitud);
        public Task<bool> EliminarSolicitud(Solicitud solicitud);

        public Task<IEnumerable<SolicitudDetalle>> GetSolicitudDetalles(int idSolicitud);
        public Task AgregarSolicitudDetalles(int idSolicitud, IEnumerable<SolicitudDetalle> solicitudDetalle);
        public Task EliminarSolicitudDetalle(int idSolicitudDetalle);

        public Task<IEnumerable<SolicitudEvento>> GetSolicitudEventos(int idSolicitud);
        //public Task EliminarSolicitudEvento(int idSolicitudDetalle);

        public Task AprobarSolicitud(int idSolicitud, string comentario = "");
        public Task RechazarSolicitud(int idSolicitud, string comentario);
        public Task<IEnumerable<EstadoSolicitud>> GetEstadosSolicitud();

    }
}