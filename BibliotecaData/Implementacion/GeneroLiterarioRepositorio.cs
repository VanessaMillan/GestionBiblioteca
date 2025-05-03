using BibliotecaData.Configuracion;
using BibliotecaData.Contrato;
using BibliotecaEntidades;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;

namespace BibliotecaData.Implementacion
{
    public class GeneroLiterarioRepositorio : IGeneroLiterarioRepositorio
    {
        private readonly ConnectionStrings con;
        public GeneroLiterarioRepositorio(IOptions<ConnectionStrings> options)
        {
            con = options.Value;
        }

        public async Task<string> Guardar(GeneroLiterario objeto)
        {
            string respuesta = "";
            using (var conexion = new SqlConnection(con.CadenaSQL))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_guardarGeneroLiterario", conexion);
                cmd.Parameters.AddWithValue("@Nombre", objeto.Nombre);
                cmd.Parameters.AddWithValue("@IdGeneroPadre", objeto.IdGeneroPadre);
                cmd.Parameters.AddWithValue("@IdCategoria", objeto.IdCategoria);
                cmd.Parameters.Add("@msgError", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    await cmd.ExecuteNonQueryAsync();
                    respuesta = Convert.ToString(cmd.Parameters["@msgError"].Value)!;
                }
                catch
                {
                    respuesta = "Error al guardar genero";
                }
            }
            return respuesta;
        }

        public async Task<string> Editar(GeneroLiterario objeto)
        {
            string respuesta = "";
            using (var conexion = new SqlConnection(con.CadenaSQL))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_editarGeneroLiterario", conexion);
                cmd.Parameters.AddWithValue("@IdGenero", objeto.IdGenero);
                cmd.Parameters.AddWithValue("@Nombre", objeto.Nombre);
                cmd.Parameters.Add("@msgError", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    await cmd.ExecuteNonQueryAsync();
                    respuesta = Convert.ToString(cmd.Parameters["@msgError"].Value)!;
                }
                catch
                {
                    respuesta = "Error al editar genero";
                }

            }
            return respuesta;
        }

        public async Task<int> Eliminar(int IdGenero)
        {
            int respuesta = 1;
            using (var conexion = new SqlConnection(con.CadenaSQL))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_eliminarGeneroLiterario", conexion);
                cmd.Parameters.AddWithValue("@IdGenero", IdGenero);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    await cmd.ExecuteNonQueryAsync();
                }
                catch
                {
                    respuesta = 0;
                }

            }
            return respuesta;
        }

        public async Task<List<GeneroLiterario>> Lista()
        {
            List<GeneroLiterario> lista = new List<GeneroLiterario>();

            using (var conexion = new SqlConnection(con.CadenaSQL))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_listaGeneroLiterario", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        lista.Add(new GeneroLiterario()
                        {
                            IdGenero = Convert.ToInt32(dr["IdGenero"]),
                            Nombre = dr["Nombre"].ToString()!,
                            IdGeneroPadre = Convert.ToInt32(dr["IdGeneroPadre"])!,
                            IdCategoria = Convert.ToInt32(dr["IdCategoria"])!
                        });
                    }
                }
            }
            return lista;
        }

        public async Task<GeneroLiterario> Obtener(int IdGenero)
        {
            GeneroLiterario objeto = null!;

            using (var conexion = new SqlConnection(con.CadenaSQL))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_obtenerGeneroLiterario", conexion);
                cmd.Parameters.AddWithValue("@IdGenero", IdGenero);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        objeto = new GeneroLiterario()
                        {
                            IdCategoria = Convert.ToInt32(dr["IdCategoria"]),
                            Nombre = dr["Nombre"].ToString()!,
                            IdGeneroPadre = Convert.ToInt32(dr["IdGeneroPadre"])!,
                        };
                    }
                }
            }
            return objeto;
        }
    }
}
