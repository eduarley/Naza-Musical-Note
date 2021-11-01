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
                    //Solamente canciones con estado activo
                    lista = ctx.Bitacora_RolServicio.ToList();
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
                    oBitacora = ctx.Bitacora_RolServicio.
                        Where(p => p.Id == id)
                        .FirstOrDefault();
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

        public Bitacora_RolServicio Save(RolServicio rs, Usuario usuarioModifica, string accion)
        {
            Bitacora_RolServicio bitacora = null;
            int retorno = 0;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    var oBitacora = GetBitacora_RolServicioById(rs.Id);

                    bitacora = new Bitacora_RolServicio();
                    bitacora.Id = rs.Id;
                    bitacora.Titulo = rs.Titulo;
                    bitacora.Descripcion = rs.Descripcion;
                    bitacora.FechaInicio = rs.FechaInicio;
                    bitacora.FechaFin = rs.FechaFin;
                    bitacora.Color = rs.Color;
                    bitacora.IdUsuario_Propietario = rs.IdUsuario_Propietario;
                    bitacora.IsFullDay = rs.IsFullDay;
                    bitacora.Fecha_creacion = rs.Fecha_creacion;
                    bitacora.Estado = rs.Estado;
                    bitacora.IdUsuarioModifica = usuarioModifica.Id;
                    bitacora.NombreUsuarioModifica = usuarioModifica.NombreCompleto;
                    bitacora.Accion = accion;
                    bitacora.Fecha_modificacion = DateTime.Now;

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
                    bitacora = GetBitacora_RolServicioById(bitacora.Id);

                return bitacora;
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
