using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Teste.Models
{
    public class Livros
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O Nome precisa ser preenchido")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "a Edição precisa ser preenchido")]
        public string Edição { get; set; }
        public DateTime Data_de_Lancamento { get; set; }
        [Required(ErrorMessage = "a Editora precisa ser preenchido")]
        public string Editora { get; set; }
        [Required(ErrorMessage = "O Autor precisa ser preenchido")]
        public string Autor { get; set; }
    }
}
