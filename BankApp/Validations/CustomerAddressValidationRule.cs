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
    internal class CustomerAddressValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex ValidAddressPattern = new(@"^(?=(.*[a-zA-Z]){1,})(?=(.*[0-9])*)(?=(.*[º\-()\\'/])*).+$");

            if (value is not string Address)
            {
                return new ValidationResult(false, "El valor debe de ser un string.");
            }
            else if (!ValidAddressPattern.IsMatch(Address))
            {
                return new ValidationResult(false, @"El valor introducido debe contener al menos una letra y puede contener números y los siguientes caracteres (º\-()\\'/)");
            }
            else if (Address.Length >= 250)
            {
                return new ValidationResult(false, "El valor escrito supera el número máximo de caracteres permitidos (máximo 100 caracteres).");
            }
            else if (Address.Length <= 2)
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
