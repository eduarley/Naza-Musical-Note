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
    public class RepositoryCancion : IRepositoryCancion
    {
        public List<Cancion> GetCancionesActivas()
        {
            List<Cancion> canciones = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    //Solamente canciones con estado activo
                    canciones = ctx.Cancion.Where(p => p.Estado == true).ToList();
                }
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }

            return canciones;
        }

        public List<Cancion> GetListaCancionesPorID(int [] cancionesID)
        {
            List<Cancion> canciones = new List<Cancion>();
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    foreach (var id in cancionesID)
                    {
                        //Permite traer las canciones que de una lista de Id
                        Cancion c = ctx.Cancion.Where(p => p.Id == id).FirstOrDefault();
                        canciones.Add(c);
                    }
                    
                }
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }

            return canciones;
        }
    }
}
