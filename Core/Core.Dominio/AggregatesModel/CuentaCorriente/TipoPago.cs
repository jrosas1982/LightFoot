using System.ComponentModel.DataAnnotations;

namespace Core.Dominio.AggregatesModel
{
    public enum TipoPago
    {
        [Display(Description = "Pago por Cheque")]
        Cheque = 1,
        [Display(Description = "Pago por Tranferencia")]
        Transferencia = 2,
        [Display(Description = "Pago por Electronico")]
        Electronico = 3,
    }

    // cheque, transferencia, electrónico
}
