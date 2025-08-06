using BudgetTracker.App.ViewModels;

namespace BudgetTracker.App.Views;

public partial class TransactionsView : ContentPage
{
    public TransactionsView()
    {
        InitializeComponent();
        BindingContext = new TransactionsViewModel();
        
        // Wire up events
        AddTransactionButton.Clicked += OnAddTransactionClicked;
        DateRangePicker.SelectedIndexChanged += OnFilterChanged;
        CategoryPicker.SelectedIndexChanged += OnFilterChanged;
        TypePicker.SelectedIndexChanged += OnFilterChanged;
        SortPicker.SelectedIndexChanged += OnFilterChanged;
        
        // Set default values
        DateRangePicker.SelectedIndex = 0; // This Month
        TypePicker.SelectedIndex = 0; // All
        SortPicker.SelectedIndex = 0; // Date (Newest)
    }

    private async void OnAddTransactionClicked(object? sender, EventArgs e)
    {
        var addTransactionView = new AddTransactionView();
        await Navigation.PushModalAsync(addTransactionView);
    }

    private void OnFilterChanged(object? sender, EventArgs e)
    {
        if (BindingContext is TransactionsViewModel viewModel)
        {
            viewModel.ApplyFilters(
                DateRangePicker.SelectedIndex,
                CategoryPicker.SelectedItem as BudgetTracker.Infrastructure.Models.Category,
                TypePicker.SelectedIndex,
                SortPicker.SelectedIndex
            );
        }
    }
}
