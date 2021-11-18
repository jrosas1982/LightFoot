using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dominio.CoreModelHelpers
{

    public class NuevoCambioPrecioModel
    {
        public string Comentario { get; set; }
        public bool CambioPrecioMayorista { get; set; }
        public ICollection<NuevoCambioPrecioDetalleModel> Detalle { get; set; } 

        public NuevoCambioPrecioModel()
        {
            Detalle = new List<NuevoCambioPrecioDetalleModel>();
        }

}

    public class NuevoCambioPrecioDetalleModel
    {
        public int IdArticulo { get; set; }
        public decimal NuevoPrecio { get; set; }

    }
}