using System.ComponentModel.DataAnnotations;
using BudgetTracker.Logic.Validators;

namespace BudgetTracker.Logic.Attributes;

public class MaterialIconAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
        {
            return true; // Allow null/empty values (use [Required] separately if needed)
        }

        return MaterialIconValidator.IsValidMaterialIcon(value.ToString());
    }

    public override string FormatErrorMessage(string name)
    {
        return $"The {name} field must contain a valid Material Design icon name. " +
               $"Valid examples: shopping_cart, restaurant, directions_car, home, etc.";
    }
}
