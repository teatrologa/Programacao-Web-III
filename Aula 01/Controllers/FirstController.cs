using Microsoft.AspNetCore.Mvc;

namespace Aula_01.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class FirstController : Controller
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

        private readonly ILogger<FirstController> _logger;

        public FirstController(ILogger<FirstController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetPersonalInfo")]

        public IEnumerable<First> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new First
            {
                Cpf = listCpf[Random.Shared.Next(listCpf.Count)],
                Name = listName[Random.Shared.Next(listCpf.Count)],
                Nascimento = RandomDay()
            })
            .ToList();
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
