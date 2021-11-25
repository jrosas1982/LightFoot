using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Core.Dominio.AggregatesModel
{
    public enum UsuarioRol : int
    {
        [Category(Policies.IsAdmin)]
        [Display(GroupName = Policies.IsAdmin)]
        Administrador = 40,

        [Category(Policies.IsFabrica)]
        [Display(GroupName = Policies.IsFabrica)]
        Gerente = 30,
        [Category(Policies.IsFabrica)]
        [Display(GroupName = Policies.IsFabrica)]
        Supervisor = 20,
        [Category(Policies.IsTesorero)]
        [Display(GroupName = Policies.IsTesorero)]
        Tesorero = 25,
        //[Category(Policies.IsFabrica)]
        //[Display(GroupName = Policies.IsFabrica)]
        //Controlador = 80,
        [Category(Policies.IsFabrica)]
        [Display(GroupName = Policies.IsFabrica)]
        Operario = 50,

        [Category(Policies.IsSucursal)]
        [Display(GroupName = Policies.IsSucursal)]
        Encargado = 60,
        [Category(Policies.IsSucursal)]
        [Display(GroupName = Policies.IsSucursal)]
        Cajero = 70,
        [Category(Policies.IsSucursal)]
        [Display(GroupName = Policies.IsSucursal)]
        Vendedor = 10,
    }

    internal static class Policies
    {
        public const string IsGod = "IsGod";
        public const string IsFabrica = "IsFabrica";
        public const string IsSucursal = "IsSucursal";

        public const string IsAdmin = "IsAdmin";

        public const string IsGerente = "IsGerente";
        public const string IsSupervisor = "IsSupervisor";
        public const string IsTesorero = "IsTesorero";
        public const string IsControlador = "IsControlador";
        public const string IsOperario = "IsOperario";

        public const string IsEncargado = "IsEncargado";
        public const string IsCajero = "IsCajero";
        public const string IsVendedor = "IsVendedor";
    }
}
