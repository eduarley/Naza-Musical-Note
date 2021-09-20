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
    public class RepositoryPuesto : IRepositoryPuesto
    {
        public List<Puesto> GetPuestos()
        {
            List<Puesto> puestos = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    puestos = ctx.Puesto.Include("Categoria").ToList();
                }
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }

            return puestos;
        }
    }
}
