using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceBitacora
    {
        List<Bitacora_RolServicio> GetBitacora_RolServicios();

        Bitacora_RolServicio GetBitacora_RolServicioById(int id);
        bool DeleteBitacora_RolServicioById(int id);
        bool DeleteAllBitacora_RolServicio();
        bool Recuperar(int idBitacora);
    }
}
