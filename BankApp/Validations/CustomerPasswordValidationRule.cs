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
    internal class CustomerPasswordValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex ValidPasswordPattern = new(@"^(?=(.*[a-z]){1,})(?=(.*[A-Z]){1,})(?=(.*[0-9]){1,})(?=(.*[!@#$%^&*()[\]{}\-_+.]){1,}).+");

            if (value is not string Password)
            {
                return new ValidationResult(false, "El valor debe de ser un string.");
            }
            else if (!ValidPasswordPattern.IsMatch(Password))
            {
                return new ValidationResult(false, "La contraseña debe contener al menos una mayúscula, una minúscula, un numero y un carácter especial.");
            }
            else if (Password.Length >= 100)
            {
                return new ValidationResult(false, "El valor escrito supera el número máximo de caracteres permitidos (máximo 100 caracteres).");
            }
            else if (Password.Length <= 8)
            {
                return new ValidationResult(false, "El valor escrito no supera el número mínimo de caracteres permitidos (mínimo 8 caracteres).");
            }
            else
            {
                return ValidationResult.ValidResult;
            }
        }
    }
}
