using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Aplicacion.Auth;
using Core.Aplicacion.Helpers;
using Core.Aplicacion.Interfaces;
using Core.Dominio.AggregatesModel;
using Core.Infraestructura;
using Microsoft.EntityFrameworkCore;
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

        public async Task<FabricaParametro> BuscarPorId(int idParametro)
        {
           return await _db.FabricaParametros.SingleOrDefaultAsync(x => x.Id == idParametro);
        }

        public Task<bool> EditarParamtro(int idParametro, int valor)
        {
            //    if(_db.GetGroupName() == Policies.IsFabrica)
            //        throw new NotImplementedException();
            throw new NotImplementedException();
        }

        public async Task<bool> CrearParamtro(FabricaParametro fabricaParametro)
        {
            try
            {
                _db.Add(fabricaParametro);
                await _db.SaveChangesAsync();
                return  true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al CrearParamtro {ex.Message}");
                return false;
            }
        }

        public async Task<IEnumerable<FabricaParametro>> GetParametros()
        {
            return await _db.FabricaParametros.ToListAsync();       
        }

        public async Task<int> GetValorByParametro(Parametro parametro)
        {
            var parametroDb = await _db.FabricaParametros.SingleAsync(x => x.Parametro == parametro);

            return parametroDb.Valor;
        }

        public async Task<bool> EditarParamtro(FabricaParametro fabricaParametro)
        {
            try
            {
                 _db.FabricaParametros.Update(fabricaParametro);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al EditarParamtro {ex.Message}");
                return false;
            }

        }


        //tiempoCalificacion
        //distanciaCalificacion
        //precioCalificacion
        //calidadCalificacion
    }
}
