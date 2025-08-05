using BudgetTracker.Infrastructure.Enums;

namespace BudgetTracker.Infrastructure.Models;

public class RecurringTransaction
{
    public int Id { get; set; }
    public required  string Title { get; set; }
    public required decimal Amount { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public RecurrencePattern Pattern { get; set; }
    public TransactionType Type { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int CategoryId { get; set; }

    public Category Category { get; set; }

    public ICollection<Transaction> GeneratedTransactions { get; set; } = new List<Transaction>();
}