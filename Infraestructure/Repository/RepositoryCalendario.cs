using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Infraestructure.Repository
{
    public class RepositoryCalendario : IRepositoryCalendario
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
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }

            return lista;
        }

        public RolServicio SaveEvent(RolServicio rs, List<Usuario_RolServicio> puestosAsignados, Usuario usuario)
        {
            IRepositoryBitacora repositoryBitacora = new RepositoryBitacora();
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
                                IdPuesto = item.IdPuesto,
                                Puesto = item.Puesto,
                                Usuario = item.Usuario
                                
                            });
                    }



                    if(rs.Id > 0)
                    {
                        //------------------------------------ UPDATE ------------------------------------\\

                        var v = ctx.RolServicio.Include("Cancion").Include("Usuario").Where(a => a.Id == rs.Id).FirstOrDefault();
                        if (v != null)
                        {
                            //v.IdUsuario_Propietario = rs.IdUsuario_Propietario;
                            v.Usuario_RolServicio = rs.Usuario_RolServicio;
                            //GUARDA LA BITACORA AL ACTUALIZAR
                            repositoryBitacora.Save(v.Id, usuario, "Modificado");
                            
                            v.Titulo = rs.Titulo;
                            v.FechaInicio = rs.FechaInicio;
                            v.FechaFin = rs.FechaFin;
                            v.Descripcion = rs.Descripcion;
                            v.IsFullDay = rs.IsFullDay;
                            v.Color = rs.Color;
                           
                            //v.Fecha_creacion = rs.Fecha_creacion;
                            v.Estado = rs.Estado;
                            v.Cancion = rs.Cancion;
                            
                        }

                        rs.Usuario = v.Usuario;
                        rs.IdUsuario_Propietario = v.IdUsuario_Propietario;
                        rs.Fecha_creacion = v.Fecha_creacion;
                        //Para al actualizar, no choquen las PK
                        ctx.Database.ExecuteSqlCommand("delete from usuario_rolservicio where IdRolServicio = " + rs.Id);


                        //Es necesario ponerlos en null para que no se guarden nuevos puestos y usuarios en la db
                        //al guardar la bitacora
                        foreach (var item in rs.Usuario_RolServicio)
                        {
                            item.Puesto = null;
                            item.Usuario = null;
                        }
                    }
                    else
                    {
                        //------------------------------------ SAVE ------------------------------------\\

                        //Es necesario ponerlos en null para que no se guarden nuevos puestos y usuarios en la db
                        //al guardar la bitacora
                        foreach (var item in rs.Usuario_RolServicio)
                        {
                            item.Puesto = null;
                            item.Usuario = null;
                        }
                        rs.Fecha_creacion = DateTime.Now;
                        ctx.RolServicio.Add(rs);
                    }


                    if (ctx.SaveChanges() >= 0)
                    {
                        ctx.Configuration.LazyLoadingEnabled = false;
                        //oRolServicio = ctx.RolServicio.Find(rs.Id);
                        oRolServicio = ctx.
                            RolServicio.
                            Include("Usuario_RolServicio").
                            Include("Usuario_RolServicio.Puesto").
                            Include("Usuario_RolServicio.Usuario").
                            Where(x => x.Id == rs.Id).
                            FirstOrDefault();
                    }
                        

                }
                return oRolServicio;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
        }


        public bool DeleteEvent(int id, Usuario usuario)
        {
            IRepositoryBitacora repositoryBitacora = new RepositoryBitacora();
            IRepositoryRolServicio repositoryRolServicio = new RepositoryRolServicio();
            bool estado = false;
            try
            {                
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //RolServicio rs = ctx.RolServicio.Include("Usuario").Include("Usuario_RolServicio").Where(a => a.Id == id).FirstOrDefault();
                    //RolServicio rs = repositoryRolServicio.GetRolServicioPorID(id);


                    
                    RolServicio rs = 
                        ctx.RolServicio.Include("Usuario")
                        .Include("Usuario_RolServicio")
                        .Include("Usuario_RolServicio.Puesto")
                        .Include("Usuario_RolServicio.Usuario")
                        .Include("Cancion")
                        .Where(p => p.Id == id)
                        .FirstOrDefault();
                    


                    if (rs != null)
                    {
                        repositoryBitacora.Save(rs.Id, usuario, "Eliminado");
                        ctx.Database.ExecuteSqlCommand("delete from usuario_rolservicio where IdRolServicio =" + rs.Id);
                        ctx.Database.ExecuteSqlCommand("delete from cancion_rolservicio where IdRolServicio =" + rs.Id);
                        ctx.Database.ExecuteSqlCommand("delete from rolservicio where Id =" + rs.Id);
                        //ctx.RolServicio.Remove(rs);



                        if (ctx.SaveChanges() >= 0)
                        {
                            estado = true;

                        }
                            
                    }
                }
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }


            return estado;
        }


        public List<Puesto> GetPuestosPorCategoria(int id)
        {
            List<Puesto> lista = new List<Puesto>();
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    List<Puesto> puestos = ctx.Puesto.Include("Categoria").Where(p => p.IdCategoria == id).ToList();
                    lista = puestos;
                }
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            return lista;
        }

        public int GetPrimerIDCategoria()
        {
            Categoria cat = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    cat = ctx.Categoria.Where(p => p.Estado).FirstOrDefault();
                    
                }
                if (cat != null)
                    return cat.Id;
                else
                    return -1;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
        }

        

    }
}
