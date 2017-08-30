using System.Globalization;
using System.Windows.Controls;
using System.Windows.Input;

namespace POS.ViewModel
{
    public class PositiveDecimalValidationRule : ValidationRule
    {
        public PositiveDecimalValidationRule() { }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            decimal n;
            bool result = decimal.TryParse(value.ToString(), out n) && n > 0;

            return result ? ValidationResult.ValidResult : new ValidationResult(false, "Должно быть больше нуля");
        }
    }
}
