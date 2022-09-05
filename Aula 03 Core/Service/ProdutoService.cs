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

        public bool NovoProduto(Produto novoProduto)
        {
            return _produtoRepository.InsertProduto(novoProduto);
        }

        public bool AtualizarProduto (long id, Produto produto)
        {
            return _produtoRepository.UpdateProduto(id, produto);
        }

        public bool DeletarProduto (long id)
        {
            return _produtoRepository.DeleteProduto(id);
        }
    }
}