using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Viewmodels
{
    public class ViewModelEnviarCorreo
    {
        [Required]
        [Display(Name = "cédula")]
        public string Id { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "correo")]
        public string Correo { get; set; }
    }
}