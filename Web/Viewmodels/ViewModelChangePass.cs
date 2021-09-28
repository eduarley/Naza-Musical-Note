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
        [Required]
        [Display(Name = "nueva contraseña")]
        public string NewClave { get; set; }
        [Compare("NewClave")]
        [Required]
        [Display(Name = "confirmar nueva contraseña")]
        public string ConfirmNewClave { get; set; }
    }
}