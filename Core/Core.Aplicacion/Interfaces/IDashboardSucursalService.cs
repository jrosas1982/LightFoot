using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Dominio.AggregatesModel;
using Core.Dominio.CoreModelHelpers;

namespace Core.Aplicacion.Interfaces
{
    public interface IDashboardSucursalService
    {
        public Task<Sucursal> GetSucursal();
        public Task<IEnumerable<Tuple<Articulo,int>>> GetMasVendidos(int n);
        public Task<IEnumerable<ArticuloStock>> GetArticulosBajoStock(int n);
        public Task<IEnumerable<CajaSucursal>> GetUltimosMovimientos(int n);
        public Task<IEnumerable<Venta>> GetUltimasVentas(int n);
    }
}
