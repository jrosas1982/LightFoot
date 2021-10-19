using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Core.Dominio.SeedWork;

namespace Core.Dominio.AggregatesModel
{
    public class ArticuloCategoria : EntityBase
    {
        public ArticuloCategoria()
        {
            Activo = true;
        }
        [Key]
        public int Id { get; set; }
        [Required]
        public string Descripcion { get; set; }
        public string Comentario { get; set; }
        public Boolean Activo { get; set; }

        public virtual ICollection<Articulo> Articulos { get; set; } = new HashSet<Articulo>();
    }
}
