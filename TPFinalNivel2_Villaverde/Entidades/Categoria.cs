using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Categoria
    {
        public int Id { get; set; }

        [DisplayName("Categoría")]
        public string DescripcionCategoria { get; set; }

        public override string ToString()
        {
            return DescripcionCategoria;
        }
    }
}
