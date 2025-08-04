using BudgetTracker.Infrastructure.Enums;

namespace BudgetTracker.Infrastructure.Models;

public class Transaction
{
    public int Id { get; set; }
    public required  string Title { get; set; }
    public required decimal Amount { get; set; }
    public required  DateTime Date { get; set; }
    public required TransactionType Type { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int? CategoryId { get; set; }
    public int? RecurringTransactionId { get; set; }
    
    public Category? Category { get; set; }
    public RecurringTransaction? RecurringTransaction { get; set; }
}