using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceBitacora_Sesion : IServiceBitacora_Sesion
    {
        IRepositoryBitacora_Sesion repository = new RepositoryBitacora_Sesion();
        public bool DeleteBitacorasSesion()
        {
            return repository.DeleteBitacorasSesion();
        }

        public List<Bitacora_Sesion> GetBitacorasSesion()
        {
            return repository.GetBitacorasSesion();
        }

        public bool Save(Usuario usuario)
        {
            return repository.Save(usuario);
        }
    }
}
