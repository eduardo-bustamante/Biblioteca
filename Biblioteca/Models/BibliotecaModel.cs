using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Models
{
    public class BibliotecaModel
    {
         public int? Id { get; set; }
        public string Titulo { get; set; }
        public string? Autor { get; set; }
        public string? Colecao { get; set; }
        public int? Volume { get; set; }
        public int Paginas { get; set; }
        public string? Idioma { get; set; }
        public int? Ano { get; set; }
        public string? Editora { get; set; }
        public string? Tema { get; set; }
        public string? Capa { get; set; }
        public DateTime DataCadastro { get; set; }
        public Boolean Emprestado { get; set; }

    }
}
