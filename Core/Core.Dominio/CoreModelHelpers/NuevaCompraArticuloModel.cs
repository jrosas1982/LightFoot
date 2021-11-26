using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Dominio.CoreModelHelpers
{
    public class NuevaCompraArticuloModel
    {
        public int IdArticulo { get; set; }
        public int IdProveedor { get; set; }
        public int Cantidad { get; set; }
        public string Comentario { get; set; }
    }
}
