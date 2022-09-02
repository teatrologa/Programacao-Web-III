using Aula_03.Models;
using Microsoft.Data.SqlClient;
using Dapper;

namespace Aula_03.Repository
{
    public class ProdutoRepository
    {
        private readonly IConfiguration _configuration;

        public ProdutoRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<Produto> GetProdutos()
        {
            var query = "SELECT * FROM Produtos";

            //var connectionString = _configuration.GetConnectionString("DefaultConnection");

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Query<Produto>(query).ToList();

            // var nomeVar = conn.Query<Produto>(query).ToList();
            //retunr nomeVar;
        }

        public bool InsertProduto(Produto produto)
        {
            var query = "INSERT INTO Produtos VALUES (@descricao, @preco, @quantidade)";

            var parameters = new DynamicParameters();
            parameters.Add("descricao", produto.Descricao);
            parameters.Add("preco", produto.Preco);
            parameters.Add("quantidade", produto.Quantidade);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;
        }

        public bool DeleteProduto(long id)
        {
            var query = "DELETE FROM Produtos WHERE id = @id";

            var parameters = new DynamicParameters();
            parameters.Add("id", id);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;
        }
    }
}
