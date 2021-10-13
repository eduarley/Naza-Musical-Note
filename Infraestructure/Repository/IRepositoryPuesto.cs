using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryPuesto
    {
        List<Puesto> GetPuestos();
        List<Puesto> GetPuestosActivos();
        Puesto GetPuestoById(int? id);
        Puesto Save(Puesto puesto);
        bool DeletePuesto(int id);
    }
}
