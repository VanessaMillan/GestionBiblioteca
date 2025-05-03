using BibliotecaData.Contrato;
using BibliotecaEntidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaWeb.Controllers
{
    [Authorize]

    public class GeneroLiterarioController : Controller
    {
        private readonly IGeneroLiterarioRepositorio _repositorio;
        public GeneroLiterarioController(IGeneroLiterarioRepositorio repositorio)
        {
            _repositorio = repositorio;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<GeneroLiterario> lista = await _repositorio.Lista();
            return StatusCode(StatusCodes.Status200OK, new { data = lista });
        }

        [HttpGet]
        public async Task<IActionResult> Obtener(int IdGenero)
        {
            GeneroLiterario objeto = await _repositorio.Obtener(IdGenero);
            if (objeto != null)
                return StatusCode(StatusCodes.Status200OK, new { data = objeto });
            else
                return StatusCode(StatusCodes.Status404NotFound, new { data = objeto });
        }

        [HttpPost]
        public async Task<IActionResult> Guardar([FromBody] GeneroLiterario objeto)
        {
            string respuesta = await _repositorio.Guardar(objeto);
            return StatusCode(StatusCodes.Status200OK, new { data = respuesta });
        }


        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] GeneroLiterario objeto)
        {
            string respuesta = await _repositorio.Editar(objeto);
            return StatusCode(StatusCodes.Status200OK, new { data = respuesta });
        }

        [HttpDelete]
        public async Task<IActionResult> Eliminar(int IdGenero)
        {
            int respuesta = await _repositorio.Eliminar(IdGenero);
            return StatusCode(StatusCodes.Status200OK, new { data = respuesta });
        }
    }
}

