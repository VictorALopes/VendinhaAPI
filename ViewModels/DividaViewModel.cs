using System.ComponentModel.DataAnnotations;

namespace Vendinha.ViewModels.Divida
{
    public class PostViewModel
    {
        [Required]
        public double valor { get; set; }
        
        [Required]
        public string CPF { get; set; }

    }

    public class PutViewModel
    {
        [Required]
        public int id { get; set; }
        [Required]
        public double valor { get; set; }
        public DateTime? dataPagamento { get; set; } 
        [Required]
        public string CPF { get; set; }  
    }
}