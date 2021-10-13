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
                    ctx.Configuration.LazyLoadingEnabled = false;
                    Categoria oCategoria = ctx.Categoria.Where(a => a.Id == id).Include(e => e.Puesto).FirstOrDefault();
                    if (oCategoria != null)
                    {
                        foreach (var item in oCategoria.Puesto)
                        {
                            ctx.Database.ExecuteSqlCommand("update Usuario_RolServicio set estado = 0 where IdPuesto =" + item.Id);
                        }
                        ctx.Database.ExecuteSqlCommand("update puesto set estado = 0 where IdCategoria =" + oCategoria.Id);
                        oCategoria.Estado = false;
                        ctx.Entry(oCategoria).State = EntityState.Modified;
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
                        .Include("Puesto")
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

        public List<Categoria> GetCategoriasActivas()
        {
            List<Categoria> categorias = null;
            //List<Categoria> categoriasConPuestos = new List<Categoria>();
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    categorias = ctx.Categoria.Where(p => p.Estado).Include("Puesto").ToList();
                    
                }
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }

            //return categoriasConPuestos;
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
                    {
                        categoria.Estado = true;
                        ctx.Categoria.Add(categoria);
                    }
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
