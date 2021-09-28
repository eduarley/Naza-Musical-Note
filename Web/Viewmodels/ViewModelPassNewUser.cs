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
        [Required]
        public string NewClave { get; set; }
        [Compare("NewClave")]
        [Required]
        public string ConfirmNewClave { get; set; }
    }
}