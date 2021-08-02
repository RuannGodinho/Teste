using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Teste.Models
{
    public class Editoras
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O CNPJ precisa ser preenchido")]
        public int CNPJ { get; set; }
        [Required(ErrorMessage = "O Nome precisa ser preenchido")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O Endereço precisa ser preenchido")]
        public string Endereco { get; set; }
    }
}
