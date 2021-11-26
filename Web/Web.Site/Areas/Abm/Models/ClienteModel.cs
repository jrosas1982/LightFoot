using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Site.Areas.Abm
{
    public class ClienteModel
    {
        public int Id { get; set; }
        public string CUIT { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Contacto { get; set; }
        public string Email { get; set; }
        public Boolean Activo { get; set; }

        //public virtual ICollection<ClienteCuentaCorriente> ClienteCuentaCorriente { get; set; } = new HashSet<ClienteCuentaCorriente>();
    }
}
