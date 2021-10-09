using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryUsuario
    {
        List<Usuario> GetIntegrantesActivos();
        Usuario Save(Usuario usuario);
        Usuario GetUsuario(string id, string password);
        Rol GetRolByUsuario(int idRolUsuario);
        string GetSha256(string str);
        Usuario UsuarioARecuperar(string id, string correo);
        Usuario ConsultarPorToken(string token);
        string AsignarClaveNewUser();
        List<Usuario> GetUsuarios();
        Usuario GetUsuarioByID(string id);
        void NoGuardarUsuario(string id);
    }
}
