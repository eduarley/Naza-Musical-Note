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
    public class RepositoryReporte : IRepositoryReporte
    {
        public List<Usuario_RolServicio> GetServiciosByUsuario(string id)
        {
            List<Usuario_RolServicio> lista = null;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.Usuario_RolServicio
                        .Include("Usuario")
                        .Include("RolServicio")
                        .Include("Puesto")
                        .Where(p => p.IdUsuario == id)
                        .ToList();
                }
                return lista;

            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public int GetTotalRegistrosSesiones()
        {
            int total = 0;
            try
            {

                using (MyContext ctx = new MyContext())
                {

                    total = ctx.Bitacora_Sesion.Count();
                }
                return total;

            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public List<Usuario> GetUsuariosIngresadosByFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            List<Usuario> lista = null;
            try
            {
                
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.Usuario
                        .Include("Rol")
                        .Where(p => DbFunctions.TruncateTime(p.Fecha_creacion) >= fechaInicio && DbFunctions.TruncateTime(p.Fecha_creacion) <= fechaFin)
                        .ToList();
                }
                return lista;

            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }

        }
    }
}
