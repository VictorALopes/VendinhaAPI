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
            public static string TemDividaPendente { get { return new string("Não foi possível cadastrar a dívida pois o cliente já possui uma dívida não quitada."); } }
            public static string Mensagem { get {return new string("minha msg"); } }
            public static string DividaJaFoiPaga { get {return new string("Esta dívida já está paga e por isso não pode mais ser alterada."); } }
            public static string DividaNaoEncontrada { get {return new string("Uma dívida com este Id não foi encontrada"); } }
            public static string ClienteNaoTemDividas { get {return new string("Nenhuma dívida foi encontrada para este cliente"); } }


            // return UnprocessableEntity("Esta dívida já está paga e por isso não pode mais ser alterada.");
            //  return NotFound("Uma dívida com este Id não foi encontrada");
        }
    }
}