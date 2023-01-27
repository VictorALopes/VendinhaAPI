using System.ComponentModel.DataAnnotations;

namespace Vendinha.Models
{
    public class Cliente
    {
        [Key]
        [Required]
        public string CPF { get; set; }
        [Required]
        public string nome { get; set; }
        [Required]
        public DateTime dataNascimento { get; set; }

        public int idade { get; set; }

        public string? email { get; set; }
    }
}