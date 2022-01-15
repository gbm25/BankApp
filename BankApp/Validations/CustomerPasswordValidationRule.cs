using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace BankApp.Validations
{
    internal class CustomerPasswordValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex ValidPasswordPattern = new(@"^(?=(.*[a-z]){1,})(?=(.*[A-Z]){1,})(?=(.*[0-9]){1,})(?=(.*[!@#$%^&*()[\]{}\-_+.]){1,}).+");

            if (value is string Password && Password.Length > 0)
            {
                if (!ValidPasswordPattern.IsMatch(Password))
                {
                    return new ValidationResult(false, "La contraseña debe contener al menos una mayúscula, una minúscula, un numero y un carácter especial.");
                }
                if (Password.Length >= 100)
                {
                    return new ValidationResult(false, "El valor escrito supera el número máximo de caracteres permitidos (máximo 100 caracteres).");
                }
                if (Password.Length < 8)
                {
                    return new ValidationResult(false, "El valor escrito no supera el número mínimo de caracteres permitidos (mínimo 8 caracteres).");
                }
            }
            return ValidationResult.ValidResult;
        }
    }
}
