﻿using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceCategoria
    {
        List<Categoria> GetCategorias();
        List<Categoria> GetCategoriasActivas();
        Categoria GetCategoriaByID(int id);
        bool DeteteCategoria(int id);
        Categoria Save(Categoria categoria);
    }
}
