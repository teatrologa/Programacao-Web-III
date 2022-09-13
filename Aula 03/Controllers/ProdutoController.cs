using Aula_03.Filters;
using Aula_03_Core.Interface;
using Aula_03_Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Aula_03.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [TypeFilter(typeof(LogResourceFilter))] //Outro lugar onde o filtro pode ser implementado
    //Dessa forma qualquer ação dessa controller passará por esse filtro.
    //Ex: todo método chamado passa por esse filtro aqui (vide o console)
    
    public class ProdutoController : ControllerBase
    {
        
        public IProdutoService _produtoService;

        public ProdutoController(IProdutoService produtoservice)
        {
            Console.WriteLine("Iniciando a instancia de ProdutoService no ProdutoController.");
            _produtoService = produtoservice;
        }

        [HttpGet("/produto")] //caminho da "url"
        [ProducesResponseType(StatusCodes.Status200OK)] //Status de ação que aparece na documentação
        
        //Forma de inserir filtro. Dessa forma esse filtro só é acionado quando o método é chamado.
        [TypeFilter(typeof(LogAuthorizationFilter))] //Podemos ter varios filtros
        //E filtros personalizados para cada métodos.
        public ActionResult<List<Produto>> GetProdutos()
        {
            Console.WriteLine("Iniciando GetProdutos");
            var allProdutos = _produtoService.ConsultarProdutos();
            return Ok(allProdutos);
        }

        [HttpGet("/produto/{descricao}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Produto> GetProduto(string descricao)
        {
            Console.WriteLine("Iniciando GetProdutos");
            var produtos = _produtoService.ConsultarProduto(descricao);
            if (produtos == null)
            {
                return NotFound();
            }
            return Ok(produtos);
        }

        [HttpPost("/produto/novo")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        
        //PASSO 2: add filtro que faz aquela validação
        [TypeFilter(typeof(LogActionFilter))] //filtro personalizado.
        public ActionResult<Produto> InsertProduto(Produto produto)
        {
            Console.WriteLine("Iniciando InsertProduto");

            //PASSO 1, estava criado assim. Porem vamos usar um filtro que vai fazer essa função
            //if (!ModelState.IsValid) //não entendi muito bem o que isso faz
            //{
            //    return BadRequest();
            //}
            
            if (!_produtoService.NovoProduto(produto))
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(InsertProduto), produto);

            ////Primeira forma de fazer:
            //ProdutoList.Add(produto);
            //return CreatedAtAction(nameof(InsertProduto), produto);
        }

        [HttpPut("/produto/atualizacao")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //Atualização: add filtro que faz aquela validação
        [TypeFilter(typeof(LogActionFilter))] //filtro personalizado.
        //mesmo caso do metodo acima
        [ServiceFilter(typeof(GaranteProdutoExisteActionFilter))]
        public IActionResult UpdateProduto(long id, Produto produto)
        {
            Console.WriteLine("Iniciando UpdateProduto");
            
            if (!_produtoService.AtualizarProduto(id, produto))
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            _produtoService.AtualizarProduto(id, produto);
            return Accepted();
        }

        [HttpDelete("/produto/deletar")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ServiceFilter(typeof(GaranteProdutoExisteActionFilter))]
        public ActionResult<List<Produto>> DeleteProduto(long id)
        {
            Console.WriteLine("Iniciando DeleteProduto");
            if (!_produtoService.DeletarProduto(id))
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            _produtoService.DeletarProduto(id);
            return NoContent();
        }
    }
}
