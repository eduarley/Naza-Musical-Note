using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryBitacora : IRepositoryBitacora
    {
        public List<Bitacora_RolServicio> GetBitacora_RolServicios()
        {
            List<Bitacora_RolServicio> lista = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //Solamente canciones con estado activo
                    lista = ctx.
                        Bitacora_RolServicio.
                        OrderByDescending(x => x.Fecha_modificacion).
                        Include(x => x.Bitacora_Usuario_RolServicio).
                        Include(x => x.Bitacora_Cancion_RolServicio).
                        ToList();
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


        public Bitacora_RolServicio GetBitacora_RolServicioById(int id)
        {
            Bitacora_RolServicio oBitacora = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oBitacora = ctx.
                        Bitacora_RolServicio.
                        Include(x => x.Bitacora_Usuario_RolServicio).
                        Include(x => x.Bitacora_Cancion_RolServicio).
                        Where(p => p.IdBitacora == id).
                        FirstOrDefault();
                }

                return oBitacora;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
        }


        public Bitacora_RolServicio Save(int idBitacora, Usuario usuarioModifica, string accion)
        {
            IRepositoryRolServicio repositoryRolServicio = new RepositoryRolServicio();
            Bitacora_RolServicio bitacora = null;
            int retorno = 0;
            try
            {


                RolServicio rs = repositoryRolServicio.GetRolServicioPorID(idBitacora);

                if(rs != null)
                {
                    //si no hay cambios, no guarde la bitacora
                    //if (!IsDifferent(rs))
                        //return null;


                    bitacora = new Bitacora_RolServicio();
                    bitacora = new Bitacora_RolServicio();
                    bitacora.IdRolServicio = rs.Id;
                    bitacora.Titulo = rs.Titulo;
                    bitacora.Descripcion = rs.Descripcion;
                    bitacora.FechaInicio = rs.FechaInicio;
                    bitacora.FechaFin = rs.FechaFin;
                    bitacora.Color = rs.Color;
                    bitacora.IdUsuario_Propietario = rs.IdUsuario_Propietario;
                    bitacora.Nombre_Usuario_Propietario = rs.Usuario.NombreCompleto;
                    bitacora.IsFullDay = rs.IsFullDay;
                    bitacora.Fecha_creacion = rs.Fecha_creacion;
                    bitacora.Estado = rs.Estado;
                    bitacora.IdUsuarioModifica = usuarioModifica.Id;
                    bitacora.NombreUsuarioModifica = usuarioModifica.NombreCompleto;
                    bitacora.Accion = accion;
                    bitacora.Fecha_modificacion = DateTime.Now;
                }


                foreach (var item in rs.Cancion)
                {
                    bitacora.Bitacora_Cancion_RolServicio.Add(new Bitacora_Cancion_RolServicio()
                    {
                        IdBitacora = bitacora.IdBitacora,
                        Nombre_cancion = item.Nombre,
                        IdCancion = item.Id
                    });

                }

                foreach (var item in rs.Usuario_RolServicio)
                {
                    if (item.Usuario != null)
                        bitacora.Bitacora_Usuario_RolServicio.Add(new Bitacora_Usuario_RolServicio()
                        {
                            IdBitacora = bitacora.IdBitacora,
                            Nombre_usuario = item.Usuario.NombreCompleto,
                            IdUsuario = item.IdUsuario,
                            IdPuesto = item.IdPuesto,
                            Nombre_puesto = item.Puesto.Descripcion,
                            Estado = item.Estado
                        });
                    else
                        //aqui cuando no se selecciona un usuario
                        bitacora.Bitacora_Usuario_RolServicio.Add(new Bitacora_Usuario_RolServicio()
                        {
                            IdUsuario = null,
                            IdPuesto = item.IdPuesto,
                            IdBitacora = bitacora.IdBitacora,
                            Nombre_puesto = item.Puesto.Descripcion,
                        });

                    
                }

                //eliminar el puesto y usuario de la lista porque hace intento de volverlos a guardar
                foreach (var item in rs.Usuario_RolServicio)
                {
                    item.Puesto = null;
                    item.Usuario = null;
                }


                using (MyContext ctx = new MyContext())
                {
                    var oBitacora = GetBitacora_RolServicioById(rs.Id);
                    if (oBitacora != null)
                    {
                        ctx.Entry(bitacora).State = EntityState.Modified;
                    }
                    else
                    {
                        ctx.Bitacora_RolServicio.Add(bitacora);
                    }

                    retorno = ctx.SaveChanges();
                }


                if (retorno >= 0)
                    bitacora = GetBitacora_RolServicioById(bitacora.IdRolServicio);

                return bitacora;
                


            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
        }

        public bool DeleteBitacora_RolServicioById(int id)
        {
            bool estado = false;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Database.ExecuteSqlCommand("delete from Bitacora_Usuario_RolServicio where IdBitacora = " + id);
                    ctx.Database.ExecuteSqlCommand("delete from Bitacora_Cancion_RolServicio where IdBitacora = " + id);
                    ctx.Database.ExecuteSqlCommand("delete from Bitacora_RolServicio where IdBitacora = "+ id);
                    if (ctx.SaveChanges() >= 0)
                        estado = true;

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

        public bool DeleteAllBitacora_RolServicio()
        {
            bool estado = false;
            try
            {
                List<Bitacora_RolServicio> lista = new List<Bitacora_RolServicio>();

                lista = GetBitacora_RolServicios();

                using (MyContext ctx = new MyContext())
                {
                    foreach (var item in lista)
                    {
                        ctx.Database.ExecuteSqlCommand("delete from Bitacora_Usuario_RolServicio where IdBitacora = " + item.IdBitacora);
                        ctx.Database.ExecuteSqlCommand("delete from Bitacora_Cancion_RolServicio where IdBitacora = " + item.IdBitacora);
                        ctx.Database.ExecuteSqlCommand("delete from Bitacora_RolServicio where IdBitacora = " + item.IdBitacora);

                    }
                    
                    if (ctx.SaveChanges() >= 0)
                        estado = true;

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

        public bool Recuperar(int idBitacora)
        {
            bool estado = false;
            IRepositoryBitacora repositoryBitacora = new RepositoryBitacora();
            IRepositoryRolServicio repositoryRolServicio = new RepositoryRolServicio();
            IRepositoryCancion repositoryCancion = new RepositoryCancion();
            IRepositoryUsuario repositoryUsuario = new RepositoryUsuario();
            IRepositoryPuesto repositoryPuesto = new RepositoryPuesto();
            

            try
            {

                Bitacora_RolServicio bitacora = repositoryBitacora.GetBitacora_RolServicioById(idBitacora);
                
                if(bitacora != null)
                {
                    RolServicio rs = repositoryRolServicio.GetRolServicioPorID(bitacora.IdRolServicio);
                    if(rs == null) //Si no existe RS, crearlo
                    {
                        rs = new RolServicio();

                        List<Cancion> canciones = new List<Cancion>();
                        List<Usuario_RolServicio> puestos = new List<Usuario_RolServicio>();

                        foreach (var item in bitacora.Bitacora_Cancion_RolServicio)
                        {
                            var c = repositoryCancion.GetCancionByID(item.IdCancion);
                            if (c != null)
                                canciones.Add(c);
                        }

                        foreach (var item in bitacora.Bitacora_Usuario_RolServicio)
                        {
                            var u = repositoryUsuario.GetUsuarioByID(item.IdUsuario);
                            var p = repositoryPuesto.GetPuestoById(item.IdPuesto);

                            if (u != null && p != null)
                            {
                                Usuario_RolServicio usuario_RolServicio = new Usuario_RolServicio()
                                {
                                    IdPuesto = p.Id,
                                    //Puesto = p,
                                    IdUsuario = u.Id,
                                    //Usuario = u,
                                    Estado = true,
                                    RolServicio = rs,
                                    IdRolServicio = rs.Id
                                };
                                puestos.Add(usuario_RolServicio);
                            }

                        }

                        //rs.Id =
                        //rs.Usuario = repositoryUsuario.GetUsuarioByID(bitacora.IdUsuario_Propietario);
                        rs.IdUsuario_Propietario = bitacora.IdUsuario_Propietario;
                        rs.Color = bitacora.Color;
                        rs.Descripcion = bitacora.Descripcion;
                        rs.Estado = bitacora.Estado;
                        rs.FechaFin = bitacora.FechaFin;
                        rs.FechaInicio = bitacora.FechaInicio;
                        rs.Fecha_creacion = bitacora.Fecha_creacion;
                        rs.Titulo = bitacora.Titulo;
                        rs.IsFullDay = bitacora.IsFullDay;
                        rs.Cancion = canciones;
                        rs.Usuario_RolServicio = puestos;

                        using(MyContext ctx = new MyContext())
                        {
                            ctx.RolServicio.Add(rs);
                            //AQUI SE DUPLICAN LAS CANCIONES
                            ctx.Database.ExecuteSqlCommand("delete from Bitacora_Usuario_RolServicio where IdBitacora = " + bitacora.IdBitacora);
                            ctx.Database.ExecuteSqlCommand("delete from Bitacora_Cancion_RolServicio where IdBitacora = " + bitacora.IdBitacora);
                            ctx.Database.ExecuteSqlCommand("delete from Bitacora_RolServicio where IdBitacora = " + bitacora.IdBitacora);
                            if (ctx.SaveChanges() >= 0)
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

        private bool IsDifferent(RolServicio rs)
        {
            var status = false;
            try
            {
                IRepositoryRolServicio repositoryRolServicio = new RepositoryRolServicio();

                RolServicio rsDataBase = repositoryRolServicio.GetRolServicioPorID(rs.Id);
                if(rsDataBase.Equals(rs))
                {
                    if (rsDataBase != rs)
                        status = true;
                }
                    
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
                
            }
            return status;
        }
    }
}
