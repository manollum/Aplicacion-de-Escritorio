using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    internal class AccesoDB
    {
        private string rutaDeConexion = "server=(localdb)\\MSSQLLocalDB; initial catalog=CATALOGO_DB; integrated security=true;";
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;

        public void setearParametros(string nombre, object valor)
        {
            if (valor == null)
                comando.Parameters.AddWithValue(nombre, DBNull.Value);
            else
                comando.Parameters.AddWithValue(nombre, valor);
        }

        public void setearConsulta(string consulta)
        {
            conexion = new SqlConnection(rutaDeConexion);
            comando = new SqlCommand(consulta, conexion);
            comando.CommandType = CommandType.Text;
            comando.Parameters.Clear();
        }

        public SqlDataReader ejecutarLectura() //Solo SELECT.
        {
            if (conexion.State != ConnectionState.Open)
            {
                conexion.Open();
            }
            return lector = comando.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public SqlDataReader Lector
        {
            get { return lector; }
        }

        public void ejecutarAccion() //INSERT, DELETE, UPDATE
        {
            if (conexion.State != ConnectionState.Open)
            {
                conexion.Open();
            }
            comando.ExecuteNonQuery();
            conexion.Close();
        }

        public void cerrarConexion()
        {
            if (lector != null) lector.Close();
            if (conexion != null) conexion.Close();
        }
    }
}
