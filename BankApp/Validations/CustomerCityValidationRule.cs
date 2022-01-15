using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace BankApp.Validations
{
    internal class CustomerCityValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex ValidCityPattern = new(@"^['’\p{L}\p{M}]+([-'’\p{L}\p{M}]|\s)+[-'’\p{L}\p{M}]$");

            if (value is string City && City.Length > 0)
            {
                if (!ValidCityPattern.IsMatch(City))
                {
                    return new ValidationResult(false, "El valor introducido solo puede contener letras");
                }
                if (City.Length >= 100)
                {
                    return new ValidationResult(false, "El valor escrito supera el número máximo de caracteres permitidos (máximo 100 caracteres).");
                }
                if (City.Length < 2)
                {
                    return new ValidationResult(false, "El valor escrito no supera el número mínimo de caracteres permitidos (mínimo 2 caracteres).");
                }
            }
            return ValidationResult.ValidResult;
        }
    }
}
