using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryBitacora
    {
        List<Bitacora_RolServicio> GetBitacora_RolServicios();
        Bitacora_RolServicio Save(int idBitacora, Usuario usuarioModifica, string accion);
        Bitacora_RolServicio GetBitacora_RolServicioById(int id);
        bool DeleteBitacora_RolServicioById(int id);
        bool DeleteAllBitacora_RolServicio();
        //Bitacora_RolServicio RecuperarBitacora_RolServicio(Bitacora_RolServicio bitacora);
        bool Recuperar(int idBitacora);
    }
}
