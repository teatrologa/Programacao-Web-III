using Aula_03___Exercicio.Models;
using Aula_03___Exercicio.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Aula_03___Exercicio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : ControllerBase
    {
        public ClienteRepository _clienteRepository;
        public List<Cliente> ClienteList { get; set; }
        public ClienteController(IConfiguration configuration)
        {
            _clienteRepository = new ClienteRepository(configuration);
            ClienteList = new List<Cliente>();
        }

        [HttpGet("/clientes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<Cliente>> GetAllClients()
        {
            //testando esse if
            if (_clienteRepository.GetCliente().Any() != null)
            {
                var allClients = _clienteRepository.GetCliente();
                return Ok(allClients);
            }
            return NoContent();
        }

        [HttpGet("/clientes/{cpf}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Cliente> GetCliente(string cpf)
        {
            var cliente = _clienteRepository.GetClienteCpf(cpf);
            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(cliente);

        }

        [HttpPost("/clientes/cadastro")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Cliente> InsertCliente(Cliente cliente)
        {
            var cpfCadastrado = _clienteRepository.GetClienteCpf(cliente.Cpf);
            if (cpfCadastrado != null)
            {
                return Conflict("Já existe um cliente com esse CPF.");
            }

            if (!_clienteRepository.InsertCliente(cliente))
            {
                return BadRequest("Não foi possível criar um registro para esse cliente.");
            }
            return CreatedAtAction(nameof(InsertCliente), cliente);
        }

        [HttpPut("clientes/atualizacao")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Atualizacao(string cpf, Cliente cliente)
        {
            if (!_clienteRepository.UpdateCliente(cpf, cliente))
            {
                return NotFound("Este cpf não pertence a nenhum cliente cadastrado.");
            }
            _clienteRepository.UpdateCliente(cpf, cliente);
            return Accepted();
        }

        [HttpDelete("/clientes/remover")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<Cliente>> RemoveCliente(string cpf)
        {
            if (!_clienteRepository.DeleteCliente(cpf))
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}