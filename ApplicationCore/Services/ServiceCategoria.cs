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
        IRepositoryCategoria repository = new RepositoryCategoria();
        public List<Categoria> GetCategorias()
        {
            return repository.GetCategorias();
        }

        public IEnumerable<Categoria> LlenarCombo()
        {
            return repository.LlenarCombo();
        }
    }
}
