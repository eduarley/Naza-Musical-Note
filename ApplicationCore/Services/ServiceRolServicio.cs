using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceRolServicio : IServiceRolServicio
    {
        public RolServicio GetRolServicioPorID(int id)
        {
            IRepositoryRolServicio repository = new RepositoryRolServicio();
            return repository.GetRolServicioPorID(id);
        }

        public List<RolServicio> GetRolServicios()
        {
            IRepositoryRolServicio repository = new RepositoryRolServicio();
            return repository.GetRolServicios();
        }

      
    }
}
