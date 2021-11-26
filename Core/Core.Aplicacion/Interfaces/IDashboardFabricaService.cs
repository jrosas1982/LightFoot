using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Dominio.AggregatesModel;
using Core.Dominio.CoreModelHelpers;

namespace Core.Aplicacion.Interfaces
{
    public interface IDashboardFabricaService
    {
        public Task<IEnumerable<Tuple<int, int>>> RankingSucursales();
        //public Task<IEnumerable<Insumo>> GetArticulosBajoStock(int n);
        public Task<IEnumerable<Insumo>> GetInsumosBajoStock(int n);
        public Task<IEnumerable<Solicitud>> GetSolicitudes();
        public Task<IEnumerable<OrdenProduccion>> GetOrdenes();


        public Task<int> GetSolicitudesRecibidas(DateTime fecha); //del dia
        public Task<int> GetSolicitudesRecibidas(DateTime fecha, TimeSpan plazoTiempo); //ultima semana

        public Task<int> GetOrdenesProduccionFinalizadas(DateTime fecha); //del dia
        public Task<int> GetOrdenesProduccionFinalizadas(DateTime fecha, TimeSpan plazoTiempo); //ultima semana

        public Task<int> GetOrdenesProduccionCanceladas(DateTime fecha); //del dia
        public Task<int> GetOrdenesProduccionCanceladas(DateTime fecha, TimeSpan plazoTiempo); //ultima semana

        public Task<IEnumerable<Tuple<Sucursal, int>>> GetTopSucursalesSolicitudes(int n); //top n sucursales
        public Task<IEnumerable<Tuple<EtapaOrdenProduccion, int>>> GetAvanceProduccion(DateTime fecha, TimeSpan plazoTiempo); //avance semanal
    }
}
