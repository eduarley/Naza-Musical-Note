﻿

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
using System.Data.Entity;
using System.Data.Entity.Infrastructure;


public partial class MyContext : DbContext
{
    public MyContext()
        : base("name=MyContext")
    {

    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        throw new UnintentionalCodeFirstException();
    }


    public virtual DbSet<Cancion> Cancion { get; set; }

    public virtual DbSet<Categoria> Categoria { get; set; }

    public virtual DbSet<CorreoEmisor> CorreoEmisor { get; set; }

    public virtual DbSet<Puesto> Puesto { get; set; }

    public virtual DbSet<Rol> Rol { get; set; }

    public virtual DbSet<RolServicio> RolServicio { get; set; }

    public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    public virtual DbSet<Usuario_RolServicio> Usuario_RolServicio { get; set; }

}

}

