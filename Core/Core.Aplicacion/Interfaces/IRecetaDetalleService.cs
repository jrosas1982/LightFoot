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
        public RecetaDetalle BuscarInsumoDeRecetaPorId(int IdInsumo);
        public Task<bool> EliminarInsumoAReceta(int lineaInsumo);

    }
}
