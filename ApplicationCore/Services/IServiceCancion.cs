using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceCancion
    {
        Cancion GetCancionByID(int id);
        List<Cancion> GetCancionesActivas();
        List<Cancion> GetCanciones();
        List<Cancion> GetListaCancionesByID(int[] cancionesID);
        Cancion Save(Cancion cancion);
        bool DeteteCancion(int id);
    }
}
