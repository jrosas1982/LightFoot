using System.Collections.Generic;
using Core.Dominio.AggregatesModel;

namespace Web.Site.Areas.Abm
{
    public class UsuarioModel
    {
        public Usuario Usuario { get; set; }
        public IList<UsuarioRol> usuarioRoles { get; set; }
    }
}
