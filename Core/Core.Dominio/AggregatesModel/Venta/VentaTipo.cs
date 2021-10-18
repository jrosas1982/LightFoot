using System.ComponentModel.DataAnnotations;

namespace Core.Dominio.AggregatesModel
{
    public enum VentaTipo
    {
        [Display(Description = "")]
        Mayorista = 1,
        [Display(Description = "")]
        Minorista = 2,
        [Display(Description = "")]
        Electronica = 3,
    }
    //VentaTipo - {Mayorista, Minorista, Electronica}
}
