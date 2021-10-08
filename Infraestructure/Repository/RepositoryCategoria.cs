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
    public class RepositoryCategoria : IRepositoryCategoria
    {
        public List<Categoria> GetCategorias()
        {
            List<Categoria> categorias = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    categorias = ctx.Categoria.Include("Puesto").ToList();
                }
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }

            return categorias;
        }

        public IEnumerable<Categoria> LlenarCombo()
        {
            try
            {
                // Forzar error
                // int x = 0;
                // x = 25 / x;


                IEnumerable<Categoria> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.Categoria.Include("Puesto").ToList<Categoria>();
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
