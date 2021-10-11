using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Security.Cryptography;
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

        public Usuario GetUsuarioByID(string id)
        {
            Usuario usuario = null;
            try
            {
                //string clave = "";
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    usuario = ctx.Usuario.
                              Include("Rol").
                             Where(p => p.Id == id).
                             FirstOrDefault<Usuario>();
                }
                //clave = Cryptography.DecrypthAES(usuario.Clave);
                //usuario.Clave = clave;

                return usuario;
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

        public Usuario Save(Usuario usuario)
        {
            int retorno = 0;
            Usuario oUsuario = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oUsuario = GetUsuarioByID(usuario.Id);
                    if (oUsuario == null)                 
                        ctx.Usuario.Add(usuario);
                    else
                        ctx.Entry(usuario).State = EntityState.Modified;

                    retorno = ctx.SaveChanges();
                }

                if (retorno >= 0)
                    oUsuario = GetUsuarioByID(usuario.Id);

                return oUsuario;
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



        public Usuario GetUsuario(string id, string password)
        {

            Usuario oUsuario = null;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oUsuario = ctx.Usuario.
                                 Where(p => p.Id.Equals(id) && p.Clave == password).
                                 FirstOrDefault<Usuario>();
                }

                if (oUsuario != null)
                    oUsuario = GetUsuarioByID(id);

                return oUsuario;
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

        public Rol GetRolByUsuario(int idRolUsuario)
        {
            try
            {
                Rol oRol = null;
                using (MyContext ctx = new MyContext())
                {
                    oRol = ctx.Rol.Where(p => p.Id.Equals(idRolUsuario)).FirstOrDefault<Rol>();
                }
                return oRol;
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

        public string GetSha256(string str)
        {
            try
            {
                SHA256 sha256 = SHA256Managed.Create();
                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] stream = null;
                StringBuilder sb = new StringBuilder();
                stream = sha256.ComputeHash(encoding.GetBytes(str));
                for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
                return sb.ToString();
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public Usuario UsuarioARecuperar(string id, string correo)
        {
            try
            {
                Usuario oUsuario = null;
                using (MyContext ctx = new MyContext())
                {
                    oUsuario = ctx.Usuario.
                                 Where(p => p.Id.Equals(id) && p.Correo.Equals(correo)).
                                 FirstOrDefault<Usuario>();
                }
                return oUsuario;
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

        public string AsignarClaveNewUser()
        {
            string[] listaMinusculas = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
            string[] listaNumeros = { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            string[] listaMayusculas = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            string[] listaSimbolos = { "!", "*", "?", "¿", "¡", "-" };
            string val = "";
            var random = new Random();
            val = "";
            for (int i = 0; i < 2; i++)
            {
                val += listaMinusculas[random.Next(0, 25)];
            }

            for (int i = 0; i < 3; i++)
            {
                val += listaNumeros[random.Next(0, 8)];
            }

            for (int i = 0; i < 3; i++)
            {
                val += listaMayusculas[random.Next(0, 25)];
            }

            val += listaSimbolos[random.Next(0, 5)];

            return val;
        }

        public Usuario ConsultarPorToken(string token)
        {
            try
            {
                Usuario oUsuario = null;
                using (MyContext ctx = new MyContext())
                {
                    oUsuario = ctx.Usuario.Where(p => p.Token_recovery == token).FirstOrDefault();
                }
                return oUsuario;
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

        public List<Usuario> GetUsuarios()
        {
            try
            {
                IEnumerable<Usuario> usuarios = null;
                using (MyContext ctx = new MyContext())
                {
                    //usuarios = ctx.Usuario.Include(u => u.Rol);
                    usuarios = ctx.Usuario.Include("Rol").ToList<Usuario>();
                }

                return usuarios.ToList();
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

        public void NoGuardarUsuario(string id)
        {
            try
            {
                Usuario oUser = GetUsuarioByID(id);
                using (MyContext ctx = new MyContext())
                {
                    ctx.Usuario.Attach(oUser);
                    ctx.Usuario.Remove(oUser);
                    ctx.SaveChanges();
                }
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
