using Core.Dominio.AggregatesModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aplicacion.Interfaces
{
    public interface IRecetaService
    {
        public Task<Receta> BuscarPorId(int IdReceta);
        public Task<IEnumerable<Receta>> GetRecetas();
        public Task<IEnumerable<RecetaDetalle>> GetRecetaDetalles(int IdSolicitud);
        public Task EliminarReceta(int IdReceta);
        public Task ActivarDesactivarReceta(int IdReceta);
        public Task CrearReceta(Receta Receta);
    }
}
