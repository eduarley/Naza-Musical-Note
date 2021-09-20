using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceCalendario
    {
        List<Usuario_RolServicio> Generar_Lista_Usuario_RolServicio(List<int> IdPuestos, List<string> IdUsuarios);
        List<RolServicio> GetEvents();
        RolServicio SaveEvent(RolServicio rs, List<Usuario_RolServicio> puestosAsignados);
        bool DeleteEvent(int id);
        List<Puesto> GetPuestosPorCategoria(int id);
        int GetPrimerIDCategoria();
    }
}
