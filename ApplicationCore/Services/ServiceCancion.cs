using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceCancion : IServiceCancion
    {
        public List<Cancion> GetCancionesActivas()
        {
            IRepositoryCancion repository = new RepositoryCancion();
            return repository.GetCancionesActivas();
        }
        public List<Cancion> GetCanciones()
        {
            IRepositoryCancion repository = new RepositoryCancion();
            return repository.GetCanciones();
        }
        public List<Cancion> GetListaCancionesByID(int[] cancionesID)
        {
            IRepositoryCancion repository = new RepositoryCancion();
            return repository.GetListaCancionesByID(cancionesID);
        }

        public Cancion Save(Cancion cancion)
        {
            IRepositoryCancion repository = new RepositoryCancion();
            return repository.Save(cancion);
        }
        public Cancion GetCancionByID(int id)
        {
            IRepositoryCancion repository = new RepositoryCancion();
            return repository.GetCancionByID(id);
        }

        public bool DeteteCancion(int id)
        {
            IRepositoryCancion repository = new RepositoryCancion();
            return repository.DeteteCancion(id);
        }

        
    }
}
