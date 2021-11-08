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
        public Task<Parametro> BuscarPorId(int idParametro)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CrearParametro(Parametro parametro)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> EditarParametro(int idParametro, string valor)
        {
            if(_db.GetGroupName() == "Fabrica")
                throw new NotImplementedException();
            throw new NotImplementedException();
        }

        public Task<bool> EditarParamtro(Parametro parametro)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EliminarParamtro(Parametro parametro)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Parametro>> GetParametros()
        {
            throw new NotImplementedException();
        }
    }
}
