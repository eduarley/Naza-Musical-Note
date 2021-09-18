using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryAgenda
    {
        List<RolServicio> GetEvents();
        RolServicio SaveEvent(RolServicio rs, List<Usuario_RolServicio> puestosAsignados);
        bool DeleteEvent(int id);
        List<Puesto> GetPuestosPorCategoria(int id);
        int GetPrimerIDCategoria();
    }
}
