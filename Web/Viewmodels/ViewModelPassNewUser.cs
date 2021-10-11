using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Viewmodels
{
    public class ViewModelPassNewUser
    {
        public string IdUsuario { get; set; }
        [Display(Name = "La nueva contraseña")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} es requerido")]
        [StringLength(200, MinimumLength = 9, ErrorMessage = "La contraseña debe tener 9 o más caracteres")]
        public string NewClave { get; set; }
        [Display(Name = "La confirmación de la nueva contraseña")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} es requerido")]
        [Compare(nameof(NewClave), ErrorMessage = "La contraseña debe coincidir en ambos campos")]
        [StringLength(200, MinimumLength = 9, ErrorMessage = "La contraseña debe tener 9 o más caracteres")]
        public string ConfirmNewClave { get; set; }
    }
}