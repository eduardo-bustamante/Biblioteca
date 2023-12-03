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
            IEnumerable<BibliotecaModel> bibliotecas = _db.Bibliotecas;
            return View(bibliotecas);
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        public IActionResult Editar(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            BibliotecaModel biblioteca = _db.Bibliotecas.FirstOrDefault(x => x.Id == id);

            if (biblioteca == null)
            {
                return NotFound();
            }

            return View(biblioteca);
        }

        public IActionResult Excluir(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            BibliotecaModel biblioteca = _db.Bibliotecas.FirstOrDefault(x => x.Id == id);
            if (biblioteca == null)
            {
                return NotFound();
            }

            return View(biblioteca);
        }

        public IActionResult Detalhar(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            BibliotecaModel biblioteca = _db.Bibliotecas.FirstOrDefault(x => x.Id == id);


            return View(biblioteca);
        }


        [HttpPost]
        public IActionResult Cadastrar(BibliotecaModel bibliotecas)
        {
            bibliotecas.DataCadastro = DateTime.Now; //Para cadastrar a data atual no banco de dado
            if (ModelState.IsValid)
            {
                _db.Bibliotecas.Add(bibliotecas);
                _db.SaveChanges();

                TempData["MensagemSucesso"] = "Cadastro Realizado com Sucesso!";


                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Editar(BibliotecaModel biblioteca)
        {
            if (ModelState.IsValid)
            {
                _db.Bibliotecas.Update(biblioteca);
                _db.SaveChanges();

                TempData["MensagemSucesso"] = "Edição Realizada com Sucesso!";

                return RedirectToAction("Index");
            }

            TempData["MensagemErro"] = "Algum erro ocorreu ao realizar a edição!";

            return View(biblioteca);
        }

        [HttpPost]
        public IActionResult Excluir(BibliotecaModel biblioteca)
        {
            if (biblioteca == null)
            {
                return NotFound();
            }

            _db.Bibliotecas.Remove(biblioteca);
            _db.SaveChanges();

            TempData["MensagemSucesso"] = "Remoção Realizada com Sucesso!";


            return RedirectToAction("Index");
        }

    }
}
