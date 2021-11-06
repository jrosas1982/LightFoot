using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Aplicacion.Helpers;
using Core.Aplicacion.Interfaces;
using Core.Dominio.AggregatesModel;
using Core.Infraestructura;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Core.Aplicacion.Services
{
    public class CompraInsumoService : ICompraInsumoService
    {

        private readonly AppDbContext _db;
        private readonly ILogger<CompraInsumoService> _logger;

        public CompraInsumoService(ExtendedAppDbContext extendedAppDbContext, ILogger<CompraInsumoService> logger)
        {
            _db = extendedAppDbContext.context;
            _logger = logger;
        }

        public async Task<bool> AgregarPagoCompra(int idCompra, TipoPago tipoPago, decimal montoPagado)
        {
            throw new System.NotImplementedException();
        }

        public async Task<CompraInsumo> BuscarPorId(int IdCompra)
        {
            var compra = await _db.ComprasInsumos
                .AsNoTracking()
                .Include(x => x.CompraInsumoDetalles)
                    .ThenInclude(x => x.Insumo)
                        .ThenInclude(x => x.Proveedor)
                .Include(x => x.Proveedor)
                .SingleAsync(x => x.Id ==IdCompra);
            return compra;
        }

        public async Task CrearCompra(CompraInsumo compra, IEnumerable<CompraInsumoDetalle> detalles)
        {
            _db.ComprasInsumos.Add(compra);

            foreach (var detalle in detalles)
            {
                compra.CompraInsumoDetalles.Add(detalle);
            }

            await _db.SaveChangesAsync();
        }

        public async Task CrearCompra(CompraInsumo compra)
        {
            _db.ComprasInsumos.Add(compra);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<CompraInsumoDetalle>> GetCompraDetalles(int IdCompra)
        {
            var compraDetalles = await _db.CompraInsumoDetalles
                .AsNoTracking()
                .Include(x => x.Insumo)
                .ThenInclude(x => x.Proveedor)
                .Where(x => x.IdCompraInsumo == IdCompra)
                .ToListAsync();
            return compraDetalles;
        }

        public async Task<IEnumerable<CompraInsumo>> GetCompras()
        {
            var comprasList = await _db.ComprasInsumos
                .AsNoTracking()
                .Include(x => x.CompraInsumoDetalles)
                    .ThenInclude(x => x.Insumo)
                        .ThenInclude(x => x.Proveedor)
                .Include(x => x.Proveedor)                
                .ToListAsync();

            _logger.LogInformation("Se buscaron las compras de insumos");
            return comprasList;
        }

        public async Task<bool> RecibirCompra(int idCompra, long nroRemito)
        {
            throw new System.NotImplementedException();
        }
    }
}
