using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceUsuario
    {
        List<Usuario> GetUsuariosActivos();
        Usuario GetUsuario(string id, string password);
        Usuario Save(Usuario usuario);
        Rol GetRolByUsuario(int idRolUsuario);
        string GetSha256(string str);
        Usuario UsuarioARecuperar(string id, string correo);
        Usuario ConsultarPorToken(string token);
        string AsignarClaveNewUser();
        List<Usuario> GetUsuarios();
        Usuario GetUsuarioByID(string id);
        void NoGuardarUsuario(string id);
        Usuario SaveClavePrimerIngreso(Usuario usuario);
        Usuario SaveClaveCambio(Usuario usuario);
        Usuario SaveGuardarToken(Usuario usuario);
        string GetEncryptedPass(string password);
        bool DeleteImagen(string id);
        bool ExisteId(string id);
    }
}
