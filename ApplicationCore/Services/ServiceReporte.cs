using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceReporte : IServiceReporte
    {
        IRepositoryReporte repository = new RepositoryReporte();

        public bool DeleteBitacorasSesion()
        {
            return repository.DeleteBitacorasSesion();
        }

        public List<Bitacora_Sesion> GetBitacorasSesion()
        {
            return repository.GetBitacorasSesion();
        }
    }
}
