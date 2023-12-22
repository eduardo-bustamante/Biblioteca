using Biblioteca.Data;
using Biblioteca.Models;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Controllers
{
    public class BibliotecaController : Controller
    {
        readonly private ApplicationDbContext _db;
        private string _filePath;



        public BibliotecaController(ApplicationDbContext db, IWebHostEnvironment sistema)
        {
            _db = db;
            _filePath = sistema.WebRootPath;
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastrar(BibliotecaModel biblioteca, IFormFile capa)
        {
            biblioteca.DataCadastro = DateTime.Now; //Para cadastrar a data atual no banco de dado
            if (ModelState.IsValid)
            {
                if (!ValidaCapa(capa))
                    return View();

                var nome = SalvarCapa(capa);

                biblioteca.Capa = nome;

                _db.Bibliotecas.Add(biblioteca);
                await _db.SaveChangesAsync();

                TempData["MensagemSucesso"] = "Cadastro Realizado com Sucesso!";

                return RedirectToAction("Index");
            }

            return View();
        }

        public bool ValidaCapa(IFormFile capa)
        {
            switch (capa.ContentType)
            {
                case "image/jpeg":
                    return true;
                case "image/bmp":
                    return true;
                case "image/gif":
                    return true;
                case "image/png":
                    return true;
                default:
                    return false;
            }
        }

        public string SalvarCapa(IFormFile capa)
        {
            var nome = Guid.NewGuid().ToString() + capa.FileName;

            var filePath = _filePath + "\\capas";
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            using (var stream = System.IO.File.Create(filePath + "\\" + nome))
            {
                capa.CopyToAsync(stream);
            }

            return nome;
        }

        [HttpPost]
        public async Task<IActionResult> Editar(BibliotecaModel biblioteca)
        {
            if (ModelState.IsValid)
            {
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
            string filePathName = _filePath + "\\capas\\" + registro.Capa;

            if (System.IO.File.Exists(filePathName))
                System.IO.File.Delete(filePathName);


            _db.Bibliotecas.Remove(registro);
            await _db.SaveChangesAsync();

            TempData["MensagemSucesso"] = "Remoção Realizada com Sucesso!";


            return RedirectToAction("Index");
        }
    }
}
