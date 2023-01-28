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
    }
}