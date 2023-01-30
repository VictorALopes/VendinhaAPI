using System.Net.Mail;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Vendinha.ViewModels.ErrorList;

namespace Vendinha.Utilities
{
    public class Util
    {
        public static int CalculateAge(DateTime DateOfBirth)
        {
            DateTime now = DateTime.Now;
            int AgeInYears = now.Year - DateOfBirth.Year;

            if ( now.DayOfYear < DateOfBirth.DayOfYear )
                AgeInYears--;

            return AgeInYears;
        }

        public static bool IsEmailValid(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public static bool IsCPFValid(string CPF)
        {
               int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
               int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

               CPF = CPF.Trim().Replace(".", "").Replace("-", "");
               if (CPF.Length != 11)
                   return false;

               for (int j = 0; j < 10; j++)
                   if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == CPF)
                       return false;

               string tempCpf = CPF.Substring(0, 9);
               int soma = 0;

               for (int i = 0; i < 9; i++)
                   soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

               int resto = soma % 11;
               if (resto < 2)
                   resto = 0;
               else
                   resto = 11 - resto;

               string digito = resto.ToString();
               tempCpf = tempCpf + digito;
               soma = 0;
               for (int i = 0; i < 10; i++)
                   soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

               resto = soma % 11;
               if (resto < 2)
                   resto = 0;
               else
                   resto = 11 - resto;

               digito = digito + resto.ToString();

               return CPF.EndsWith(digito);
        }
        public static string GetConcatenatedErrorMessages(ModelStateDictionary ModelState)
        {
            return string.Join(Environment.NewLine
                ,ModelState
                .Values
                .SelectMany(e => e.Errors)
                .Select(m => m.ErrorMessage));
        }

        public static ErrorListViewModel GetErrorMessages(ModelStateDictionary ModelState)
        {
            var errors = ModelState
                            .Values
                            .SelectMany(e => e.Errors)
                            .Select(m => m.ErrorMessage);
            
            ErrorListViewModel ErrorObject = new ErrorListViewModel();
            
            foreach (var error in errors)
            {
                ErrorObject.Erros.Add(error.ToString());
            }
            
            return ErrorObject;
        }
    }
}