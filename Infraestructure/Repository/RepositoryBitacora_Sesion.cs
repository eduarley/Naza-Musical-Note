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
    public class RepositoryBitacora_Sesion : IRepositoryBitacora_Sesion
    {


        public bool DeleteBitacorasSesion()
        {
            bool estado = false;
            try
            {
                using (MyContext ctx = new MyContext())
                {

                    ctx.Database.ExecuteSqlCommand("delete from Bitacora_Sesion");
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

       

        public List<Bitacora_Sesion> GetBitacorasSesion()
        {
            List<Bitacora_Sesion> lista = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.Bitacora_Sesion.OrderByDescending(x => x.Fecha).ToList();
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

        public bool Save(Usuario usuario)
        {
            var status = false;
            try
            {
                Bitacora_Sesion bitacora = new Bitacora_Sesion()
                {
                    IdUsuario = usuario.Id,
                    Fecha = DateTime.Now,
                    Descripcion = usuario.NombreCompleto + " ingresó al sistema con el rol de " + usuario.Rol.Descripcion
                };
                using (MyContext ctx = new MyContext())
                {
                    ctx.Bitacora_Sesion.Add(bitacora);
                    if (ctx.SaveChanges() >= 0)
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
