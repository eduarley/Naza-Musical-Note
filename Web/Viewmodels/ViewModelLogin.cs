using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Viewmodels
{
    public class ViewModelLogin
    {
        [Required]
        [Display(Name = "cédula")]
        public string Id { get; set; }
        [Required]
        [Display(Name = "contraseña")]
        public string Clave { get; set; }
    }
}