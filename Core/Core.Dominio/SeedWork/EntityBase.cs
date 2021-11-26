using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Dominio.SeedWork
{
    public class EntityBase
    {
        public EntityBase()
        {
            FechaCreacion = DateTime.Now;
        }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string CreadoPor { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string ModificadoPor { get; set; }
        public bool Eliminado { get; set; }
    }

}
