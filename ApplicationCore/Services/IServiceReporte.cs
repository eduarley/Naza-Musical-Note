using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceReporte
    {

        List<Usuario> GetUsuariosIngresadosByFechas(DateTime fechaInicio, DateTime fechaFin);
        List<Usuario_RolServicio> GetServiciosByUsuario(string id);
        int GetTotalRegistrosSesiones();
    }
}
