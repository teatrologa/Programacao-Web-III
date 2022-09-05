using Aula_03_Core.Interface;
using Aula_03_Core.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Aula_03_Infra_Data.Ropository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly IConfiguration _configuration;

        public ProdutoRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<Produto> ConsultarProdutos()
        {
            var query = "SELECT * FROM Produtos";

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Query<Produto>(query).ToList();
        }
        
        public Produto GetProduto(string descricao)
        {
            var query = "SELECT * FROM Produtos where descricao = @descricao";

            var parameters = new DynamicParameters(new
            {
                descricao
            });

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.QueryFirstOrDefault<Produto>(query, parameters);
        }
        
        public bool InsertProduto(Produto produto)
        {
            var query = "INSERT INTO Produtos VALUES (@descricao, @preco, @quantidade)";

            var parameters = new DynamicParameters(new
            {
                produto.Descricao,
                produto.Quantidade,
                produto.Preco
            });

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
        
        public bool UpdateProduto(long id, Produto produto)
        {
            var query = @"UPDATE Produtos set descricao = @descricao,
                          preco = @preco, quantidade = @quantidade
                          where id = @id";

            produto.Id = id;
            var parameters = new DynamicParameters(produto);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;
        }
        
    }
}