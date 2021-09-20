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
    public class RepositoryUsuario : IRepositoryUsuario
    {
        public List<Usuario> GetIntegrantesActivos()
        {
            List<Usuario> usuarios = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    //Solamente usuarios rol integrante y estado activo
                    usuarios = ctx.Usuario.Include("Rol").Where(p => p.IdRol != 1 && p.Estado == true).ToList();
                }
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }

            return usuarios;
        }
    }
}
