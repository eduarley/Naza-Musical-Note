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

        public List<Categoria> GetCategoriasActivas()
        {
            return repository.GetCategoriasActivas();
        }

        public List<Categoria> GetCategoriasActivasConPuestos()
        {
            List<Categoria> lista = repository.GetCategoriasActivas();
            List<Categoria> listaCategoriasConPuestos = new List<Categoria>();
            foreach (var item in lista)
            {

                var estado = item.Puesto.Select(p => p.Estado);

                foreach (var status in estado)
                {
                    if(status)
                        listaCategoriasConPuestos.Add(item);
                }
                
            }
            return listaCategoriasConPuestos.Distinct().ToList();
        }

        public Categoria Save(Categoria categoria)
        {
            return repository.Save(categoria);
        }
    }
}
