using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos;
using Entidades;

namespace Negocio
{
    public class ArticuloNegocio
    {
        private ArticuloDao daoArticulo = new ArticuloDao();

        public List<Articulo> listar()
        {
            return daoArticulo.listar();
        }

        public void insertar(Articulo articulo)
        {
            daoArticulo.insertarArticulo(articulo);
        }

        public void eliminar(Articulo articulo)
        {
            daoArticulo.eliminarArticulo(articulo);
        }

        public void modificarArticulo(Articulo articulo)
        {
            daoArticulo.modificarArticulo(articulo);
        }
    }
}
