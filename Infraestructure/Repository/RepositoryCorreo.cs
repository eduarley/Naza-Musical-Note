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
    public class RepositoryCorreo : IRepositoryCorreo
    {
        public CorreoEmisor Edit(CorreoEmisor correo)
        {
            CorreoEmisor oCorreo = null;
            int retorno = 0;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //oCorreo = correo;
                        ctx.Entry(correo).State = EntityState.Modified;

                    retorno = ctx.SaveChanges();
                }

                return oCorreo;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
        }

        public CorreoEmisor GetCorreo()
        {
            CorreoEmisor correo = new CorreoEmisor();
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    //Permite traer las canciones que de una lista de Id
                    ctx.Configuration.LazyLoadingEnabled = false;
                    correo = ctx.CorreoEmisor.
                        Where(p => p.Id == 1)
                        .FirstOrDefault<CorreoEmisor>();

                }
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }

            return correo;
        }
    }
}
