using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Controllers
{
    public class BibliotecaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
