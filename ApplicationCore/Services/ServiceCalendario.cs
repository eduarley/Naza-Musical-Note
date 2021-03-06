using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceCalendario : IServiceCalendario
    {

        public List<Usuario_RolServicio> Generar_Lista_Usuario_RolServicio(List<int> IdPuestos, List<string> IdUsuarios)
        {
            IRepositoryPuesto repositoryPuesto = new RepositoryPuesto();
            IRepositoryUsuario repositoryUsuario = new RepositoryUsuario();
            List<Usuario_RolServicio> lista = new List<Usuario_RolServicio>();
            var prueba = IdPuestos.Zip(IdUsuarios, (primero, segundo) => primero + ";" + segundo);
            string[] separado;
            foreach (var item in prueba)
            {
                Usuario_RolServicio urs = new Usuario_RolServicio();
                separado = item.Split(';');
                urs.IdPuesto = Convert.ToInt32(separado[0]);
                urs.IdUsuario = separado[1];
                urs.Puesto = repositoryPuesto.GetPuestoById(urs.IdPuesto);
                urs.Usuario = repositoryUsuario.GetUsuarioByID(urs.IdUsuario);
                lista.Add(urs);
            }
            return lista;
        }
        public bool DeleteEvent(int id, Usuario usuario)
        {
            IRepositoryCalendario repository = new RepositoryCalendario();
            return repository.DeleteEvent(id, usuario);
        }

        public List<RolServicio> GetEvents()
        {
            IRepositoryCalendario repository = new RepositoryCalendario();
            return repository.GetEvents();
        }

        public int GetPrimerIDCategoria()
        {
            IRepositoryCalendario repository = new RepositoryCalendario();
            return repository.GetPrimerIDCategoria();
        }

        public List<Puesto> GetPuestosPorCategoria(int id)
        {
            IRepositoryCalendario repository = new RepositoryCalendario();
            return repository.GetPuestosPorCategoria(id);
        }

        public RolServicio SaveEvent(RolServicio rs, List<Usuario_RolServicio> puestosAsignados, Usuario usuario)
        {
            IRepositoryCalendario repository = new RepositoryCalendario();
            return repository.SaveEvent(rs, puestosAsignados, usuario);
        }
    }
}
