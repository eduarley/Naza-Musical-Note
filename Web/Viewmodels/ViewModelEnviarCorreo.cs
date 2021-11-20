using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Viewmodels
{
    public class ViewModelEnviarCorreo
    {
        [RegularExpression(@"[0-9]{9,12}", ErrorMessage = "Debe contener mínimo 9 y máximo 12 dígitos numéricos sin guiones. Ejemplo: 101110111")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "La {0} es requerida")]
        [Display(Name = "Cédula")]
        public string Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "El {0} es requerido")]
        //[RegularExpression(@"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$", ErrorMessage = "Formato de correo inválido")]
        [RegularExpression(@"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$", ErrorMessage = "Formato de correo inválido")]
        [Display(Name = "Correo")]
        public string Correo { get; set; }
    }
}