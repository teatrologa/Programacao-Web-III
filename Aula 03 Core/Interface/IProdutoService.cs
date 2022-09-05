using Aula_03_Core.Models;

namespace Aula_03_Core.Interface
{
    public interface IProdutoService
    {
        public List<Produto> ConsultarProdutos();

        public Produto ConsultarProduto(string descricao);

        public bool NovoProduto(Produto novoProduto);

        public bool AtualizarProduto(long id, Produto produto);

        public bool DeletarProduto(long id);
    }
}
