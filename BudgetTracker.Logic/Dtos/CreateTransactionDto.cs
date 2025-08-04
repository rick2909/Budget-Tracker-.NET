using System.ComponentModel.DataAnnotations;
using BudgetTracker.Infrastructure.Models;
using BudgetTracker.Logic.Attributes;

namespace BudgetTracker.Logic.Dtos;

public class CreateTransactionDto
{
    [MaxLength(100)]
    public required  string Title { get; set; }
    [Decimal(18, 2, "This field must be a decimal with up to 18 digits in total maximum of 2 decimal places.")]
    public required decimal Amount { get; set; }
    public required  DateTime Date { get; set; }
    public required TransactionType Type { get; set; }
    public int CategoryId { get; set; }
    
    public Category? Category { get; set; }
}