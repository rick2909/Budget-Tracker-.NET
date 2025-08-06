using System.Collections.ObjectModel;
using BudgetTracker.Infrastructure.Enums;
using BudgetTracker.Infrastructure.Models;

namespace BudgetTracker.App.ViewModels;

public class AddTransactionViewModel : BaseViewModel
{
    private string _title = string.Empty;
    private string _amount = string.Empty;
    private DateTime _date = DateTime.Now;
    private TimeSpan _time = DateTime.Now.TimeOfDay;
    private string _notes = string.Empty;
    private Category? _selectedCategory;
    private TransactionType _transactionType = TransactionType.Expense;
    private bool _keepFormOpen;
    private ObservableCollection<Category> _categories;

    public AddTransactionViewModel()
    {
        _categories = new ObservableCollection<Category>();
        LoadCategories();
    }

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public string Amount
    {
        get => _amount;
        set => SetProperty(ref _amount, value);
    }

    public DateTime Date
    {
        get => _date;
        set => SetProperty(ref _date, value);
    }

    public TimeSpan Time
    {
        get => _time;
        set => SetProperty(ref _time, value);
    }

    public string Notes
    {
        get => _notes;
        set => SetProperty(ref _notes, value);
    }

    public Category? SelectedCategory
    {
        get => _selectedCategory;
        set => SetProperty(ref _selectedCategory, value);
    }

    public TransactionType TransactionType
    {
        get => _transactionType;
        set => SetProperty(ref _transactionType, value);
    }

    public bool KeepFormOpen
    {
        get => _keepFormOpen;
        set => SetProperty(ref _keepFormOpen, value);
    }

    public ObservableCollection<Category> Categories
    {
        get => _categories;
        set => SetProperty(ref _categories, value);
    }

    public async Task<bool> SaveTransactionAsync()
    {
        // Validate required fields
        if (string.IsNullOrWhiteSpace(Title) || 
            string.IsNullOrWhiteSpace(Amount) || 
            SelectedCategory == null)
        {
            return false;
        }

        if (!decimal.TryParse(Amount, out var amountValue) || amountValue <= 0)
        {
            return false;
        }

        // Create transaction object
        var transaction = new Transaction
        {
            Title = Title.Trim(),
            Amount = amountValue,
            Date = Date.Date.Add(Time),
            Type = TransactionType,
            CategoryId = SelectedCategory.Id,
            Category = SelectedCategory,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        // TODO: Save to database/API
        // For now, just simulate success
        await Task.Delay(500); // Simulate API call

        return true;
    }

    public void ClearForm()
    {
        Title = string.Empty;
        Amount = string.Empty;
        Date = DateTime.Now;
        Time = DateTime.Now.TimeOfDay;
        Notes = string.Empty;
        SelectedCategory = null;
    }

    private void LoadCategories()
    {
        // Sample categories - in real app, load from API/database
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
            new() { Id = 10, Name = "Freelance", Description = "Freelance income" },
            new() { Id = 11, Name = "Investment", Description = "Investment returns" },
            new() { Id = 12, Name = "Rent", Description = "Rent and housing" },
            new() { Id = 13, Name = "Insurance", Description = "Insurance payments" }
        };

        Categories.Clear();
        foreach (var category in sampleCategories)
        {
            Categories.Add(category);
        }
    }
}
