using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Viewmodels
{
    public class ViewModelEnviarCorreo
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} es requerida")]
        [Display(Name = "Cédula")]
        public string Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} es requerido")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Formato de correo inválido")]
        [Display(Name = "Correo")]
        public string Correo { get; set; }
    }
}