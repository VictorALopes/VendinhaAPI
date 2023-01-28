using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vendinha.Data;

namespace Vendinha.Helpers.Divida
{
    public static class DividaHelper
    {
        public static bool ClienteTemDividaPendente(string CPF, [FromServices] AppDbContext context)
        {
            if( context.Dividas.AsNoTracking().FirstOrDefault(x => x.CPF == CPF && x.pago == false) != null )
                return true;
            
            return false;
        }

        public class Mensagens
        {
            public static string TemDividaPendente => "Não foi possível cadastrar a dívida pois o cliente já possui uma dívida não quitada.";
            public static string DividaJaFoiPaga => "Esta dívida já está paga e por isso não pode mais ser alterada.";
            public static string DividaNaoEncontrada => "Uma dívida com este Id não foi encontrada";
            public static string ClienteNaoTemDividas => "Nenhuma dívida foi encontrada para este cliente";
        }
    }
}