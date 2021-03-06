using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceRol : IServiceRol
    {
        public Rol GetRolById(int id)
        {
            IRepositoryRol repository = new RepositoryRol();
            return repository.GetRolById(id);
        }

        public List<Rol> GetRoles()
        {
            IRepositoryRol repository = new RepositoryRol();
            return repository.GetRoles();
        }
    }
}
