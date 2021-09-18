using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Infraestructure.Repository
{
    public class RepositoryAgenda : IRepositoryAgenda
    {
        public List<RolServicio> GetEvents()
        {
            List<RolServicio> lista = new List<RolServicio>();
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    List<RolServicio> events = ctx.RolServicio.Include("Cancion").Include("Usuario_RolServicio").ToList();
                    lista = events;
                }
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Web.Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }

            return lista;
        }

        public RolServicio SaveEvent(RolServicio rs, List<Usuario_RolServicio> puestosAsignados)
        {
            RolServicio oRolServicio = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    List<Cancion> canciones = new List<Cancion>();

                    foreach (var item in rs.Cancion)
                    {
                        Cancion c = ctx.Cancion.Where(p => p.Id == item.Id).FirstOrDefault();
                        canciones.Add(c);
                    }
                    rs.Cancion = canciones;

                    foreach (var item in puestosAsignados)
                    {
                        if (item.IdUsuario != "")
                            rs.Usuario_RolServicio.Add(item);
                        else
                            //aqui cuando no se selecciona un usuario
                            rs.Usuario_RolServicio.Add(new Usuario_RolServicio()
                            {
                                IdUsuario = null,
                                IdRolServicio = item.IdRolServicio,
                                IdPuesto = item.IdPuesto
                            });
                    }

                    if(rs.Id > 0)
                    {
                        //Update
                        var v = ctx.RolServicio.Include("Cancion").Include("Usuario").Where(a => a.Id == rs.Id).FirstOrDefault();
                        if (v != null)
                        {
                            v.Titulo = rs.Titulo;
                            v.FechaInicio = rs.FechaInicio;
                            v.FechaFin = rs.FechaFin;
                            v.Descripcion = rs.Descripcion;
                            v.IsFullDay = rs.IsFullDay;
                            v.Color = rs.Color;
                            v.IdUsuario_Propietario = rs.IdUsuario_Propietario;
                            v.Fecha_creacion = rs.Fecha_creacion;
                            v.Estado = rs.Estado;
                            v.Cancion = rs.Cancion;
                            v.Usuario_RolServicio = rs.Usuario_RolServicio;
                        }
                        //Para al actualizar, no choquen las PK
                        ctx.Database.ExecuteSqlCommand("delete from usuario_rolservicio where IdRolServicio = " + rs.Id);

                    }
                    else
                    {
                        //save
                        ctx.RolServicio.Add(rs);
                    }


                    if (ctx.SaveChanges() >= 0)
                        oRolServicio = ctx.RolServicio.Find(rs.Id);

                }
                return oRolServicio;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Web.Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
        }


        public bool DeleteEvent(int id)
        {
            bool estado = false;
            using (MyContext ctx = new MyContext())
            {
                var rs = ctx.RolServicio.Where(a => a.Id == id).FirstOrDefault();
                if (rs != null)
                {
                    ctx.Database.ExecuteSqlCommand("delete from usuario_rolservicio where IdRolServicio =" + rs.Id);
                    ctx.Database.ExecuteSqlCommand("delete from cancion_rolservicio where IdRolServicio =" + rs.Id);
                    ctx.RolServicio.Remove(rs);

                    if (ctx.SaveChanges() >= 0)
                        estado = true;
                }
            }
            return estado;
        }


        public List<Puesto> GetPuestosPorCategoria(int id)
        {
            List<Puesto> lista = new List<Puesto>();
            using (MyContext ctx = new MyContext())
            {
                List<Puesto> puestos = ctx.Puesto.Include("Categoria").Where(p => p.IdCategoria == id).ToList();
                lista = puestos;
            }
            return lista;
        }

        public int GetPrimerIDCategoria()
        {
            Categoria cat = null;
            using (MyContext ctx = new MyContext())
            {
                cat = ctx.Categoria.FirstOrDefault();
            }
            if (cat != null)
                return cat.Id;
            else
                return -1;
        }



    }
}
