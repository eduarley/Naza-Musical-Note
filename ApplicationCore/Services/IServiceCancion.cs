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
        List<Cancion> GetCancionesActivas();
        List<Cancion> GetCanciones();
        List<Cancion> GetListaCancionesPorID(int[] cancionesID);
        Cancion Save(Cancion cancion);
    }
}
