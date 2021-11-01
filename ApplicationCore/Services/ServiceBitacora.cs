using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceBitacora : IServiceBitacora
    {
        IRepositoryBitacora repositoryBitacora = new RepositoryBitacora();

        public Bitacora_RolServicio GetBitacora_RolServicioById(int id)
        {
            return repositoryBitacora.GetBitacora_RolServicioById(id);
        }

        public List<Bitacora_RolServicio> GetBitacora_RolServicios()
        {
            return repositoryBitacora.GetBitacora_RolServicios();
        }

        
    }
}
