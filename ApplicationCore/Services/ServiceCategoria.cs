using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceCategoria : IServiceCategoria
    {
        public List<Categoria> GetCategorias()
        {
            IRepositoryCategoria repository = new RepositoryCategoria();
            return repository.GetCategorias();
        }
    }
}
