using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace CrudApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ComentariosController : ControllerBase
    {
        private readonly ILogger<ComentariosController> _logger;

        private const string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        public ComentariosController(ILogger<ComentariosController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetComentarios")]
        public IEnumerable<Comentarios> Get()
        {
            List<Comentarios> comentarios = new List<Comentarios>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "SELECT * FROM Comentarios";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Comentarios comentario = new Comentarios
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Nome = reader["Nome"].ToString(),
                        Comentario = reader["Comentario"].ToString(),
                        DataHora = (DateTime)reader["DataHora"]
                    };

                    comentarios.Add(comentario);
                }

                reader.Close();
            }

            return comentarios;
        }

        [HttpGet("{id}", Name = "GetComentarioById")]
        public ActionResult GetComentarioById(int id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "SELECT * FROM Comentarios WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Comentarios comentario = new Comentarios
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Nome = reader["Nome"].ToString(),
                        Comentario = reader["Comentario"].ToString()
                    };

                    reader.Close();

                    return Ok(comentario);
                }

                reader.Close();
            }

            return NotFound();
        }

        [HttpPost]
        public ActionResult CreateComentario(Comentarios comentario)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "INSERT INTO Comentarios (Nome, Comentario, DataHora) VALUES (@Nome, @Comentario, @DataHora)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Nome", comentario.Nome);
                command.Parameters.AddWithValue("@Comentario", comentario.Comentario);
                command.Parameters.AddWithValue("@DataHora", DateTime.Now);

                connection.Open();

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    return Ok();
                }
            }

            return BadRequest();
        }
    }
}
