﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Dominio.AggregatesModel;

namespace Core.Aplicacion.Interfaces
{
    public interface ISolicitudService
    {
        public Task CrearSolicitud(Solicitud solicitud, IEnumerable<SolicitudDetalle> solicitudDetalles, string comentario = "");
        public Task AprobarSolicitud(int idSolicitud, string comentario = "");
        public Task RechazarSolicitud(int idSolicitud, string comentario);

        public Task<Solicitud> BuscarPorId(int IdSolicitud);
        public Task<IEnumerable<Solicitud>> GetSolicitudes();
        public Task<IEnumerable<SolicitudDetalle>> GetSolicitudDetalles(int idSolicitud);
        public Task<IEnumerable<SolicitudEvento>> GetSolicitudEventos(int idSolicitud);
        public Task<IEnumerable<EstadoSolicitud>> GetEstadosSolicitud();

        public Task<bool> HayStockSuficiente(int idSolicitud);
    }
}