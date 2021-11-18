using System;
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
    public class ArticuloService : IArticuloService
    {
        private readonly AppDbContext _db;
        private readonly ILogger<ArticuloService> _logger;

        public ArticuloService(AppDbContext db, ILogger<ArticuloService> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<IEnumerable<Articulo>> GetArticulos()
        {
            var articulosList = await _db.Articulos
                .Include(x => x.ArticuloCategoria)
                .OrderBy(x => x.ArticuloCategoria.Descripcion)
                .ToListAsync();
            _logger.LogInformation("Se buscaron los articulos");
            return articulosList;
        }

        public async Task<IEnumerable<Articulo>> GetArticulosFabrica()
        {
            var articulosList = await _db.Articulos
                .Where(x => x.ProveedoresArticulo.Any(z => z.Proveedor.EsFabrica == true))
                .Include(x => x.ArticuloCategoria)
                .OrderBy(x => x.ArticuloCategoria.Descripcion)
                .ToListAsync();
            _logger.LogInformation("Se buscaron los articulos");
            return articulosList;
        }

        public async Task<Articulo> BuscarPorId(int IdArticulo)
        {
            var articulo = await _db.Articulos.FindAsync(IdArticulo);
            return articulo;
        }

        public async Task CrearArticulo(Articulo articulo)
        {
            if (_db.Articulos.Any(x => x.Nombre == articulo.Nombre && x.Color == articulo.Color && x.TalleArticulo == articulo.TalleArticulo))
                throw new Exception($"El articulo ya existe");

            //if (string.IsNullOrEmpty(usuario.Contraseña))
            //    throw new Exception($"La contraeña no puede estar vacia");           

            _db.Articulos.Add(articulo);
            await _db.SaveChangesAsync();
            _logger.LogInformation($"Se creó el articulo nombre: {articulo.Nombre}");
        }

        public async Task EditarArticulo(Articulo articulo)
        {
            //if (string.IsNullOrEmpty(usuario.Contraseña))
            //    throw new Exception($"La contraeña no puede estar vacia");

            var articuloDb = await _db.Articulos.FindAsync(articulo.Id);

            articuloDb.CodigoArticulo = articulo.CodigoArticulo;
            articuloDb.Nombre = articulo.Nombre;
            articuloDb.ArticuloEstado = articulo.ArticuloEstado;
            articuloDb.TalleArticulo = articulo.TalleArticulo;
            articuloDb.Descripcion = articulo.Descripcion;
            articuloDb.Color = articulo.Color;

            _db.Update(articuloDb);
            await _db.SaveChangesAsync();

        }

        public async Task<bool> EliminarArticulo(Articulo articulo)
        {
            try
            {
                var articuloDb = await _db.Articulos.FindAsync(articulo.Id);

                _db.Remove(articuloDb);
                await _db.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al CambioPrecio: {ex.Message }");
                return false;
            }
        }

        public async Task<IEnumerable<ArticuloEstado>> GetArticuloEstados()
        {
            return await Task.FromResult(EnumExtensions.GetValues<ArticuloEstado>());
        }

        public async Task CambioPrecio(IEnumerable<Articulo> articulos)
        {
            try
            {
                foreach (var item in articulos)
                {
                    _db.Update(item);
                    await _db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al CambioPrecio: {ex.Message }");
                throw;
            }
      
        }

        public async Task EnviarMailPrecioActualizado()
        {
            //var compraRealizada = await _db.ComprasInsumos
            //        .Include(x => x.CompraInsumoDetalles)
            //            .ThenInclude(x => x.Insumo)
            //            .ThenInclude(x => x.ProveedoresInsumo)
            //        .Include(x => x.Proveedor)
            //        .SingleAsync(x => x.Id == IdCompra);

            //byte[] dataRow = Convert.FromBase64String(_Configuration.GetSection("EmailTemplates").GetSection("CompraItem")["EmailTableRow"]);
            //string templateBaseRow = Encoding.UTF8.GetString(dataRow);

            //string Maildetalles = "";
            //foreach (var item in compraRealizada.CompraInsumoDetalles)
            //{
            //    var row = templateBaseRow.Replace("@NombreItem", item.Insumo.Nombre)
            //                             .Replace("@DescripcionItem", item.Insumo.Descripcion)
            //                             .Replace("@UnidadesItem", item.Cantidad.ToString() + item.Insumo.Unidad)
            //                             .Replace("@PrecioItem", item.Insumo.ProveedoresInsumo.Single(x => x.IdProveedor == compraRealizada.IdProveedor).Precio.ToString());
            //    Maildetalles += row;
            //}


            //byte[] dataMail = Convert.FromBase64String(_Configuration.GetSection("EmailTemplates").GetSection("CompraItem")["EmailBody"]);
            //string templateBaseMail = Encoding.UTF8.GetString(dataMail);

            //var template = templateBaseMail.Replace("@NombreProveedor", compraRealizada.Proveedor.Nombre)
            //                               .Replace("@IdCompra", compraRealizada.Id.ToString())
            //                               .Replace("@Cliente", "Fabrica LightFoot")
            //                               .Replace("@Producto", "Insumos")
            //                               .Replace("@Fecha", DateTime.Now.ToLongDateString())
            //                               .Replace("@TableRows", Maildetalles)
            //                               .Replace("@MontoTotal", compraRealizada.MontoTotal.ToString())
            //                               .Replace("@Direccion", "4562 Hazy Panda Limits, Chair Crossing, Kentucky, US, 607898");

            //await EmailSender.SendEmail($"Nueva Compra Orden #{compraRealizada.Id}", template, compraRealizada.Proveedor.Email);
            await EmailSender.SendEmail("","");
        }
    }
}
