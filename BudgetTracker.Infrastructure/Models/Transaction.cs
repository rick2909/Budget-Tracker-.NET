namespace BudgetTracker.Infrastructure.Models;

public class Transaction
{
    public int Id { get; set; }
    public required  string Title { get; set; }
    public required decimal Amount { get; set; }
    public required  DateTime Date { get; set; }
    public required TransactionType Type { get; set; }
    public int CategoryId { get; set; }
    
    public Category? Category { get; set; }
}