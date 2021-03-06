
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
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(Bitacora_RolservicioMetadata))]
    public partial class Bitacora_RolServicio
    {

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Bitacora_RolServicio()
        {

            this.Bitacora_Cancion_RolServicio = new HashSet<Bitacora_Cancion_RolServicio>();

            this.Bitacora_Usuario_RolServicio = new HashSet<Bitacora_Usuario_RolServicio>();

        }


        public int IdBitacora { get; set; }

        public int IdRolServicio { get; set; }

        public string IdUsuario_Propietario { get; set; }

        public string Nombre_Usuario_Propietario { get; set; }

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



        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

        public virtual ICollection<Bitacora_Cancion_RolServicio> Bitacora_Cancion_RolServicio { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

        public virtual ICollection<Bitacora_Usuario_RolServicio> Bitacora_Usuario_RolServicio { get; set; }

    }

}
