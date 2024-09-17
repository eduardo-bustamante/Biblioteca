using Biblioteca.Data;
using Biblioteca.Models;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
using System.IO;



namespace Biblioteca.Controllers
{
    public class BibliotecaController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _environment;

        public BibliotecaController(ApplicationDbContext db, IWebHostEnvironment environment)
        {
            _db = db;
            _environment = environment;

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
            if (biblioteca.Capa == null || biblioteca.Capa == "")
            {
                biblioteca.Capa = "/capas/default.jpeg";
            }

            return View(biblioteca);
        }


        [HttpPost]
        public async Task<IActionResult> Cadastrar(BibliotecaModel biblioteca, IFormFile capa)
        {
            biblioteca.DataCadastro = DateTime.Now; //Para cadastrar a data atual no banco de dado
            if (ModelState.IsValid)
            {
                if (capa != null && capa.Length > 0)
                {
                    var uploads = Path.Combine("wwwroot/capas");
                    var nomeArquivo = Guid.NewGuid().ToString() + biblioteca.Titulo + Path.GetExtension(capa.FileName);
                    Directory.CreateDirectory(uploads);
                    var filePath = Path.Combine(uploads, nomeArquivo);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await capa.CopyToAsync(stream);
                    }

                    biblioteca.Capa = "/capas/" + nomeArquivo;
                }


                _db.Bibliotecas.Add(biblioteca);
                await _db.SaveChangesAsync();

                TempData["MensagemSucesso"] = "Cadastro Realizado com Sucesso!";

                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(BibliotecaModel biblioteca, IFormFile capa)
        {

            ModelState.Clear();
            if (ModelState.IsValid)
            {
                if (capa != null && capa.Length > 0)
                {
                    var uploads = Path.Combine(_environment.WebRootPath, "capas");
                    var nomeArquivo = Guid.NewGuid().ToString() + biblioteca.Titulo + Path.GetExtension(capa.FileName);
                    var filePath = Path.Combine(uploads, nomeArquivo);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await capa.CopyToAsync(stream);
                    }

                    biblioteca.Capa = "/capas/" + nomeArquivo;

                }
                _db.Bibliotecas.Update(biblioteca);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            TempData["MensagemErro"] = "Algum erro ocorreu ao realizar a edição!";

            return View(biblioteca);
        }

        [HttpPost]
        public async Task<IActionResult> Excluir(BibliotecaModel biblioteca)
        {
            if (biblioteca == null)
            {
                return NotFound();
            }

            var registro = await _db.Bibliotecas.FindAsync(biblioteca.Id);

            _db.Bibliotecas.Remove(registro);
            await _db.SaveChangesAsync();

            TempData["MensagemSucesso"] = "Remoção Realizada com Sucesso!";


            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Detalhar(BibliotecaModel biblioteca, IFormFile capa, int id)
        {
            if (id != biblioteca.Id) return NotFound();

            ModelState.Clear();
            if (ModelState.IsValid)
            {
                if (capa != null && capa.Length > 0)
                {
                    var uploads = Path.Combine(_environment.WebRootPath, "capas");
                    var nomeArquivo = biblioteca.Id + Guid.NewGuid().ToString() + biblioteca.Titulo + Path.GetExtension(capa.FileName);
                    var filePath = Path.Combine(uploads, nomeArquivo);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await capa.CopyToAsync(stream);
                    }

                    biblioteca.Capa = "/capas/" + nomeArquivo;

                }

                var nomeCapa = biblioteca.Capa;

                _db.Update(biblioteca);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            TempData["MensagemErro"] = "Algum erro ocorreu ao realizar a edição!";

            return View(biblioteca);
        }
    }
}
