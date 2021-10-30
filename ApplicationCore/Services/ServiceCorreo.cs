using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceCorreo : IServiceCorreo
    {
        IRepositoryCorreo repository = new RepositoryCorreo();
        public CorreoEmisor Edit(CorreoEmisor correo)
        {
            return repository.Edit(correo);
        }

        public CorreoEmisor GetCorreo()
        {
            return repository.GetCorreo();
        }
    }
}
