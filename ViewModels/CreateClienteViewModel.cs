using System.ComponentModel.DataAnnotations;

namespace Vendinha.ViewModels
{
    public class CreateClienteViewModel
    {
        [Required]
        public string nome { get; set; }
        [Required]
        public string CPF { get; set; }
        [Required]
        public DateTime dataNascimento { get; set; }
        public string? email { get; set; }
    }
}