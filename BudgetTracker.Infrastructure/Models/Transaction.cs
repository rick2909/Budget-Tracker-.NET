namespace BudgetTracker.Infrastructure.Models;

public class Transaction
{
    public int Id { get; set; }
    public string Title { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Type { get; set; }
    public int CategoryId { get; set; }
    
    public Category Category { get; set; }
}