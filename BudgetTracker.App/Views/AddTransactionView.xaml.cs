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
        
        // Wire up events after the page appears
        if (CloseButton != null)
            CloseButton.Clicked += OnCloseClicked;
        if (CancelButton != null)
            CancelButton.Clicked += OnCancelClicked;
        if (SaveButton != null)
            SaveButton.Clicked += OnSaveClicked;
        if (IncomeButton != null)
            IncomeButton.Clicked += OnIncomeClicked;
        if (ExpenseButton != null)
            ExpenseButton.Clicked += OnExpenseClicked;
        
        // Set default transaction type to Expense
        SetTransactionType(TransactionType.Expense);
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
