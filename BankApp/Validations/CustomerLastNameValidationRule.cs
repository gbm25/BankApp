﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BankApp.Validations
{
    internal class CustomerLastNameValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex ValidNamePattern = new(@"^['’\p{L}\p{M}]+([-'’\p{L}\p{M}]|\s)+[-'’\p{L}\p{M}]$");

            if (value is not string LastName)
            {
                return new ValidationResult(false, "El valor debe de ser un string.");
            }
            else if (!ValidNamePattern.IsMatch(LastName))
            {
                return new ValidationResult(false, "El apellido solo puede contener letras de la 'A' a la 'Z', puntos, espacios y no empezar y/o acabar con espacio.");
            }
            else if (LastName.Length >= 50)
            {
                return new ValidationResult(false, "El valor escrito supera el número máximo de caracteres permitidos (máximo 50 caracteres).");
            }
            else if (LastName.Length <= 2)
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
