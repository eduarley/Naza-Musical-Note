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
    public class RepositoryRolServicio : IRepositoryRolServicio
    {
        public RolServicio GetRolServicioPorID(int id)
        {
            RolServicio rs = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    rs = ctx.RolServicio.Include("Usuario")
                        .Include("Usuario_RolServicio")
                        .Include("Usuario_RolServicio.Puesto")
                        .Include("Usuario_RolServicio.Usuario")
                        .Include("Cancion")
                        .Where(p => p.Id == id)
                        .FirstOrDefault();
                   
                }
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }

            return rs;
        }

        public List<RolServicio> GetRolServicios()
        {
            List<RolServicio> rs = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    rs = ctx.RolServicio.Include("Usuario").OrderByDescending(x => x.FechaInicio).ToList();
                }
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }

            return rs;
        }

    }
}
