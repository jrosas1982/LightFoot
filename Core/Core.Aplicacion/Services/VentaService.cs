using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Aplicacion.Helpers;
using Core.Aplicacion.Interfaces;
using Core.Dominio.AggregatesModel;
using Core.Dominio.CoreModelHelpers;
using Core.Infraestructura;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Core.Aplicacion.Services
{
    public class VentaService : IVentaService
    {
        private readonly AppDbContext _db;
        private readonly ILogger<VentaService> _logger;
        private readonly IArticuloService _articuloService;
        private readonly IProveedorService _proveedorService;
        private readonly IProveedorArticuloService _proveedorArticuloService;
        private readonly IControlStockArticuloService _controlStockArticuloService;
        private readonly IConfiguration _Configuration;

        public VentaService( ExtendedAppDbContext extendedAppDbContext, ILogger<VentaService> logger, IArticuloService articuloService, IProveedorService proveedorService, IProveedorArticuloService proveedorArticuloService, IControlStockArticuloService controlStockArticuloService, IConfiguration configuration)
        {
            _db = extendedAppDbContext.context;
            _logger = logger;
            _articuloService = articuloService;
            _proveedorService = proveedorService;
            _proveedorArticuloService = proveedorArticuloService;
            _controlStockArticuloService = controlStockArticuloService;
            _Configuration = configuration;
        }

        public Task AgregarPagoVenta(int idVenta, TipoPago tipoPago, decimal montoPagado)
        {
            throw new NotImplementedException();
        }

        public Task<Venta> BuscarPorId(int idVenta)
        {
            throw new NotImplementedException();
        }

        public Task CrearVenta(NuevaVentaModel cabecera, IEnumerable<NuevaVentaDetalleModel> detalles)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TipoPago>> GetTiposPago()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<VentaTipo>> GetTiposVenta()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<VentaDetalle>> GetVentaDetalles(int idVenta)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Venta>> GetVentas()
        {
            var ventasList = await _db.Ventas
                .AsNoTracking()
                .Include(x => x.VentaDetalles)
                    .ThenInclude(x => x.Articulo)
                .Include(x => x.Cliente)
                .Include(x => x.Sucursal)
                .OrderBy(x => x.FechaModificacion)
                .ThenBy(x => x.Descuento)
                .ThenByDescending(x => x.FechaModificacion.HasValue)
                .ThenByDescending(x => x.FechaCreacion)
                .ToListAsync();
            return ventasList;
        }

    }
}
