using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceReporte : IServiceReporte
    {
        IRepositoryReporte repository = new RepositoryReporte();

        public List<Usuario_RolServicio> GetServiciosByUsuario(string id)
        {
            List<Usuario_RolServicio> lista = repository.GetServiciosByUsuario(id);
            //AGRUPAR POR FECHAS
            List<Usuario_RolServicio> listaFiltrada = lista
                                                      .GroupBy(p => p.RolServicio.FechaInicio)
                                                      .Select(g => g.First())
                                                      .ToList();



            return listaFiltrada;
        

        }

        public List<Usuario> GetUsuariosIngresadosByFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            return repository.GetUsuariosIngresadosByFechas(fechaInicio, fechaFin);
        }
    }
}
