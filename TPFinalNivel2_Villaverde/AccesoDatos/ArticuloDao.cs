using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace AccesoDatos
{
    public class ArticuloDao
    {
        List<Articulo> lista = new List<Articulo>();
        AccesoDB accesoDB = new AccesoDB();
        SqlDataReader lector;
        public List<Articulo> listar()
        {


            string consultar = "select A.Id, A.Codigo, A.Nombre, A.Descripcion, M.Descripcion as Marca, C.Descripcion as Categoria, A.ImagenUrl, A.Precio from ARTICULOS A inner join CATEGORIAS C on A.IdCategoria = C.Id inner join MARCAS M on A.IdMarca = M.Id";

            accesoDB.setearConsulta(consultar);

            lector = accesoDB.ejecutarLectura();

            while (lector.Read())
            {
                Articulo aux = new Articulo();
                aux.Id = (int)lector["Id"];
                aux.Codigo = (string)lector["Codigo"];
                aux.Nombre = (string)lector["Nombre"];
                aux.Descripcion = (string)lector["Descripcion"];
                aux.IdMarca.DescripcionMarca = (string)lector["Marca"];
                aux.IdCategoria.DescripcionCategoria = (string)lector["Categoria"];
                aux.ImagenUrl = (string)lector["ImagenUrl"];
                aux.Precio = (decimal)lector["Precio"];


                lista.Add(aux);
            }

            return lista;             
        }

        public void insertarArticulo(Articulo articulo)
        {
             
            string consulta = "INSERT INTO ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, ImagenUrl, Precio) VALUES (@Codigo, @Nombre, @Descripcion, @IdMarca, @IdCategoria, @ImagenUrl, @Precio)";
            accesoDB.setearConsulta(consulta);

            accesoDB.setearParametros("@Codigo", articulo.Codigo);
            accesoDB.setearParametros("@Nombre", articulo.Nombre);
            accesoDB.setearParametros("Descripcion", articulo.Descripcion);
            accesoDB.setearParametros("IdMarca", articulo.IdMarca.Id);
            accesoDB.setearParametros("@IdCategoria", articulo.IdCategoria.Id);
            accesoDB.setearParametros("@ImagenUrl", articulo.ImagenUrl);
            accesoDB.setearParametros("@Precio", articulo.Precio);

            accesoDB.ejecutarAccion();

        }

        public void eliminarArticulo(Articulo articulo)
        {
            string consulta = "delete from ARTICULOS where Id = @Id";
            accesoDB.setearConsulta(consulta);
            accesoDB.setearParametros("@Id", articulo.Id);
            accesoDB.ejecutarAccion();
            accesoDB.cerrarConexion();
        }

        public void modificarArticulo(Articulo articulo)
        {
            string consulta = "update ARTICULOS set Nombre = @Nombre, Descripcion = @Descripcion, Precio = @Precio where Id = @Id";
            accesoDB.setearConsulta(consulta);
            accesoDB.setearParametros("@Id", articulo.Id);
            accesoDB.setearParametros("@Nombre", articulo.Nombre);
            accesoDB.setearParametros("@Descripcion", articulo.Descripcion);
            accesoDB.setearParametros("@Precio", articulo.Precio);
            accesoDB.ejecutarAccion();
            accesoDB.cerrarConexion();
        }
        
    }
}
