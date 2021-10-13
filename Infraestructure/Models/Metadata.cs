using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Infraestructure.Utils;

namespace Infraestructure.Models
{
    internal partial class RolServicioMetadata
    {
        public int Id { get; set; }
        [Display(Name = "Propietario ID")]
        public string IdUsuario_Propietario { get; set; }
        [Display(Name = "Título")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} es requerido")]
        [StringLength(200, MinimumLength = 0, ErrorMessage = "Título debe ser menor a 200 caracteres")]
        public string Titulo { get; set; }
        [Display(Name = "Descripción")]
        [StringLength(600, MinimumLength = 0, ErrorMessage = "Descripción debe ser menor a 600 caracteres")]
        [DataType(DataType.MultilineText)]
        public string Descripcion { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:g}", ApplyFormatInEditMode = true)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} es requerida")]
        [Display(Name = "Fecha de inicio")]
        public System.DateTime FechaInicio { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:g}", ApplyFormatInEditMode = true)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} es requerida")]
        [Display(Name = "Fecha de cierre")]
        public Nullable<System.DateTime> FechaFin { get; set; }
        [Display(Name = "Fecha de creación")]
        public System.DateTime Fecha_creacion { get; set; }
        public string Color { get; set; }
        public bool IsFullDay { get; set; }
        public bool Estado { get; set; }

        [Display(Name = "Propietario")]
        public virtual Usuario Usuario { get; set; }

        [Display(Name = "Hora inicial")]
        public String HoraInicioSt { get; }

        [Display(Name = "Hora final")]
        public String HoraFinSt { get; }

        [Display(Name = "Fecha")]
        public String FechaSt { get; }

        [Display(Name = "Puestos")]
        public virtual ICollection<Usuario_RolServicio> Usuario_RolServicio { get; set; }
        [Display(Name = "Canciones")]
        public virtual ICollection<Cancion> Cancion { get; set; }
    }






    internal partial class CancionMetadata
    {
        [DataType(DataType.Date)]
        public System.DateTime Fecha_creacion { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} es requerido.")]
        public string Nombre { get; set; }

        [Display(Name = "Género")]
        public string Genero { get; set; }

        [Display(Name = "Enlace para la versión")]
        [RegularExpression(@"https:\/\/youtu.be[^' '\n\r]+",
         ErrorMessage = "Formato no permitido. Ejemplo de formato: https://youtu.be/......")]
        public string Url_version { get; set; }
    }

    internal partial class PuestoMetadata
    {

        [Required(ErrorMessage = "{0} es requerido.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} es requerida.")]
        [Display(Name = "Categoría")]
        public int IdCategoria { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} es requerida.")]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }
    }

    public partial class UsuarioMetadata
    {
        //FORMATO A CEDULA, 9 DIGITOS, UNICAMENTE DIGITOS
        [Display(Name = "Cédula")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} es requerida.")]
        public string Id { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} es requerido.")]
        public string Nombre { get; }

        [Display(Name = "Rol")]
        [Required(ErrorMessage = "{0} es requerido.")]
        public int IdRol { get; set; }

        [Display(Name = "Apellido paterno")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} es requerido.")]
        public string Apellido_paterno { get; set; }

        [Display(Name = "Apellido materno")]
        public string Apellido_materno { get; set; }

        [PasswordValidation]
        [DataType(DataType.Password)]
        public string Clave { get; set; }

        [Display(Name = "Teléfono")]
        //[DataType(DataType.PhoneNumber, ErrorMessage = "El {0} no tiene un formato válido.")]
        public string Telefono { get; set; }


        [Display(Name = "Correo electrónico")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} es requerido.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "El {0} no tiene un formato válido.")]
        [EmailAddress(ErrorMessage = "El correo no tiene un formato válido.")]
        public string Correo { get; set; }


        [Display(Name = "Fecha de creación")]
        [DataType(DataType.DateTime)]
        public System.DateTime Fecha_creacion { get; set; }



        [Display(Name = "Nombre")]
        public string NombreCompleto { get; }
    }

    



    public partial class CategoriaMetadata
    {
        [Display(Name = "Descripción")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} es requerida.")]
        public string Descripcion { get; set; }
    }



    
}
