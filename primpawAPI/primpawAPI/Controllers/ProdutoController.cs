using Microsoft.AspNetCore.Mvc;
using primpawAPI;
using System.Data.SqlClient;

namespace primpawAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly ILogger<ProdutoController> _logger;

        private readonly string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=PrimPaw;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
        public ProdutoController(ILogger<ProdutoController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "Getproduto")]
        public IEnumerable<Produto> Get()
        {
            List<Produto> produto = new List<Produto>();

            using (SqlConnection conection = new SqlConnection(ConnectionString))
            {
                string query = "select * from produto";
                SqlCommand command = new SqlCommand(query, conection);
                conection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Produto produtos = new Produto
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Nome = reader["Nome"].ToString(),
                        Img = reader["Img"].ToString(),
                        Preco = Convert.ToDecimal(reader["Preco"])
                    };

                    produto.Add(produtos);
                }

                reader.Close();
            }

            return produto;
        }
    }
}