using System.Globalization;
using System.Windows.Controls;

namespace BankApp.Validations
{
    public class CustomerIDValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is string ID && ID.Length > 0)
            {
                if (!int.TryParse(ID, out int number))
                {
                    return new ValidationResult(false, "El valor debe de ser un número.");
                }
                if (number <= 0)
                {
                    return new ValidationResult(false, "El valor debe de ser un número mayor que 0");
                }
            }
            return ValidationResult.ValidResult;
        }
    }
}
