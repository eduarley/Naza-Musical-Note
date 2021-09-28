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
        List<Usuario> GetIntegrantesActivos();
        Usuario GetUsuario(string id, string password);
        Usuario Save(Usuario usuario);
        Rol RolDelUsuario(int idRolUsuario);
        string GetSha256(string str);
        Usuario UsuarioARecuperar(string id, string correo);
        Usuario ConsultarPorToken(string token);
        string AsignarClaveNewUser();
        IEnumerable<Usuario> ListaUsuarios();
        Usuario GetUsuarioByID(string id);
        void NoGuardarUsuario(string id);
    }
}
