using System.ComponentModel.DataAnnotations;

namespace Core.Dominio.AggregatesModel
{
    public enum VentaTipo
    {
        [Display(Description = "Mayorista")]
        Mayorista = 1,
        [Display(Description = "Minorista")]
        Minorista = 2,
        [Display(Description = "Electronica")]
        Electronica = 3,
    }
    //VentaTipo - {Mayorista, Minorista, Electronica}
}
