using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Viewmodels
{
    public class ViewModelChangePass
    {
        public string token { get; set; }
        [Display(Name = "Nueva contraseña")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} es requerido")]
        [StringLength(25, MinimumLength = 9, ErrorMessage = "La contraseña debe tener entre 9 y 25 caracteres")]
        public string NewClave { get; set; }
        [Compare(nameof(NewClave), ErrorMessage = "La contraseña debe coincidir en ambos campos")]
        [Display(Name = "La confirmación de la nueva contraseña")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} es requerido")]
        [StringLength(25, MinimumLength = 9, ErrorMessage = "La contraseña debe tener entre 9 y 25 caracteres")]
        public string ConfirmNewClave { get; set; }
    }
}