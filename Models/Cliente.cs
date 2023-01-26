using System.ComponentModel.DataAnnotations;

namespace Vendinha.Models
{
    public class Cliente
    {
        [Key]
        public string nome { get; set; }
        public string CPF { get; set; }
        public DateTime dataNascimento { get; set; }
        public int idade { get; set; }
        public string email { get; set; }
    }
}