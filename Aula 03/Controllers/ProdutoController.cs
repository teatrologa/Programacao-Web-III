using Aula_03.Models;
using Aula_03.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Aula_03.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class ProdutoController : ControllerBase
    {
        
        public ProdutoRepository _repositoryProduto;
        public List<Produto> ProdutoList { get; set; }
        public ProdutoController(IConfiguration configuration)
        {
            ProdutoList = new List<Produto>();
            _repositoryProduto = new ProdutoRepository(configuration);
        }

        [HttpGet("/produto/{descricao}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Produto> GetProduto(string descricao)
        {
            var produtos = ProdutoList;
            if (produtos == null)
            {
                return NotFound();
            }
            return Ok(produtos);
        }

        [HttpGet("/produto")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<Produto>> GetProdutos()
        {
            var allProdutos = _repositoryProduto.GetProdutos();
            return Ok(allProdutos);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Produto> InsertProduto(Produto produto)
        {

            if (!_repositoryProduto.InsertProduto(produto))
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
            var produtos = ProdutoList;
            if (produtos == null)
                return NotFound();

            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<Produto>> DeleteProduto(long id)
        {
            if (!_repositoryProduto.DeleteProduto(id))
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
