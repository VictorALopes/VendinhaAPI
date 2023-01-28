using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vendinha.Models
{
    public class Divida
    {
        [Key]
        public int id { get; set; }
        public double valor { get; set; }
        public bool pago { get; set; }
        public DateTime dataCriacao { get; set; }
        public DateTime? dataPagamento { get; set; } 
        
           // Foreign key   
            [Display(Name = "cliente")]  
            public virtual string CPF { get; set; }  

            [ForeignKey("CPF")]  
            public virtual Cliente cliente { get; set; }  
    }
}