
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

    [MetadataType(typeof(CorreoEmisorMetadata))]
    public partial class CorreoEmisor
    {

        public int Id { get; set; }

        public string Correo { get; set; }

        public string Clave { get; set; }

        public bool Estado { get; set; }

    }

}
