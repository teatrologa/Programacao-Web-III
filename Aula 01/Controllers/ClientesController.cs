using Microsoft.AspNetCore.Mvc;

namespace Aula_01.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]

    public class ClientesController : Controller
    {
        private Random rdn = new Random();

        private List<string> listName = new()
        {
            "Jon Snow",
            "Arya Stark",
            "Rhys",
            "Aelin Galathynius",
            "Sherlock Holmes",
            "Harry Potter",
            "Alvo Dumbledore"
        };

        private List<string> listCpf = new()
        {
            "111.111.111-11",
            "222.222.222-22",
            "333.333.333-33",
            "444.444.444-44",
            "555.555.555-55",
            "666.666.666-66",
            "777.777.777-77"

        };

        private DateTime RandomDay()
        {
            DateTime start = new DateTime(1945, 1, 1);
            int range = (DateTime.Today - start).Days;
            var dateRdn = start.AddDays(rdn.Next(range));
            return dateRdn;
        }
        public List<Cliente> clientes { get; set; }

        private readonly ILogger<ClientesController> _logger;

        public ClientesController(ILogger<ClientesController> logger)
        {
            _logger = logger;
            clientes = Enumerable.Range(1, listName.Count).Select(index => new Cliente
            {
                //Cpf = listCpf[Random.Shared.Next(listCpf.Count)],
                Cpf = Convert.ToString("000000000-" + index),
                Name = listName[index -1],
                Nascimento = RandomDay() //Toda e qualquer chamada ela traz nascimentos variados. GetAll != GetCPF != GetIndex
            })
            .ToList();
        }



        [HttpGet("Registros")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<Cliente>> AllCliente()
        {
            if (clientes == null)
            {
                return NoContent();
            }
            return Ok(clientes);
        }


        [HttpGet("{index}/Info")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<Cliente>> Info(int index)
        {
            index = index - 1;
            if (index > clientes.Count - 1 || index < 0)
            {
                return NotFound();
            }
            return Ok(clientes[index]);
        }


        [HttpGet("{cpf}/View")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<Cliente>> Info (string cpf)
        {
            var clienteEscolhido = clientes.Where(n => n.Cpf == cpf).ToList();
            if (clienteEscolhido.Any())
            {
                return Ok(clienteEscolhido);
            }
            return NotFound();
        }


        [HttpPost("Novo")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Cliente> NovoCadastro([FromBody]Cliente cliente)
        {
            clientes.Add(cliente);
            var teste = clientes.Contains(cliente);
            if (teste == true)
            {
                //return Created(nameof(Atualizacao), cliente); //Teste que precisa ser entendido melhor.
                return StatusCode(201, cliente);
            }
            return BadRequest("Não foi possível criar um registro para esse cliente.");
        }


        [HttpPut("{index}/Atualizar")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public IActionResult Atualizacao(int index, Cliente cliente)
        //Testando o uso de uma interface de resposta, ela aceita qualquer tipo de retorno, sem especificação
        {
            index = index - 1;
            if (index > clientes.Count || index < 0)
            {
                return NotFound();
            }
            clientes[index] = cliente;
            //return Ok(clientes[index]); //Caso use o Ok como resposta.
            return Accepted(clientes[index]);
        }


        [HttpDelete("{index}/Deletar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<Cliente>> Deletar(int index)
        {
            index = index - 1;
            if (index > clientes.Count || index < 0)
            {
                return NotFound();
            }
            clientes.RemoveAt(index);
            return Ok(clientes);
            
        }


    }

}
