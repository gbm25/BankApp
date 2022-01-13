using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BankApp.Validations
{
    internal class CustomerUsernameValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex ValidUsernamePattern = new(@"^[a-zA-Z0-9]*$");

            if (value is not string Username)
            {
                return new ValidationResult(false, "El valor debe de ser un string.");
            }
            else if (!ValidUsernamePattern.IsMatch(Username))
            {
                return new ValidationResult(false, "El nombre de usuario solo puede contener números y letras.");
            }
            else if (Username.Length >= 50)
            {
                return new ValidationResult(false, "Nombre de usuario demasiado largo (máximo 50 caracteres).");
            }
            else if (Username.Length <= 2)
            {
                return new ValidationResult(false, "El valor escrito no supera el número mínimo de caracteres permitidos (mínimo 2 caracteres).");
            }
            else
            {
                return ValidationResult.ValidResult;
            }
        }
    }
}
