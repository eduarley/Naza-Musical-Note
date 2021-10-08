using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServicePuesto : IServicePuesto
    {
        IRepositoryPuesto repository = new RepositoryPuesto();
        public bool BorrarPuesto(int id)
        {
            return repository.BorrarPuesto(id);
        }

        public Puesto GetPuestoById(int? id)
        {
            return repository.GetPuestoById(id);
        }

        public List<Puesto> GetPuestos()
        {

            return repository.GetPuestos();
        }

        public Puesto Save(Puesto puesto)
        {
            return repository.Save(puesto);
        }
    }
}
