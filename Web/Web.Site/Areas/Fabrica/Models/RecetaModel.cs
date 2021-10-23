using Core.Dominio.AggregatesModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Site.Areas.Fabrica.Models
{
    public class RecetaModel : Receta
    {
        public RecetaDetalle RecetaDetalle { get; set; }

        public virtual IEnumerable<Receta> Recetas { get; set; }

        public RecetaModel()
        {

        }
        public RecetaModel(Receta receta , IEnumerable<RecetaDetalle> recetaDetalle)
        {
            Id = receta.Id;
            IdArticulo = receta.IdArticulo;
            Activo = receta.Activo;
            RecetaDetalles = recetaDetalle;
        }
    }
}
