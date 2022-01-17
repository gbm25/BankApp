using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace BankApp.Validations
{
    internal class CustomerLastNameValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex ValidNamePattern = new(@"^['’\p{L}\p{M}]+([-'’\p{L}\p{M}]|\s)+[-'’\p{L}\p{M}]$");

            if (value is string LastName && LastName.Length > 0)
            {
                if (!ValidNamePattern.IsMatch(LastName))
                {
                    return new ValidationResult(false, "El apellido solo puede contener letras de la 'A' a la 'Z', puntos, espacios y no empezar y/o acabar con espacio.");
                }
                if (LastName.Length >= 50)
                {
                    return new ValidationResult(false, "El valor escrito supera el número máximo de caracteres permitidos (máximo 50 caracteres).");
                }
                if (LastName.Length < 2)
                {
                    return new ValidationResult(false, "El valor escrito no supera el número mínimo de caracteres permitidos (mínimo 2 caracteres).");
                }
            }
            return ValidationResult.ValidResult;
        }
    }
}
