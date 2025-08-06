using BudgetTracker.Infrastructure.Enums;
using BudgetTracker.Infrastructure.Models;

namespace BudgetTracker.App.ViewModels;

public class TransactionItemViewModel : BaseViewModel
{
    private readonly Transaction _transaction;

    public TransactionItemViewModel(Transaction transaction)
    {
        _transaction = transaction;
    }

    public int Id => _transaction.Id;
    public string Title => _transaction.Title;
    public decimal Amount => _transaction.Amount;
    public DateTime Date => _transaction.Date;
    public TransactionType Type => _transaction.Type;
    public string CategoryName => _transaction.Category?.Name ?? "Uncategorized";

    public string FormattedAmount => Type == TransactionType.Income 
        ? $"+{Amount:C}" 
        : $"-{Amount:C}";

    public Color AmountColor => Type == TransactionType.Income 
        ? Color.FromArgb("#0F7B0F") // Success color
        : Color.FromArgb("#C50E20"); // Danger color

    public string CategoryIcon => GetCategoryIcon(CategoryName);

    private static string GetCategoryIcon(string categoryName)
    {
        return categoryName.ToLower() switch
        {
            "food" or "groceries" => "🛒",
            "transport" or "transportation" => "🚗",
            "entertainment" => "🎬",
            "utilities" => "⚡",
            "health" => "🏥",
            "shopping" => "🛍️",
            "education" => "📚",
            "travel" => "✈️",
            "salary" => "💰",
            "freelance" => "💼",
            "investment" => "📈",
            "rent" => "🏠",
            "insurance" => "🛡️",
            _ => "💳"
        };
    }
}
