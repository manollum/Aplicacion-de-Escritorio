using Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class MarcaDao
    {
        public List<Marca> listarMarca()
        {
            List<Marca> listaMarca = new List<Marca>();
            AccesoDB BDatos = new AccesoDB();
            SqlDataReader lector;

            string consultaCategoria = "select Id, Descripcion from Marcas";

            BDatos.setearConsulta(consultaCategoria);
            lector = BDatos.ejecutarLectura();

            while (lector.Read())
            {
                Marca aux = new Marca();
                aux.Id = (int)lector["Id"];
                aux.DescripcionMarca = (string)lector["Descripcion"];

                listaMarca.Add(aux);
            }
            return listaMarca;
        }

        public List<Marca> listarIds()
        {
            List<Marca> lista = new List<Marca>();
            AccesoDB datos = new AccesoDB();
            SqlDataReader lector;

            try
            {
                datos.setearConsulta("SELECT Id FROM Marcas");
                lector = datos.ejecutarLectura();

                while (lector.Read())
                {
                    Marca aux = new Marca();
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
