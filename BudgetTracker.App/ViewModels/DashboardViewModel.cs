using System.Collections.ObjectModel;
using BudgetTracker.Infrastructure.Enums;
using BudgetTracker.Infrastructure.Models;

namespace BudgetTracker.App.ViewModels;

public class DashboardViewModel : BaseViewModel
{
    private DateTime _currentDate = DateTime.Now;
    private decimal _totalBalance;
    private decimal _totalIncome;
    private decimal _totalExpenses;
    private ObservableCollection<TransactionItemViewModel> _recentTransactions;

    public DashboardViewModel()
    {
        _recentTransactions = new ObservableCollection<TransactionItemViewModel>();
        LoadSampleData();
    }

    public string CurrentMonth => _currentDate.ToString("MMMM yyyy");

    public decimal TotalBalance
    {
        get => _totalBalance;
        set => SetProperty(ref _totalBalance, value);
    }

    public decimal TotalIncome
    {
        get => _totalIncome;
        set => SetProperty(ref _totalIncome, value);
    }

    public decimal TotalExpenses
    {
        get => _totalExpenses;
        set => SetProperty(ref _totalExpenses, value);
    }

    public string IncomeVsExpenses
    {
        get
        {
            var difference = TotalIncome - TotalExpenses;
            var percentage = TotalExpenses > 0 
                ? (difference / TotalExpenses) * 100 
                : 0;
            return $"{difference:C} ({percentage:+0;-0;0}%)";
        }
    }

    public string IncomeVsExpensesText
    {
        get
        {
            var difference = TotalIncome - TotalExpenses;
            return difference >= 0 ? "Surplus" : "Deficit";
        }
    }

    public string IncomeVsExpensesPercentage
    {
        get
        {
            var difference = TotalIncome - TotalExpenses;
            var percentage = TotalIncome > 0 
                ? Math.Abs(difference / TotalIncome) * 100 
                : 0;
            return $"{percentage:F1}%";
        }
    }

    public Color IncomeVsExpensesColor
    {
        get
        {
            var difference = TotalIncome - TotalExpenses;
            return difference >= 0 
                ? Color.FromArgb("#0F7B0F") // Success color
                : Color.FromArgb("#C50E20"); // Danger color
        }
    }

    public ObservableCollection<TransactionItemViewModel> RecentTransactions
    {
        get => _recentTransactions;
        set => SetProperty(ref _recentTransactions, value);
    }

    public void NavigateToPreviousMonth()
    {
        _currentDate = _currentDate.AddMonths(-1);
        OnPropertyChanged(nameof(CurrentMonth));
        LoadDataForCurrentMonth();
    }

    public void NavigateToNextMonth()
    {
        _currentDate = _currentDate.AddMonths(1);
        OnPropertyChanged(nameof(CurrentMonth));
        LoadDataForCurrentMonth();
    }

    private void LoadDataForCurrentMonth()
    {
        // TODO: Load actual data from API/Service
        LoadSampleData();
    }

    private void LoadSampleData()
    {
        // Sample categories
        var categories = new List<Category>
        {
            new() { Id = 1, Name = "Food", Description = "Food and groceries" },
            new() { Id = 2, Name = "Transport", Description = "Transportation" },
            new() { Id = 3, Name = "Entertainment", Description = "Entertainment" },
            new() { Id = 4, Name = "Salary", Description = "Salary income" },
            new() { Id = 5, Name = "Utilities", Description = "Utilities" }
        };

        // Sample transactions
        var sampleTransactions = new List<Transaction>
        {
            new()
            {
                Id = 1,
                Title = "Fresh Foods Market",
                Amount = 75.25m,
                Date = DateTime.Now.AddDays(-1),
                Type = TransactionType.Expense,
                CategoryId = 1,
                Category = categories[0],
                CreatedAt = DateTime.Now.AddDays(-1),
                UpdatedAt = DateTime.Now.AddDays(-1)
            },
            new()
            {
                Id = 2,
                Title = "Power Company",
                Amount = 150.00m,
                Date = DateTime.Now.AddDays(-2),
                Type = TransactionType.Expense,
                CategoryId = 5,
                Category = categories[4],
                CreatedAt = DateTime.Now.AddDays(-2),
                UpdatedAt = DateTime.Now.AddDays(-2)
            },
            new()
            {
                Id = 3,
                Title = "Acme Corp",
                Amount = 3100.00m,
                Date = DateTime.Now.AddDays(-3),
                Type = TransactionType.Income,
                CategoryId = 4,
                Category = categories[3],
                CreatedAt = DateTime.Now.AddDays(-3),
                UpdatedAt = DateTime.Now.AddDays(-3)
            },
            new()
            {
                Id = 4,
                Title = "Coffee Shop",
                Amount = 4.75m,
                Date = DateTime.Now.AddDays(-4),
                Type = TransactionType.Expense,
                CategoryId = 1,
                Category = categories[0],
                CreatedAt = DateTime.Now.AddDays(-4),
                UpdatedAt = DateTime.Now.AddDays(-4)
            },
            new()
            {
                Id = 5,
                Title = "Online Shopping",
                Amount = 156.00m,
                Date = DateTime.Now.AddDays(-5),
                Type = TransactionType.Expense,
                CategoryId = 3,
                Category = categories[2],
                CreatedAt = DateTime.Now.AddDays(-5),
                UpdatedAt = DateTime.Now.AddDays(-5)
            }
        };

        // Calculate totals
        TotalIncome = sampleTransactions
            .Where(t => t.Type == TransactionType.Income)
            .Sum(t => t.Amount);
        
        TotalExpenses = sampleTransactions
            .Where(t => t.Type == TransactionType.Expense)
            .Sum(t => t.Amount);
        
        TotalBalance = TotalIncome - TotalExpenses;

        // Update recent transactions
        RecentTransactions.Clear();
        foreach (var transaction in sampleTransactions
                     .OrderByDescending(t => t.Date)
                     .Take(5))
        {
            RecentTransactions.Add(new TransactionItemViewModel(transaction));
        }

        // Notify UI of changes
        OnPropertyChanged(nameof(IncomeVsExpenses));
        OnPropertyChanged(nameof(IncomeVsExpensesText));
        OnPropertyChanged(nameof(IncomeVsExpensesPercentage));
        OnPropertyChanged(nameof(IncomeVsExpensesColor));
    }
}
