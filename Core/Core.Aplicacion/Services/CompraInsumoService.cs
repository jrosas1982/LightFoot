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
    public class CompraInsumoService : ICompraInsumoService
    {

        private readonly AppDbContext _db;
        private readonly ILogger<CompraInsumoService> _logger;
        private readonly IInsumoService _insumoService;
        private readonly IProveedorService _proveedorService;
        private readonly IProveedorInsumoService _proveedorInsumoService;
        private readonly IParametroService _parametroService;
        private readonly IConfiguration _Configuration;

        public CompraInsumoService(AppDbContext db, ILogger<CompraInsumoService> logger, IConfiguration configuration, IInsumoService insumoService, IProveedorService proveedorService, IProveedorInsumoService proveedorInsumoService, IParametroService parametroService)
        {
            _db = db;
            _logger = logger;
            _Configuration = configuration;
            _insumoService = insumoService;
            _proveedorService = proveedorService;
            _proveedorInsumoService = proveedorInsumoService;
            _parametroService = parametroService;
        }

        public async Task<bool> AgregarPagoCompra(int idCompra, TipoPago tipoPago, decimal montoPagado)
        {
            try
            {
                var compra = await _db.ComprasInsumos.SingleOrDefaultAsync(x => x.Id == idCompra);
                if (compra == null)
                    throw new Exception("No existe la compra");

                var montoPagadoTotal = await _db.ProveedoresInsumosCuentaCorriente.Where(x => x.IdCompraInsumo == idCompra).SumAsync(x => x.MontoPagado);

                if (compra.MontoTotal < (montoPagado + montoPagadoTotal))
                    throw new Exception("No se puede pagar mas del total de la compra");

                var proveedorCuentaCorriente = new ProveedorInsumoCuentaCorriente()
                {
                    IdProveedor = compra.IdProveedor,
                    IdCompraInsumo = compra.Id,
                    TipoPago = tipoPago,
                    MontoPagado = montoPagado
                };

                compra.Pagado = true;
                compra.FechaPagado = DateTime.Now;

                _db.ComprasInsumos.Update(compra);

                _db.ProveedoresInsumosCuentaCorriente.Add(proveedorCuentaCorriente);

                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
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

        public async Task CrearCompra(IEnumerable<NuevaCompraInsumoModel> detalles)
        {
            var comprasAgrupadas = detalles.GroupBy(x => x.IdProveedor).ToList();
            var proveedores = await _proveedorService.GetProveedores();

            foreach (var grupo in comprasAgrupadas)
            {
                var compra = new CompraInsumo()
                {
                    IdProveedor = grupo.Key,
                };

                decimal montoTotal = 0;
                foreach (var item in grupo)
                {
                    var proveedorInsumo = await _proveedorInsumoService.BuscarProveedorInsumo(item.IdInsumo, item.IdProveedor);
                    var proveedoresInsumoList = proveedores.Where(x => x.ProveedorInsumos.Any(y => y.IdInsumo == item.IdInsumo));

                    if (!proveedoresInsumoList.Any() || proveedorInsumo == null)
                        throw new Exception($"No existe ningun proveedor asignado para el insumo {(await _insumoService.BuscarPorId(item.IdInsumo)).Nombre}");

                    var proveedorSugerido = proveedoresInsumoList.OrderByDescending(x => x.Calificacion).First();

                    var montoDetalle = proveedorInsumo.Precio * item.Cantidad;
                    montoTotal += montoDetalle;

                    var detalle = new CompraInsumoDetalle()
                    {
                        IdInsumo = item.IdInsumo,
                        IdProveedorSugerido = proveedorSugerido.Id,
                        Monto = montoDetalle,
                        Cantidad = item.Cantidad,
                        Comentario = item.Comentario,
                    };

                    compra.CompraInsumoDetalles.Add(detalle);
                }
                compra.MontoTotal = montoTotal;
                _db.ComprasInsumos.Add(compra);

                await _db.SaveChangesAsync();


                //Envio de Email a proveedor
                await EnviarMailCompra(compra.Id);
            }
        }        

        public async Task<IEnumerable<CompraInsumoDetalle>> GetCompraDetalles(int IdCompra)
        {
            var compraDetalles = await _db.CompraInsumoDetalles
                .AsNoTracking()
                .Include(x => x.Insumo)
                .ThenInclude(x => x.Proveedor)
                .Where(x => x.IdCompraInsumo == IdCompra)
                .OrderByDescending(x => x.FechaModificacion.HasValue)
                .ThenByDescending(x => x.FechaCreacion)
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
                .OrderBy(x => x.Recibido)
                .ThenBy(x => x.Pagado)
                .ThenByDescending(x => x.FechaModificacion.HasValue)
                .ThenByDescending(x => x.FechaCreacion)
                .ToListAsync();

            _logger.LogInformation("Se buscaron las compras de insumos");
            return comprasList;
        }

        public async Task<IEnumerable<TipoPago>> GetTiposPago()
        {
            return await Task.FromResult(EnumExtensions.GetValues<TipoPago>());
        }

        public async Task<bool> RecibirCompra(int idCompra, long nroRemito, int tiempoCalificacion, int distanciaCalificacion, int precioCalificacion, int calidadCalificacion)
        {
            try
            {
                var compra = await _db.ComprasInsumos.Include(x => x.CompraInsumoDetalles).SingleOrDefaultAsync(x => x.Id == idCompra);
                if (compra == null)
                    throw new Exception("No existe la compra");

                compra.Recibido = true;
                compra.FechaRecibido = DateTime.Now;
                compra.NroRemito = nroRemito;

                foreach (var detalle in compra.CompraInsumoDetalles)
                {
                    var insumo = await _insumoService.BuscarPorId(detalle.IdInsumo);
                    insumo.StockTotal += detalle.Cantidad;
                    _db.Insumos.Update(insumo);
                }

                _db.ComprasInsumos.Update(compra);

                var proveedor = await _db.Proveedores.FindAsync(compra.IdProveedor);                
                proveedor.Calificacion = await ObtenerCalificacion(tiempoCalificacion, distanciaCalificacion, precioCalificacion, calidadCalificacion);
                _db.Proveedores.Update(proveedor);

                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private async Task<double> ObtenerCalificacion(int tiempoCalificacion, int distanciaCalificacion, int precioCalificacion, int calidadCalificacion)
        {
            var pesoTiempoCalificacion = await _parametroService.GetValorByParametro(Parametro.Tiempo);
            var pesoDistanciaCalificacion = await _parametroService.GetValorByParametro(Parametro.Distancia);
            var pesoPrecioCalificacion = await _parametroService.GetValorByParametro(Parametro.Precio);
            var pesoCalidadCalificacion = await _parametroService.GetValorByParametro(Parametro.Calidad);

            int total = pesoTiempoCalificacion + pesoDistanciaCalificacion + pesoPrecioCalificacion + pesoCalidadCalificacion;

            double media = (tiempoCalificacion * pesoTiempoCalificacion) 
                         + (distanciaCalificacion * pesoDistanciaCalificacion) 
                         + (precioCalificacion * pesoPrecioCalificacion) 
                         + (calidadCalificacion * pesoCalidadCalificacion) 
                         / total;

            double media2 = media / 10;

            return media2;
        }
        
        public async Task EnviarMailCompra(int IdCompra)
        {
            var compraRealizada = await _db.ComprasInsumos
                    .Include(x => x.CompraInsumoDetalles)
                        .ThenInclude(x => x.Insumo)
                        .ThenInclude(x => x.ProveedoresInsumo)
                    .Include(x => x.Proveedor)
                    .SingleAsync(x => x.Id == IdCompra);

            byte[] dataRow = Convert.FromBase64String(_Configuration.GetSection("EmailTemplates").GetSection("CompraItem")["EmailTableRow"]);
            string templateBaseRow = Encoding.UTF8.GetString(dataRow);

            string Maildetalles = "";
            foreach (var item in compraRealizada.CompraInsumoDetalles)
            {
                var row = templateBaseRow.Replace("@NombreItem", item.Insumo.Nombre)
                                         .Replace("@DescripcionItem", item.Insumo.Descripcion)
                                         .Replace("@UnidadesItem", item.Cantidad.ToString() + item.Insumo.Unidad)
                                         .Replace("@PrecioItem", item.Insumo.ProveedoresInsumo.Single(x => x.IdProveedor == compraRealizada.IdProveedor).Precio.ToString());
                Maildetalles += row;
            }


            byte[] dataMail = Convert.FromBase64String(_Configuration.GetSection("EmailTemplates").GetSection("CompraItem")["EmailBody"]);
            string templateBaseMail = Encoding.UTF8.GetString(dataMail);

            var template = templateBaseMail.Replace("@NombreProveedor", compraRealizada.Proveedor.Nombre)
                                           .Replace("@IdCompra", compraRealizada.Id.ToString())
                                           .Replace("@Cliente", "Fabrica LightFoot")
                                           .Replace("@Producto", "Insumos")
                                           .Replace("@Fecha", DateTime.Now.ToLongDateString())
                                           .Replace("@TableRows", Maildetalles)
                                           .Replace("@MontoTotal", compraRealizada.MontoTotal.ToString())
                                           .Replace("@Direccion", "4562 Hazy Panda Limits, Chair Crossing, Kentucky, US, 607898");

            await EmailSender.SendEmail($"Nueva Compra Orden #{compraRealizada.Id}", template, compraRealizada.Proveedor.Email);
        }

        public async Task<IEnumerable<ProveedorInsumoCuentaCorriente>> GetPagosRealizados(int IdCompra)
        {
            var pagos = await _db.ProveedoresInsumosCuentaCorriente.Where(x => x.IdCompraInsumo == IdCompra).ToListAsync();

            return pagos;
        }

    }
}
