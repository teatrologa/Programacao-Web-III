using Aula_03_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aula_03_Core.Interface
{
    public interface IProdutoRepository
    {
        List<Produto> ConsultarProdutos();

        public Produto GetProduto(string descricao);

        public Produto GetIdProduto(long id);

        public bool UpdateProduto(Produto produto);

        public bool InsertProduto(Produto produto);

        public bool DeleteProduto(long id);
    }
}
