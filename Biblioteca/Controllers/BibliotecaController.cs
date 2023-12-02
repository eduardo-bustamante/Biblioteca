using Biblioteca.Data;
using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Controllers
{
    public class BibliotecaController : Controller
    {
        readonly private ApplicationDbContext _db;

        public BibliotecaController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<BibliotecaModel>bibliotecas = _db.Bibliotecas;
            return View(bibliotecas);
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(BibliotecaModel bibliotecas)
        {
            if (ModelState.IsValid)
            {
                _db.Bibliotecas.Add(bibliotecas);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }
    }
}
