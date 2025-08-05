using System.ComponentModel.DataAnnotations;
using BudgetTracker.Infrastructure.Enums;
using BudgetTracker.Logic.Attributes;

namespace BudgetTracker.Logic.Dtos;

public class BaseTransactionDto
{
    [Required, MaxLength(100)]
    public required  string Title { get; set; }
    [Required, Decimal(18, 2, "This field must be a decimal with up to 18 digits in total maximum of 2 decimal places.")]
    public required decimal Amount { get; set; }
    [Required]
    public required TransactionType Type { get; set; }
    [Required]
    public int CategoryId { get; set; }
}