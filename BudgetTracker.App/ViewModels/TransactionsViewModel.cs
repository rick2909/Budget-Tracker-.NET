using System.Collections.ObjectModel;
using System.Windows.Input;
using BudgetTracker.Infrastructure.Enums;
using BudgetTracker.Infrastructure.Models;

namespace BudgetTracker.App.ViewModels;

public class TransactionGroup : ObservableCollection<TransactionItemViewModel>
{
    public string GroupName { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public bool IsExpanded { get; set; } = true;
    public ICommand ToggleExpandCommand { get; set; }

    public TransactionGroup(string groupName, decimal totalAmount)
    {
        GroupName = groupName;
        TotalAmount = totalAmount;
        ToggleExpandCommand = new Command(ToggleExpand);
    }

    private void ToggleExpand()
    {
        IsExpanded = !IsExpanded;
        // TODO: Implement expand/collapse logic
    }
}

public class TransactionsViewModel : BaseViewModel
{
    private ObservableCollection<TransactionGroup> _groupedTransactions;
    private ObservableCollection<Category> _categories;
    private List<Transaction> _allTransactions;

    public TransactionsViewModel()
    {
        _groupedTransactions = new ObservableCollection<TransactionGroup>();
        _categories = new ObservableCollection<Category>();
        _allTransactions = new List<Transaction>();
        
        EditTransactionCommand = new Command<TransactionItemViewModel>(OnEditTransaction);
        DeleteTransactionCommand = new Command<TransactionItemViewModel>(OnDeleteTransaction);
        
        LoadData();
    }

    public ObservableCollection<TransactionGroup> GroupedTransactions
    {
        get => _groupedTransactions;
        set => SetProperty(ref _groupedTransactions, value);
    }

    public ObservableCollection<Category> Categories
    {
        get => _categories;
        set => SetProperty(ref _categories, value);
    }

    public ICommand EditTransactionCommand { get; }
    public ICommand DeleteTransactionCommand { get; }

    public void ApplyFilters(int dateRangeIndex, Category? selectedCategory, 
                           int typeIndex, int sortIndex)
    {
        var filteredTransactions = _allTransactions.AsEnumerable();

        // Apply date filter
        var now = DateTime.Now;
        filteredTransactions = dateRangeIndex switch
        {
            0 => filteredTransactions.Where(t => t.Date.Month == now.Month && t.Date.Year == now.Year), // This Month
            1 => filteredTransactions.Where(t => t.Date.Month == now.AddMonths(-1).Month && t.Date.Year == now.AddMonths(-1).Year), // Last Month
            2 => filteredTransactions.Where(t => t.Date >= now.AddMonths(-3)), // Last 3 Months
            3 => filteredTransactions.Where(t => t.Date.Year == now.Year), // This Year
            _ => filteredTransactions // All or Custom Range
        };

        // Apply category filter
        if (selectedCategory != null)
        {
            filteredTransactions = filteredTransactions.Where(t => t.CategoryId == selectedCategory.Id);
        }

        // Apply type filter
        if (typeIndex == 1) // Income only
        {
            filteredTransactions = filteredTransactions.Where(t => t.Type == TransactionType.Income);
        }
        else if (typeIndex == 2) // Expense only
        {
            filteredTransactions = filteredTransactions.Where(t => t.Type == TransactionType.Expense);
        }

        // Apply sorting
        filteredTransactions = sortIndex switch
        {
            0 => filteredTransactions.OrderByDescending(t => t.Date), // Date (Newest)
            1 => filteredTransactions.OrderBy(t => t.Date), // Date (Oldest)
            2 => filteredTransactions.OrderByDescending(t => t.Amount), // Amount (High to Low)
            3 => filteredTransactions.OrderBy(t => t.Amount), // Amount (Low to High)
            4 => filteredTransactions.OrderBy(t => t.Title), // Title (A-Z)
            _ => filteredTransactions.OrderByDescending(t => t.Date)
        };

        GroupTransactions(filteredTransactions.ToList());
    }

    private void GroupTransactions(List<Transaction> transactions)
    {
        GroupedTransactions.Clear();

        var groupedByMonth = transactions
            .GroupBy(t => new { t.Date.Year, t.Date.Month })
            .OrderByDescending(g => new DateTime(g.Key.Year, g.Key.Month, 1));

        foreach (var group in groupedByMonth)
        {
            var monthName = new DateTime(group.Key.Year, group.Key.Month, 1).ToString("MMMM yyyy");
            var totalAmount = group.Sum(t => t.Type == TransactionType.Income ? t.Amount : -t.Amount);
            
            var transactionGroup = new TransactionGroup(monthName, totalAmount);
            
            foreach (var transaction in group.OrderByDescending(t => t.Date))
            {
                transactionGroup.Add(new TransactionItemViewModel(transaction));
            }
            
            GroupedTransactions.Add(transactionGroup);
        }
    }

