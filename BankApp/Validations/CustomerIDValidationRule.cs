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
    public class CustomerIDValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
        
            if (!int.TryParse(value.ToString(),out int number))
            {
                return new ValidationResult(false, "El valor debe de ser un número.");
            }
            else if (number < 0)
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
