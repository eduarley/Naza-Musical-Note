using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Viewmodels
{
    public class ViewModelLogin
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} es requerida")]
        [Display(Name = "Cédula")]
        public string Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} es requerida")]
        [Display(Name = "Contraseña")]
        public string Clave { get; set; }
    }
}