    private async void OnEditTransaction(TransactionItemViewModel transaction)
    {
        // TODO: Navigate to edit transaction view
        if (Shell.Current != null)
        {
            await Shell.Current.DisplayAlert("Edit", $"Edit transaction: {transaction.Title}", "OK");
        }
    }

    private async void OnDeleteTransaction(TransactionItemViewModel transaction)
    {
        if (Shell.Current != null)
        {
            var result = await Shell.Current.DisplayAlert(
                "Delete Transaction", 
                $"Are you sure you want to delete '{transaction.Title}'?", 
                "Delete", 
                "Cancel");

            if (result)
            {
                // TODO: Delete from database/API
                // For now, just remove from local list
                var transactionToRemove = _allTransactions.FirstOrDefault(t => t.Id == transaction.Id);
                if (transactionToRemove != null)
                {
                    _allTransactions.Remove(transactionToRemove);
                    ApplyFilters(0, null, 0, 0); // Refresh with current filters
                }
            }
        }
    }

    private void LoadData()
    {
        LoadCategories();
        LoadTransactions();
        ApplyFilters(0, null, 0, 0); // Load with default filters
    }

    private void LoadCategories()
    {
        var sampleCategories = new List<Category>
        {
            new() { Id = 1, Name = "Food", Description = "Food and groceries" },
            new() { Id = 2, Name = "Transport", Description = "Transportation costs" },
            new() { Id = 3, Name = "Entertainment", Description = "Entertainment and leisure" },
            new() { Id = 4, Name = "Utilities", Description = "Utilities and bills" },
            new() { Id = 5, Name = "Health", Description = "Healthcare expenses" },
            new() { Id = 6, Name = "Shopping", Description = "Shopping and retail" },
            new() { Id = 7, Name = "Education", Description = "Education and learning" },
            new() { Id = 8, Name = "Travel", Description = "Travel expenses" },
            new() { Id = 9, Name = "Salary", Description = "Salary income" },
            new() { Id = 10, Name = "Freelance", Description = "Freelance income" }
        };

        Categories.Clear();
        Categories.Add(new Category { Id = 0, Name = "All Categories", Description = "Show all" });
        foreach (var category in sampleCategories)
        {
            Categories.Add(category);
        }
    }

    private void LoadTransactions()
    {
        var categories = Categories.Skip(1).ToList(); // Skip "All Categories"
        var random = new Random();

        // Generate sample transactions for the last 6 months
        var sampleTransactions = new List<Transaction>();
        var startDate = DateTime.Now.AddMonths(-6);

        for (int i = 0; i < 50; i++)
        {
            var randomDays = random.Next(0, 180);
            var transactionDate = startDate.AddDays(randomDays);
            var category = categories[random.Next(categories.Count)];
            var isIncome = (category.Name is "Salary" or "Freelance") || random.Next(10) == 0;
            
            sampleTransactions.Add(new Transaction
            {
                Id = i + 1,
                Title = GenerateTransactionTitle(category.Name, isIncome),
                Amount = GenerateRandomAmount(isIncome),
                Date = transactionDate,
                Type = isIncome ? TransactionType.Income : TransactionType.Expense,
                CategoryId = category.Id,
                Category = category,
                CreatedAt = transactionDate,
                UpdatedAt = transactionDate
            });
        }

        _allTransactions = sampleTransactions;
    }

    private static string GenerateTransactionTitle(string categoryName, bool isIncome)
    {
        var titles = categoryName.ToLower() switch
        {
            "food" => new[] { "Grocery Store", "Restaurant", "Coffee Shop", "Fast Food", "Bakery" },
            "transport" => new[] { "Gas Station", "Bus Ticket", "Taxi", "Parking", "Car Service" },
            "entertainment" => new[] { "Movie Theater", "Concert", "Streaming Service", "Games", "Books" },
            "utilities" => new[] { "Electricity Bill", "Water Bill", "Internet", "Phone Bill", "Gas Bill" },
            "health" => new[] { "Pharmacy", "Doctor Visit", "Dentist", "Gym Membership", "Health Insurance" },
            "shopping" => new[] { "Online Store", "Department Store", "Electronics", "Clothing", "Home Goods" },
            "education" => new[] { "Course Fee", "Books", "Online Learning", "Workshop", "Certification" },
            "travel" => new[] { "Hotel", "Flight", "Train Ticket", "Travel Insurance", "Vacation" },
            "salary" => new[] { "Monthly Salary", "Bonus", "Overtime Pay" },
            "freelance" => new[] { "Client Payment", "Project Fee", "Consulting" },
            _ => new[] { "Transaction", "Payment", "Purchase" }
        };

        var random = new Random();
        return titles[random.Next(titles.Length)];
    }

    private static decimal GenerateRandomAmount(bool isIncome)
    {
        var random = new Random();
        
        if (isIncome)
        {
            return random.Next(500, 5000); // Income: $500 - $5000
        }
        
        return random.Next(5, 500); // Expense: $5 - $500
    }
}
