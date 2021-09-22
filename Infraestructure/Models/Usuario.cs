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
    
    public partial class Usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuario()
        {
            this.RolServicio = new HashSet<RolServicio>();
            this.Usuario_RolServicio = new HashSet<Usuario_RolServicio>();
        }
    
        public string Id { get; set; }
        public int IdRol { get; set; }
        public string Nombre { get; set; }
        public string Apellido_paterno { get; set; }
        public string Apellido_materno { get; set; }
        public string Clave { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public byte[] Foto { get; set; }
        public string Token_recovery { get; set; }
        public System.DateTime Fecha_creacion { get; set; }
        public bool Primer_ingreso { get; set; }
        public bool Estado { get; set; }

        public string NombreCompleto
        {
            get { return Nombre + " " + Apellido_paterno; }
        }

        public virtual Rol Rol { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RolServicio> RolServicio { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Usuario_RolServicio> Usuario_RolServicio { get; set; }
    }
}
