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

        public bool DeteteCategoria(int id)
        {
            return repository.DeteteCategoria(id);
        }

        public Categoria GetCategoriaByID(int id)
        {
            return repository.GetCategoriaByID(id);
        }

        public List<Categoria> GetCategorias()
        {
            return repository.GetCategorias();
        }

        public Categoria Save(Categoria categoria)
        {
            return repository.Save(categoria);
        }
    }
}
