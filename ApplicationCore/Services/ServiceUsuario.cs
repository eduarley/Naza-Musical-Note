using ApplicationCore.Utils;
using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceUsuario : IServiceUsuario
    {
        public List<Usuario> GetUsuariosActivos()
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            return repository.GetUsuariosActivos();
        }

        public Usuario Save(Usuario usuario)
        {
            if (usuario.Primer_ingreso)
            {
                string clave = Cryptography.EncrypthAES(usuario.Clave);
                usuario.Clave = clave;
            }

            IRepositoryUsuario repository = new RepositoryUsuario();
            return repository.Save(usuario);
        }

        public Usuario SaveClavePrimerIngreso(Usuario usuario)
        {
            if (usuario.Primer_ingreso)
            {
                string clave = Cryptography.EncrypthAES(usuario.Clave);
                usuario.Clave = clave;
                usuario.Primer_ingreso = false;
            }

            IRepositoryUsuario repository = new RepositoryUsuario();
            return repository.SaveCambioClave(usuario);
        }

        public Usuario SaveClaveCambio(Usuario usuario)
        {
                string clave = Cryptography.EncrypthAES(usuario.Clave);
                usuario.Clave = clave;

            IRepositoryUsuario repository = new RepositoryUsuario();
            return repository.SaveCambioClave(usuario);
        }

        public Usuario SaveGuardarToken(Usuario usuario)
        {
            string clave = Cryptography.EncrypthAES(usuario.Clave);
            usuario.Clave = clave;

            IRepositoryUsuario repository = new RepositoryUsuario();
            return repository.SaveGuardarToken(usuario);
        }

        public Usuario GetUsuario(string id, string password)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            // Se encripta el valor que viene y se compara con el valor encriptado en al BD.
            string crytpPasswd = Cryptography.EncrypthAES(password);
            return repository.GetUsuario(id, crytpPasswd);

        }

        public Rol GetRolByUsuario(int idRolUsuario)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            return repository.GetRolByUsuario(idRolUsuario);
        }

        public string GetSha256(string str)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            return repository.GetSha256(str);
        }

        public Usuario UsuarioARecuperar(string id, string correo)
        {
            string clave = "";
            IRepositoryUsuario repository = new RepositoryUsuario();
            Usuario usuario = repository.UsuarioARecuperar(id, correo);
            if (usuario != null)
            {
                clave = Cryptography.DecrypthAES(usuario.Clave);
                usuario.Clave = clave;
            }
            return usuario;
        }

        public Usuario ConsultarPorToken(string token)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            return repository.ConsultarPorToken(token);
        }

        public string AsignarClaveNewUser()
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            return repository.AsignarClaveNewUser();
        }

        public List<Usuario> GetUsuarios()
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            return repository.GetUsuarios();
        }

        public Usuario GetUsuarioByID(string id)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            return repository.GetUsuarioByID(id);
        }

        public void NoGuardarUsuario(string id)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            repository.NoGuardarUsuario(id);
        }

        public string GetEncryptedPass(string password)
        {
            return Cryptography.EncrypthAES(password);
        }

        public bool DeleteImagen(string id)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            return repository.DeleteImagen(id);
        }
    }
}
