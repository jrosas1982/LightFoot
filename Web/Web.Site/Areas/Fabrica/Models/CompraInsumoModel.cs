using Core.Dominio.AggregatesModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Site.Dtos;

namespace Web.Site.Areas
{
    public class CompraInsumoModel : CompraInsumo
    {
        public ICollection<CompraInsumoDetalle> CompraInsumoDetalles { get; set; } = new List<CompraInsumoDetalle>();
        public IEnumerable<SelectListItem> Insumos { get; set; }
        public IEnumerable<SelectListItem> Proveedores { get; set; }
        public CompraInsumoDetalle CompraInsumoDetalle { get; set; }

        public CompraInsumoModel()
        {

        }

        public CompraInsumoModel(CompraInsumo compraInsumo)
        {
            Id = compraInsumo.Id;
            IdProveedor = compraInsumo.IdProveedor;
            Recibido = compraInsumo.Recibido;
            Pagado = compraInsumo.Pagado;
            NroRemito = compraInsumo.NroRemito;
            MontoTotal = compraInsumo.MontoTotal;
        }
    }
}
