using Microsoft.AspNetCore.Mvc;

namespace Aula_01.Controllers
{
    public class FirstController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
