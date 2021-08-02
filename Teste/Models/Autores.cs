using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Teste.Models
{
    public class Autores
    {
        public int Id { get; set; }
        [Required( ErrorMessage = "O Nome precisa ser preenchido")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O Campo Cpf é obrigatorio")]
        public int CPF { get; set; }
        [Required(ErrorMessage = "O Celular precisa ser preenchido")]
        public int Celular { get; set; }
        [Required(ErrorMessage = "O Email precisa ser preenchido")]
        public string Email { get; set; }
    }
}
