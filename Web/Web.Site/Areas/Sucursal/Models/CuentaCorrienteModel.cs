using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Site.Areas 
{ 
    public class CuentaCorrienteModel
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public int IdVenta { get; set; }
        public decimal MontoPercibido { get; set; }
        public decimal TotalPagado { get; set; }
        public decimal DeudaTotal { get; set; }
        public bool PagoAcreditado { get; set; }
        public string NombreCliente { get; set; }


    }
}
