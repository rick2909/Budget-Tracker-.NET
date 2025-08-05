using BudgetTracker.Infrastructure.Enums;
using BudgetTracker.Infrastructure.Models;

namespace BudgetTracker.Logic.Dtos;

public class UpdateTransactionDto
{
    public string? Title { get; set; }
    public decimal? Amount { get; set; }
    public DateTime? Date { get; set; }
    public TransactionType? Type { get; set; }
    public int? CategoryId { get; set; }
}