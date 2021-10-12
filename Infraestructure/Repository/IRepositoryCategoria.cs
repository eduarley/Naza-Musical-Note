using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryCategoria
    {
        List<Categoria> GetCategorias();
        List<Categoria> GetCategoriasActivas();
        Categoria GetCategoriaByID(int id);
        bool DeteteCategoria(int id);
        Categoria Save(Categoria categoria);

    }
}
