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
    
    public partial class Usuario_RolServicio
    {
        public string IdUsuario { get; set; }
        public int IdRolServicio { get; set; }
        public int IdPuesto { get; set; }
    
        public virtual Puesto Puesto { get; set; }
        public virtual RolServicio RolServicio { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
