using Aula_03_Core.Interface;
using Aula_03_Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Aula_03.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class ProdutoController : ControllerBase
    {
        
        public IProdutoService _produtoService;

        //public List<Produto> ProdutoList { get; set; }

        public ProdutoController(IProdutoService produtoservice)
        {
            //ProdutoList = new List<Produto>();
            _produtoService = produtoservice;
        }

        [HttpGet("/produto")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<Produto>> GetProdutos()
        {
            var allProdutos = _produtoService.ConsultarProdutos();
            return Ok(allProdutos);
        }

        [HttpGet("/produto/{descricao}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Produto> GetProduto(string descricao)
        {
            var produtos = _produtoService.ConsultarProduto(descricao);
            if (produtos == null)
            {
                return NotFound();
            }
            return Ok(produtos);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Produto> InsertProduto(Produto produto)
        {

            if (!_produtoService.NovoProduto(produto))
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(InsertProduto), produto);

            ////Primeira forma de fazer:
            //ProdutoList.Add(produto);
            //return CreatedAtAction(nameof(InsertProduto), produto);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateProduto(long id, Produto produto)
        {
            if (!_produtoService.AtualizarProduto(id, produto))
            {
                return NotFound();
            }
            _produtoService.AtualizarProduto(id, produto);
            return Accepted();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<Produto>> DeleteProduto(long id)
        {
            if (!_produtoService.DeletarProduto(id))
            {
                return NotFound();
            }
            _produtoService.DeletarProduto(id);
            return NoContent();
        }
    }
}
