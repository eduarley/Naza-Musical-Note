using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

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
}
