using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Dominio.AggregatesModel;

namespace Core.Aplicacion.Interfaces
{
    public interface IParametroService
    {
        public Task<IEnumerable<Parametro>> GetParametros();
        public Task<Parametro> BuscarPorId(int idParametro);
        public Task<bool> CrearParametro(Parametro parametro);
        public Task<bool> EditarParamtro(Parametro parametro);
        public Task<bool> EliminarParamtro(Parametro parametro);
        public Task<bool> EditarParametro(int idParametro, string valor);
        //public Task<bool> EditarParametroSucursal(int idParametro, string valor);
        //public Task<bool> EditarParametroFabrica(int idParametro, string valor);
    }
}
