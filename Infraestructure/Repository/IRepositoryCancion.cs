using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryCancion
    {
        Cancion GetCancionByID(int id);
        List<Cancion> GetCancionesActivas();
        List<Cancion> GetCanciones();
        List<Cancion> GetListaCancionesByID(int[] cancionesID);
        bool DeteteCancion(int id);
        Cancion Save(Cancion cancion);
    }
}
