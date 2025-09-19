using AccesoDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Negocio
{
    public class CategoriaNegocio
    {
        private CategoriaDao CategoriaDao = new CategoriaDao();

        public List<Categoria> listarCategoria() 
        { 
            return CategoriaDao.listarCategoria();
        }

        public List<Categoria> Ids()
        {
            return CategoriaDao.listarIds();
        }
    }
}
