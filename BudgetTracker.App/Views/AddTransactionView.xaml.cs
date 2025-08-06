using BudgetTracker.App.ViewModels;
using BudgetTracker.Infrastructure.Enums;

namespace BudgetTracker.App.Views;

public partial class AddTransactionView : ContentPage
{
    public AddTransactionView()
    {
        InitializeComponent();
        BindingContext = new AddTransactionViewModel();
    }
    
    protected override void OnAppearing()
    {
        base.OnAppearing();
        
        // Wire up events safely
        try
        {
            var closeButton = this.FindByName<Button>("CloseButton");
            if (closeButton != null)
                closeButton.Clicked += OnCloseClicked;
                
            var cancelButton = this.FindByName<Button>("CancelButton");
            if (cancelButton != null)
                cancelButton.Clicked += OnCancelClicked;
                
            var saveButton = this.FindByName<Button>("SaveButton");
            if (saveButton != null)
                saveButton.Clicked += OnSaveClicked;
                
            var incomeButton = this.FindByName<Button>("IncomeButton");
            if (incomeButton != null)
                incomeButton.Clicked += OnIncomeClicked;
                
            var expenseButton = this.FindByName<Button>("ExpenseButton");
            if (expenseButton != null)
                expenseButton.Clicked += OnExpenseClicked;
        
            // Set default transaction type to Expense
            SetTransactionType(TransactionType.Expense);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error wiring AddTransactionView events: {ex.Message}");
        }
    }
    
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
    
        var closeButton = this.FindByName<Button>("CloseButton");
        if (closeButton != null)
            closeButton.Clicked -= OnCloseClicked;
        
        var cancelButton = this.FindByName<Button>("CancelButton");
        if (cancelButton != null)
            cancelButton.Clicked -= OnCancelClicked;
        
        var saveButton = this.FindByName<Button>("SaveButton");
        if (saveButton != null)
            saveButton.Clicked -= OnSaveClicked;
        
        var incomeButton = this.FindByName<Button>("IncomeButton");
        if (incomeButton != null)
            incomeButton.Clicked -= OnIncomeClicked;
        
        var expenseButton = this.FindByName<Button>("ExpenseButton");
        if (expenseButton != null)
            expenseButton.Clicked -= OnExpenseClicked;
    }

    private async void OnCloseClicked(object? sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    private async void OnCancelClicked(object? sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    private async void OnSaveClicked(object? sender, EventArgs e)
    {
        var viewModel = (AddTransactionViewModel)BindingContext;
        if (await viewModel.SaveTransactionAsync())
        {
            if (viewModel.KeepFormOpen)
            {
                // Clear form but keep it open
                viewModel.ClearForm();
                await DisplayAlert("Success", "Transaction saved successfully!", "OK");
            }
            else
            {
                // Close the modal
                await Navigation.PopModalAsync();
            }
        }
        else
        {
            await DisplayAlert("Error", "Please fill in all required fields.", "OK");
        }
    }

    private void OnIncomeClicked(object? sender, EventArgs e)
    {
        SetTransactionType(TransactionType.Income);
    }

    private void OnExpenseClicked(object? sender, EventArgs e)
    {
        SetTransactionType(TransactionType.Expense);
    }

    private void SetTransactionType(TransactionType type)
    {
        var viewModel = (AddTransactionViewModel)BindingContext;
        viewModel.TransactionType = type;
    }
}
