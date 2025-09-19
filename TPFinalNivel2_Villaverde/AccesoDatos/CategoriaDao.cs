using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace AccesoDatos
{
    public class CategoriaDao
    {
        public List<Categoria> listarCategoria()
        {
            List<Categoria> listaCategoria = new List<Categoria>();
            AccesoDB BDatos = new AccesoDB();
            SqlDataReader lector;

            string consultaCategoria = "select Id, Descripcion from Categorias";

            BDatos.setearConsulta(consultaCategoria);
            lector = BDatos.ejecutarLectura();

            while (lector.Read())
            {
                Categoria aux = new Categoria();
                aux.Id = (int)lector["Id"];
                aux.DescripcionCategoria = (string)lector["Descripcion"];

                listaCategoria.Add(aux);
            }
            return listaCategoria;
        }


        public List<Categoria> listarIds()
        {
            List<Categoria> lista = new List<Categoria>();
            AccesoDB datos = new AccesoDB();
            SqlDataReader lector;

            try
            {
                datos.setearConsulta("SELECT Id FROM Marcas");
                lector = datos.ejecutarLectura();

                while (lector.Read())
                {
                    Categoria aux = new Categoria();
                    aux.Id = (int)lector["Id"]; 
                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
