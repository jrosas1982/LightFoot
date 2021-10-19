using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Dominio.AggregatesModel
{
    public class ArticuloCategoria
    {
        public ArticuloCategoria()
        {
            Activo = true;
        }
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Comentario { get; set; }
        public Boolean Activo { get; set; }

        public virtual ICollection<Articulo> Articulos { get; set; }
    }
}
