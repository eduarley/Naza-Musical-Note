using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceAgenda : IServiceAgenda
    {

        public List<Usuario_RolServicio> Generar_Lista_Usuario_RolServicio(List<int> IdPuestos, List<string> IdUsuarios)
        {
            List<Usuario_RolServicio> lista = new List<Usuario_RolServicio>();
            var prueba = IdPuestos.Zip(IdUsuarios, (primero, segundo) => primero + ";" + segundo);
            string[] separado;
            foreach (var item in prueba)
            {
                Usuario_RolServicio urs = new Usuario_RolServicio();
                separado = item.Split(';');
                urs.IdPuesto = Convert.ToInt32(separado[0]);
                urs.IdUsuario = separado[1];
                lista.Add(urs);
            }
            return lista;
        }
        public bool DeleteEvent(int id)
        {
            IRepositoryAgenda repository = new RepositoryAgenda();
            return repository.DeleteEvent(id);
        }

        public List<RolServicio> GetEvents()
        {
            IRepositoryAgenda repository = new RepositoryAgenda();
            return repository.GetEvents();
        }

        public int GetPrimerIDCategoria()
        {
            IRepositoryAgenda repository = new RepositoryAgenda();
            return repository.GetPrimerIDCategoria();
        }

        public List<Puesto> GetPuestosPorCategoria(int id)
        {
            IRepositoryAgenda repository = new RepositoryAgenda();
            return repository.GetPuestosPorCategoria(id);
        }

        public RolServicio SaveEvent(RolServicio rs, List<Usuario_RolServicio> puestosAsignados)
        {
            IRepositoryAgenda repository = new RepositoryAgenda();
            return repository.SaveEvent(rs, puestosAsignados);
        }
    }
}
