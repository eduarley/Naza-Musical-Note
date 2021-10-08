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
    public class RepositoryPuesto : IRepositoryPuesto
    {
        public bool BorrarPuesto(int id)
        {
            try
            {
                bool estado = false;
                using (MyContext ctx = new MyContext())
                {
                    Puesto puesto = GetPuestoById(id);
                    if (puesto != null)
                    {
                        ctx.Database.ExecuteSqlCommand("delete from Puesto where Id =" + puesto.Id);
                        ctx.Puesto.Remove(puesto);

                        if (ctx.SaveChanges() >= 0)
                            estado = true;
                    }
                }
                return estado;
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

        public Puesto GetPuestoById(int? id)
        {
            Puesto puesto = null;
            try
            {
                //string clave = "";
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    puesto = ctx.Puesto.
                              Include("Categoria").
                             Where(p => p.Id == id).
                             FirstOrDefault<Puesto>();
                }

                return puesto;
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

        public Puesto Save(Puesto puesto)
        {
            int retorno = 0;
            Puesto oPuesto = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oPuesto = GetPuestoById(puesto.Id);
                    if (oPuesto == null)
                    {
                        ctx.Puesto.Add(puesto);
                    }
                    else
                    {
                        ctx.Entry(puesto).State = EntityState.Modified;
                    }
                    retorno = ctx.SaveChanges();
                }

                if (retorno >= 0)
                    oPuesto = GetPuestoById(puesto.Id);

                return oPuesto;
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
