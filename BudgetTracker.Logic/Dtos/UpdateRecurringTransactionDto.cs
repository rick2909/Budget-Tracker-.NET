using System.ComponentModel.DataAnnotations;
using BudgetTracker.Infrastructure.Enums;

namespace BudgetTracker.Logic.Dtos;

public class UpdateRecurringTransactionDto
{
    public string? Title { get; set; }
    public decimal? Amount { get; set; }
    public TransactionType? Type { get; set; }
    public int? CategoryId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public RecurrencePattern? RecurrencePattern { get; set; }
}
