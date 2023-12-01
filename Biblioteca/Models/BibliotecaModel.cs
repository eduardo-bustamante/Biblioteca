using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Models
{
    public class BibliotecaModel
    {
         public int Id { get; set; }
        [Required(ErrorMessage = "Digite o Nome do Livro")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "Digite o Nome do Autor")]
        public string Autor { get; set; }
        public string Colecao { get; set; }
        public int Volume { get; set; }
        [Required(ErrorMessage = "Digite o numero de páginas")]
        public int Paginas { get; set; }
        [Required(ErrorMessage = "Digite o Idioma")]
        public string Idioma { get; set; }
        public int Ano { get; set; }
        [Required(ErrorMessage = "Digite a Editora")]
        public string Editora { get; set; }
        public string Tema { get; set; }
        public string Capa { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public Boolean Emprestado { get; set; }

    }
}
