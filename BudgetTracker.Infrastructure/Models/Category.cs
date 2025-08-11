namespace BudgetTracker.Infrastructure.Models;

public class Category
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    
    public string? Icon { get; set; }
}