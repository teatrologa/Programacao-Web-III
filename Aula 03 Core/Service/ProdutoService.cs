using Aula_03_Core.Models;
using Aula_03_Core.Interface;

namespace Aula_03_Core.Service
{
    public class ProdutoService : IProdutoService
    {
        public IProdutoRepository _produtoRepository;
        
        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public List<Produto> ConsultarProdutos()
        {
            return _produtoRepository.ConsultarProdutos();
        }

        public Produto ConsultarProduto(string descricao)
        {
            return _produtoRepository.GetProduto(descricao);
        }

        public Produto ConsultarIdProduto(long id)
        {
            return _produtoRepository.GetIdProduto(id);
        }

        public bool NovoProduto(Produto novoProduto)
        {
            return _produtoRepository.InsertProduto(novoProduto);
        }

        public bool AtualizarProduto (long id, Produto produto)
        {
            /* O TRY CATCH NÃO É USADO QUANDO TEMOS FILTRO DE EXCEPTIONS
            try
            {
                produto = null;
                produto.Id = id;
            }
            catch (Exception ex) //EXEMPLO 1: é possível usar mais de 1 catch
            // usar varios catch é como um switch ou passar algo por diversos numeros de uma peneira.
            {
                var mensagem = ex.Message;
                var caminho = ex.InnerException.StackTrace;
                var tipoExcecao = ex.GetType().Name;
                
                Console.WriteLine($"Tipo da exceção: {tipoExcecao}. " +
                    $"Mensagem: {mensagem}. " +
                    $"Stack trace: {caminho}.");
                
                return false; //manda o programa voltar informando que deu erro
            }
            */
            //produto = null; //usado para gerar um erro intencional a fim de testar nosso filtro de erro
            produto.Id = id;
            return _produtoRepository.UpdateProduto(produto);
        }

        public bool DeletarProduto (long id)
        {
            return _produtoRepository.DeleteProduto(id);
        }
    }
}