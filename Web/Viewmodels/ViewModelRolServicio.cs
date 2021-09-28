using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Viewmodels
{
    public class ViewModelRolServicio
    {
        public int Id { get; set; }
        public string IdUsuario_Propietario { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime FechaInicio { get; set; }
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime FechaFin { get; set; }
        public System.DateTime Fecha_creacion { get; set; }
        public string Color { get; set; }
        public bool IsFullDay { get; set; }
        public bool Estado { get; set; }
    }
}