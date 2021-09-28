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

        public string FormatURL(string url)
        {
            string UrlFormat = "";
            try
            {
                if (url != null)
                {
                    if (url.Contains("youtu.be"))
                        UrlFormat = url.Replace("youtu.be", "youtube.com/embed");
                    else
                        return url;
                }
                
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return UrlFormat;
        }
    }
}
