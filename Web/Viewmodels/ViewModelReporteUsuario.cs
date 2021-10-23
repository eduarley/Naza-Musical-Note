using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Viewmodels
{
    public class ViewModelReporteUsuario
    {
        public string Id { get; set; }
        public string NombreCompleto { get; set; }
        public DateTime Fecha_Creacion { get; set; }
        public string Rol { get; set; }
    }
}