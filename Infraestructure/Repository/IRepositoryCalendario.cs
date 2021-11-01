using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryCalendario
    {
        List<RolServicio> GetEvents();
        RolServicio SaveEvent(RolServicio rs, List<Usuario_RolServicio> puestosAsignados, Usuario usuario);
        bool DeleteEvent(int id, Usuario usuario);
        List<Puesto> GetPuestosPorCategoria(int id);
        int GetPrimerIDCategoria();
    }
}
