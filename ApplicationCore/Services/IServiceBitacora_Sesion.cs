using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceBitacora_Sesion
    {
        List<Bitacora_Sesion> GetBitacorasSesion();
        bool DeleteBitacorasSesion();
        bool Save(Usuario usuario);
    }
}
