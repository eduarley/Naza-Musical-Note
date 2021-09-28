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
                    {
                        //if (usuario.Foto == null)
                        //{
                        //    usuario.Foto = Encoding.ASCII.GetBytes("0x89504E470D0A1A0A0000000D494844520000001E0000001E08060000003B30AEA20000000473424954080808087C086488000000097048597300000B1200000B1201D2DD7EFC00000016744558744372656174696F6E2054696D650030392F32302F31320690A1BE0000001C74455874536F6674776172650041646F62652046697265776F726B7320435336E8BCB28C000001CD494441544889ED96DF4BC25014C7BFE595CD39072E2E5E631233D88341BDF43FF517F427F4AFF5146141838469A43041CC98E50F5AD48360DE39F52C66BEF47DBCBB7C3F67E71CCEB97B1797575FD881F67701FD07FFA95892CB07450376A50CC18BF3B3E06D84D6B38FB6DFDB0EB852E638AB1D2F9D1BBA86B3DA314ADCC49DEBE1230C497EA4546BAA120B5D94E04538558B042583A9867645405395F4C0829B243300B00E793AE02C63602C43061B7A3E2D301D9AE4FE46F068324D040EDE46E98093980140307C4F0FDC68764866E3C9943C4848E06EEF051D8261DDF5485020C1E4AABB1E3EC24FD815B1F46D3C99A2EE7AE80F8274C046210FC18BD054058D66070F8D27B49E7D94B8896C76D6BDC170846EEF05C06C963BB685FE6B80FE20581B482C58701327CE11720B534870138FCD0E5A6D1FADB62FDDD754054ED58255E6F30060CF9AB2EE7AB10DB7177DFA086EE2FCD45919E9783245B737982F0343CF4BDB2AAA30FCC4F5ADBB045FFAE34D7339A72AB1755E25C632706C0B37F78FD2B9D4D559C660E81AD994AAB88C4860A3903EF4C75B9EE11298BAD27EA3A8B704CEE5B6078E66534E3571A5FD0AACAF4975D2159844516FB9C65B4C75D45BAEF1169B2BEABDB307FD37A22C82E270ED73F40000000049454E44AE426082");
                        //}

                        ctx.Usuario.Add(usuario);
                    }
                    else
                    {
                        ctx.Entry(usuario).State = EntityState.Modified;
                    }
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

        public Rol RolDelUsuario(int idRolUsuario)
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
            string[] lista = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
            string val = "";
            var random = new Random();
            for (int i = 0; i < 6; i++)
            {
                val += lista[random.Next(0, 34)];
            }
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

        public IEnumerable<Usuario> ListaUsuarios()
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
