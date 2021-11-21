using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Aplicacion.Helpers;
using Core.Aplicacion.Interfaces;
using Core.Dominio;
using Core.Dominio.AggregatesModel;
using Core.Dominio.CoreModelHelpers;
using Core.Infraestructura;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Core.Aplicacion.Services
{
    public class ArticuloService : IArticuloService
    {
        private readonly AppDbContext _db;
        private readonly ILogger<ArticuloService> _logger;
        private readonly IConfiguration _Configuration;

        public ArticuloService(AppDbContext db, ILogger<ArticuloService> logger, IConfiguration configuration)
        {
            _db = db;
            _logger = logger;
            _Configuration = configuration;
        }

        public async Task<IEnumerable<Articulo>> GetArticulos()
        {
            var articulosList = await _db.Articulos
                .Where(x => !x.Eliminado)
                .Include(x => x.ArticuloCategoria)
                .OrderBy(x => x.ArticuloCategoria.Descripcion)
                .ToListAsync();
            _logger.LogInformation("Se buscaron los articulos");
            return articulosList;
        }

        public async Task<IEnumerable<Articulo>> GetArticulosFabrica()
        {
            var articulosList = await _db.Articulos
                .Where(x => !x.Eliminado)
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
                throw new ExcepcionControlada($"El articulo ya existe");

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
            articuloDb.PrecioMinorista = articulo.PrecioMinorista;
            articuloDb.PrecioMayorista = articulo.PrecioMayorista;
            articuloDb.Color = articulo.Color;

            _db.Update(articuloDb);
            await _db.SaveChangesAsync();

        }

        public async Task<bool> EliminarArticulo(Articulo articulo)
        {
            try
            {
                var articuloDb = await _db.Articulos.FindAsync(articulo.Id);

                articuloDb.Eliminado = true;

                _db.Articulos.Update(articuloDb);
                await _db.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar articulo: {ex.Message }");
                return false;
            }
        }

        public async Task<IEnumerable<ArticuloEstado>> GetArticuloEstados()
        {
            return await Task.FromResult(EnumExtensions.GetValues<ArticuloEstado>());
        }

        public async Task CambioPrecio(NuevoCambioPrecioModel modelo)
        {
            var articulosDb = _db.Articulos.Where(x => !x.Eliminado).Where(x => modelo.Detalle.Select(a => a.IdArticulo).Contains(x.Id));

         
             decimal precio;
            var articuloPrecioList = new List<ArticuloPrecio>();
            var tipoCambio = "Mayorista";
            foreach (var item in modelo.Detalle)
            {
                var articulo = articulosDb.Single(x => x.Id == item.IdArticulo);

                if (modelo.CambioPrecioMayorista)
                    precio = articulo.PrecioMayorista;
                else {
                    tipoCambio = "Minorista";
                    precio = articulo.PrecioMinorista;
                }
                var articuloPrecio = new ArticuloPrecio()
                {
                    IdArticulo = item.IdArticulo,
                    NombreArticulo = $"{articulo.CodigoArticulo} - {articulo.Nombre} - {articulo.Color} - Talle {articulo.TalleArticulo}",
                    PrecioAnterior = precio,
                    PrecioNuevo = item.NuevoPrecio
                };
                articuloPrecioList.Add(articuloPrecio);
            }

            foreach (var item in articulosDb)
            {
                var nuevoPrecio = modelo.Detalle.Single(x => x.IdArticulo == item.Id).NuevoPrecio;

                if (modelo.CambioPrecioMayorista)
                    item.PrecioMayorista = nuevoPrecio;
                else
                    item.PrecioMinorista = nuevoPrecio;

                _db.Articulos.Update(item);
         
            }
            await _db.SaveChangesAsync();
            await EnviarMailPrecioActualizado(articuloPrecioList, tipoCambio, modelo.Comentario, articuloPrecioList.Count());

        }

        private async Task EnviarMailPrecioActualizado(List<ArticuloPrecio> articulos, string tipoCambio, string comentario, int cantidadModificada)
        {
            //busqueda de supervisores

            var supervisores = _db.Usuarios.Where(x => x.UsuarioRol == UsuarioRol.Supervisor);

            byte[] dataRow = Convert.FromBase64String(_Configuration.GetSection("EmailTemplates").GetSection("CambioPrecio")["EmailTableRow"]);
            string templateBaseRow = Encoding.UTF8.GetString(dataRow);

            string Maildetalles = "";

            foreach (var item in articulos)
            {
                var row = templateBaseRow.Replace("@NombreArticulo", item.NombreArticulo)
                                         .Replace("@PrecioAnterior", item.PrecioAnterior.ToString())
                                         .Replace("@PrecioNuevo", item.PrecioNuevo.ToString()); 
                Maildetalles += row;
            }
            byte[] dataMail = Convert.FromBase64String(_Configuration.GetSection("EmailTemplates").GetSection("CambioPrecio")["EmailBody"]);
            string templateBaseMail = Encoding.UTF8.GetString(dataMail);



            var template = templateBaseMail.Replace("@TipoDeCambio", tipoCambio)
                                           .Replace("@cantMod", cantidadModificada.ToString())
                                           .Replace("@Comentario", comentario)
                                           .Replace("@TableRows", Maildetalles);


            foreach (var item in supervisores)
            {
                 var body = template.Replace("@NombreSupervisor!", item.NombreUsuario);
                 await EmailSender.SendEmail($"Nueva Modificación de precio ", body, item.Email);
            }
        }

        private class ArticuloPrecio
        {
            public int IdArticulo { get; set; }
            public string NombreArticulo { get; set; }
            public decimal PrecioAnterior { get; set; }
            public decimal PrecioNuevo { get; set; }
        }
    }
}
