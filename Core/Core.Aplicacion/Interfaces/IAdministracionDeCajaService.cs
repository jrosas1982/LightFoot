using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Dominio.AggregatesModel;

namespace Core.Aplicacion.Interfaces
{
    public interface IAdministracionDeCajaService
    {
        public Task<IEnumerable<CajaSucursal>> GetMovimientos();
        public Task<CajaSucursal> BuscarPorId(int idCajaSucursal);
        //public Task<IEnumerable<RecetaDetalle>> GetRecetaDetalles(int IdSolicitud);
        //public Task<bool> EliminarReceta(int IdReceta);
        //public Task<bool> ActivarDesactivarReceta(int IdReceta);
        public Task AgregarMovimiento(CajaSucursal cajaSucursal);
    }
}
