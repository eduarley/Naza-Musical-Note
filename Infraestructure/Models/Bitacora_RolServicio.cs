//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Infraestructure.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Bitacora_RolServicio
    {
        public int Id { get; set; }
        public string IdUsuario_Propietario { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public System.DateTime FechaInicio { get; set; }
        public Nullable<System.DateTime> FechaFin { get; set; }
        public System.DateTime Fecha_creacion { get; set; }
        public string Color { get; set; }
        public bool IsFullDay { get; set; }
        public bool Estado { get; set; }
        public System.DateTime Fecha_modificacion { get; set; }
        public string NombreUsuarioModifica { get; set; }
        public string IdUsuarioModifica { get; set; }
        public string Accion { get; set; }
    }
}
