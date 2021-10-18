using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Aplicacion.Auth;
using Web.Site.Dtos;

namespace Web.Site.Areas
{
    public class UserLoginModel
    {
        public UserLoginDTO UserLoginDTO { get; set; }
        [Display(Name = "Sucursal")]
        public IList<DesplegableModel> Sucursales { get; set; }
    }
}
