using System.ComponentModel.DataAnnotations;

namespace Vendinha.ViewModels.Cliente
{
    public class PostViewModel
    {
        [Required]
        public string nome { get; set; }
        [Required]
        public string CPF { get; set; }
        [Required]
        public DateTime dataNascimento { get; set; }
        public string? email { get; set; }
    }

    public class PutViewModel
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