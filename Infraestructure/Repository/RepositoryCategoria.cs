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
    public class RepositoryCategoria : IRepositoryCategoria
    {
        public bool DeteteCategoria(int id)
        {
            bool estado = false;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    Categoria oCategoria = ctx.Categoria.Where(a => a.Id == id).FirstOrDefault();
                    if (oCategoria != null)
                    {
                        ctx.Categoria.Remove(oCategoria);
                        if (ctx.SaveChanges() >= 0)
                            estado = true;
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

        public Categoria GetCategoriaByID(int id)
        {
            Categoria oCategoria = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oCategoria = ctx.Categoria.
                        Where(p => p.Id == id)
                        .FirstOrDefault<Categoria>();
                }

                return oCategoria;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
        }

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

        public Categoria Save(Categoria categoria)
        {
            Categoria oCategoria = null;
            int retorno = 0;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oCategoria = GetCategoriaByID(categoria.Id);
                    if (oCategoria == null)
                        ctx.Categoria.Add(categoria);
                    else
                        ctx.Entry(categoria).State = EntityState.Modified;

                    retorno = ctx.SaveChanges();
                }


                if (retorno >= 0)
                    oCategoria = GetCategoriaByID(categoria.Id);

                return oCategoria;
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
