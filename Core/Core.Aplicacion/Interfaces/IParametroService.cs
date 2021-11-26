using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Dominio.AggregatesModel;

namespace Core.Aplicacion.Interfaces
{
    public interface IParametroService
    {
        public Task<IEnumerable<FabricaParametro>> GetParametros();
        public Task<FabricaParametro> BuscarPorId(int idParametro);
        public Task<bool> EditarParamtro(int idParametro, int valor);
        public Task<bool> EditarParamtro(FabricaParametro fabricaParametro);
        public Task<int> GetValorByParametro(Parametro parametro);
        public Task<bool> CrearParamtro(FabricaParametro fabricaParametro);
        //public Task<bool> EliminarParamtro(Parametro parametro);
        //public Task<bool> EditarParametro(int idParametro, string valor);
        //public Task<bool> EditarParametroSucursal(int idParametro, string valor);
        //public Task<bool> EditarParametroFabrica(int idParametro, string valor);
    }
}
