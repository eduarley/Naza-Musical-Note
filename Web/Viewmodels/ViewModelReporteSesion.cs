using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Viewmodels
{
    public class ViewModelReporteSesion
    {
        public string IdUsuario { get; set; }

        public string Descripcion { get; set; }

        public System.DateTime Fecha { get; set; }
    }
}