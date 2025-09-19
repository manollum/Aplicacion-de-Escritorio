using AccesoDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Negocio
{
    public class MarcaNegocio
    {
        MarcaDao marcaDao = new MarcaDao();

        public List<Marca> listarMarcas()
        {
            return marcaDao.listarMarca();
        }

        public List<Marca> Ids()
        {
            return marcaDao.listarIds();    
        }
    }
}
