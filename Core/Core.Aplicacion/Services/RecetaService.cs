using Core.Aplicacion.Interfaces;
using Core.Dominio.AggregatesModel;
using Core.Infraestructura;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aplicacion.Services
{
    public class RecetaService : IRecetaService
    {
        private readonly AppDbContext _db;
        private readonly ILogger<SolicitudService> _logger;

        public RecetaService(ExtendedAppDbContext extendedAppDbContext, ILogger<SolicitudService> logger)
        {
            _db = extendedAppDbContext.context;
            _logger = logger;
        }

        public async Task<Receta> BuscarPorId(int IdReceta)
        {
            var receta = await _db.Recetas.FindAsync(IdReceta);
            return receta;
        }

        public async Task<IEnumerable<Receta>> GetRecetas()
        {
            var recetasList = _db.Recetas
                .Include(x => x.Articulo)
                .Include(x => x.RecetaDetalles)
                .ThenInclude(x => x.Insumo)
                .Include(x => x.RecetaDetalles)
                .ThenInclude(x => x.EtapaOrdenProduccion);
           
            _logger.LogInformation("Se buscaron las solicitudes");
            return await recetasList.ToListAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<RecetaDetalle>> GetRecetaDetalles(int Idreceta)
        {
            var detallesSolicitud = _db.RecetaDetalles.Where(x => x.IdReceta == Idreceta);

            return await detallesSolicitud.ToListAsync();
        }

        public Task<bool> EliminarReceta(int IdReceta)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DesactivarReceta(int IdReceta)
        {
           throw new NotImplementedException();
        }
    }
}
