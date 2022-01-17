using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace BankApp.Validations
{
    internal class CustomerAddressValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex ValidAddressPattern = new(@"^(?=(.*\p{L}){1,})(?=(.*[0-9])*)(?=(.*[º\-()\\'/])*).+$");

            if (value is string Address && Address.Length>0)
            {
                if (!ValidAddressPattern.IsMatch(Address))
                {
                    return new ValidationResult(false, @"El valor introducido debe contener al menos una letra y puede contener números y los siguientes caracteres (º\-()\\'/)");
                }
                if (Address.Length >= 250)
                {
                    return new ValidationResult(false, "El valor escrito supera el número máximo de caracteres permitidos (máximo 100 caracteres).");
                }
                if (Address.Length < 2)
                {
                    return new ValidationResult(false, "El valor escrito no supera el número mínimo de caracteres permitidos (mínimo 2 caracteres).");
                }
            }

            return ValidationResult.ValidResult;
        }
    }
}
