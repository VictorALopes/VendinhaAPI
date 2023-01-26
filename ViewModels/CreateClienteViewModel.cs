using System.ComponentModel.DataAnnotations;

namespace Vendinha.ViewModels
{
    public class CreateClienteViewModel
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public string CPF { get; set; }
        [Required]
        public DateTime DataNascimento { get; set; }

        public int Idade { get; set; }
        public string? Email { get; set; }
    }
}