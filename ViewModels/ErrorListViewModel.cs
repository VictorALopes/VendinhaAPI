using System.ComponentModel.DataAnnotations;

namespace Vendinha.ViewModels.ErrorList
{
    public class ErrorListViewModel
    {
        public List<string> Erros { get; set; } = new List<string>();
    }
}