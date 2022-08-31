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
                Cpf = listCpf[Random.Shared.Next(listCpf.Count)],
                Name = listName[Random.Shared.Next(listCpf.Count)],
                Nascimento = RandomDay()
            })
            .ToList();
        }



        [HttpGet("/clientes/index/Info")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<Cliente>> Info(int index)
        {
            if (index > clientes.Count - 1)
            {
                return NotFound();
            }
            return Ok(clientes[index]);
        }

        [HttpPost("/clientes/index/NovoCliente")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<Cliente> NovoCadastro([FromBody]Cliente cliente)
        {
            clientes.Add(cliente);
            return StatusCode(201, cliente);
        }

        [HttpPut("/clientes/index/Atualização")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public ActionResult Atualizacao(int index, Cliente cliente)
        {
            if (index > clientes.Count - 1)
            {
                return NotFound();
            }
            clientes[index] = cliente;
            return Ok(clientes[index]);
        }


        [HttpDelete("/clientes/index/DeletarCadastro")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Deletar(int index)
        {
            if (index > clientes.Count - 1 || index < 0)
            {
                return NotFound();
            }
            return Ok(clientes[index]);
            clientes.RemoveAt(index);
        }


    }

}
