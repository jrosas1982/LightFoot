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
    }
}
