using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Aplicacion.Auth;
using Core.Aplicacion.Helpers;
using Core.Aplicacion.Interfaces;
using Core.Dominio.AggregatesModel;
using Core.Infraestructura;
using Microsoft.Extensions.Logging;

namespace Core.Aplicacion.Services
{
    public class ParametroService : IParametroService
    {
        private readonly AppDbContext _db;
        private readonly ILogger<ParametroService> _logger;

        public ParametroService(ExtendedAppDbContext extendedAppDbContext, ILogger<ParametroService> logger)
        {
            _db = extendedAppDbContext.context;
            _logger = logger;
        }

        public Task<FabricaParametro> BuscarPorId(int idParametro)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditarParamtro(int idParametro, int valor)
        {
            //    if(_db.GetGroupName() == Policies.IsFabrica)
            //        throw new NotImplementedException();
            throw new NotImplementedException();
        }

        public Task<IEnumerable<FabricaParametro>> GetParametros()
        {
            throw new NotImplementedException();
        }

        public Task<int> GetValorByParametro(Parametro parametro)
        {
            throw new NotImplementedException();
        }
        
        //tiempoCalificacion
        //distanciaCalificacion
        //precioCalificacion
        //calidadCalificacion
    }
}
