using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Articulo
    {
        public int Id { get; set; }

        [DisplayName("Código")]
        public string Codigo { get; set; }
        public string Nombre { get; set; }

        [DisplayName("Descripción")] //Annotations
        public string Descripcion { get; set; }
        public Marca IdMarca { get; set; } = new Marca();
        public Categoria IdCategoria { get; set; } = new Categoria();
        public string ImagenUrl { get; set; }
        public decimal Precio { get; set; }

        public Articulo()
        {

        }


    }
}
