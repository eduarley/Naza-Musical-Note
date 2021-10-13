using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServicePuesto
    {
        List<Puesto> GetPuestos();
        List<Puesto> GetPuestosActivos();
        Puesto GetPuestoById(int? id);
        Puesto Save(Puesto puesto);
        bool DeletePuesto(int id);
    }
}
