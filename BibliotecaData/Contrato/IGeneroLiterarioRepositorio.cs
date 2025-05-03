using BibliotecaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaData.Contrato
{
    public interface IGeneroLiterarioRepositorio
    {
        Task<List<GeneroLiterario>> Lista();
        Task<GeneroLiterario> Obtener(int IdGenero);
        Task<string> Guardar(GeneroLiterario objeto);
        Task<string> Editar(GeneroLiterario objeto);
        Task<int> Eliminar(int IdGenero);
    }
}
