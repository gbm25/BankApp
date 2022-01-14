using System.Globalization;
using System.Windows.Controls;

namespace BankApp.Validations
{
    public class CustomerIDValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {

            if (!int.TryParse(value.ToString(), out int number))
            {
                return new ValidationResult(false, "El valor debe de ser un número.");
            }
            else if (number <= 0)
            {
                return new ValidationResult(false, "El valor debe de ser un número mayor que 0");
            }
            else
            {
                return ValidationResult.ValidResult;
            }

        }
    }
}
