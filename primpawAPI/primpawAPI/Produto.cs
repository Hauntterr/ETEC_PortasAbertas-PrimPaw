using System.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;

namespace primpawAPI
{
    public class Produto
    {
        public int Id { get; set; }
        public string CImg { get; set; }
        public byte[] Img { get; set; }
        public string Nome { get; set; }
        public decimal preco { get; set; }
    }


    
}