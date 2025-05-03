using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaEntidades
{
    public class GeneroLiterario
    {
        public int IdGenero {  get; set; }
        public string Nombre { get; set; } = null!;
        public int IdGeneroPadre { get; set; }
        public Categoria iCategoria { get; set; } = null!;
        public int IdCategoria { get; set; }
    }
}
