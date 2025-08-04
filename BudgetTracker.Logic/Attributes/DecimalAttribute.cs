using System;
using System.ComponentModel.DataAnnotations;

namespace BudgetTracker.Logic.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class DecimalAttribute : ValidationAttribute
    {
        public int Precision { get; }
        public int Scale { get; }

        public DecimalAttribute(int precision, int scale)
            : base()
        {
            Precision = precision;
            Scale = scale;
        }

        public DecimalAttribute(int precision, int scale, string errorMessage)
            : base(errorMessage)
        {
            Precision = precision;
            Scale = scale;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            decimal decimalValue;
            if (value is decimal d)
            {
                decimalValue = d;
            }
            else if (value is string s)
            {
                string decimalString = s.Contains(',') ? s.Replace(',', '.') : s;
                decimal.TryParse(decimalString, out decimalValue);
            }
            else
            {
                string typeErrorMsg = ErrorMessage ?? $"The field {validationContext.DisplayName} must be a decimal value.";
                return new ValidationResult(typeErrorMsg);
            }

            // Count digits and decimals
            string[] parts = decimalValue.ToString(System.Globalization.CultureInfo.InvariantCulture).Split('.');
            int integerLength = parts[0].TrimStart('-').Length;
            int fractionLength = parts.Length > 1 ? parts[1].Length : 0;
            if (integerLength + fractionLength > Precision || fractionLength > Scale)
            {
                string errorMsg = ErrorMessage ?? $"The field {validationContext.DisplayName} must be a decimal with up to {Precision} digits in total and {Scale} decimal places.";
                return new ValidationResult(errorMsg);
            }
            return ValidationResult.Success;
        }
    }
}
