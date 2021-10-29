using Core.Dominio.AggregatesModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aplicacion.Interfaces
{
   public interface IRecetaDetalleService
    {
        public Task<int> AgregarInsumoAReceta(RecetaDetalle receta);
        public Task<RecetaDetalle> BuscarInsumoDeRecetaPorId(int idInsumo);
        public Task<bool> EliminarInsumoAReceta(int lineaInsumo);

    }
}
