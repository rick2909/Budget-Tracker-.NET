using BudgetTracker.App.ViewModels;
using BudgetTracker.App.Components;

namespace BudgetTracker.App.Views;

public partial class TransactionsView : ContentPage
{
    public TransactionsView()
    {
        InitializeComponent();
        BindingContext = new TransactionsViewModel();
    }
    
    protected override void OnAppearing()
    {
        base.OnAppearing();
        
        // Wire up filter events
        var allFilter = this.FindByName<Button>("AllFilterButton");
        if (allFilter != null)
            allFilter.Clicked += OnAllFilterClicked;
            
        var incomeFilter = this.FindByName<Button>("IncomeFilterButton");
        if (incomeFilter != null)
            incomeFilter.Clicked += OnIncomeFilterClicked;
            
        var expenseFilter = this.FindByName<Button>("ExpenseFilterButton");
        if (expenseFilter != null)
            expenseFilter.Clicked += OnExpenseFilterClicked;

        // Wire up events safely
        try
        {
            var dateRangePicker = this.FindByName<Picker>("DateRangePicker");
            if (dateRangePicker != null)
                dateRangePicker.SelectedIndexChanged += OnFilterChanged;
                
            var categoryPicker = this.FindByName<Picker>("CategoryPicker");
            if (categoryPicker != null)
                categoryPicker.SelectedIndexChanged += OnFilterChanged;
                
            var typePicker = this.FindByName<Picker>("TypePicker");
            if (typePicker != null)
                typePicker.SelectedIndexChanged += OnFilterChanged;
                
            var sortPicker = this.FindByName<Picker>("SortPicker");
            if (sortPicker != null)
                sortPicker.SelectedIndexChanged += OnFilterChanged;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error wiring events: {ex.Message}");
        }
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        var allFilter = this.FindByName<Button>("AllFilterButton");
        if (allFilter != null)
            allFilter.Clicked -= OnAllFilterClicked;
            
        var incomeFilter = this.FindByName<Button>("IncomeFilterButton");
        if (incomeFilter != null)
            incomeFilter.Clicked -= OnIncomeFilterClicked;
            
        var expenseFilter = this.FindByName<Button>("ExpenseFilterButton");
        if (expenseFilter != null)
            expenseFilter.Clicked -= OnExpenseFilterClicked;

        var dateRangePicker = this.FindByName<Picker>("DateRangePicker");
        if (dateRangePicker != null)
            dateRangePicker.SelectedIndexChanged -= OnFilterChanged;
            
        var categoryPicker = this.FindByName<Picker>("CategoryPicker");
        if (categoryPicker != null)
            categoryPicker.SelectedIndexChanged -= OnFilterChanged;
            
        var typePicker = this.FindByName<Picker>("TypePicker");
        if (typePicker != null)
            typePicker.SelectedIndexChanged -= OnFilterChanged;
            
        var sortPicker = this.FindByName<Picker>("SortPicker");
        if (sortPicker != null)
            sortPicker.SelectedIndexChanged -= OnFilterChanged;
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
            // Get current filter values from UI controls
            var dateRangePicker = this.FindByName<Picker>("DateRangePicker");
            var categoryPicker = this.FindByName<Picker>("CategoryPicker");
            var typePicker = this.FindByName<Picker>("TypePicker");
            var sortPicker = this.FindByName<Picker>("SortPicker");

            viewModel.ApplyFilters(
                dateRangePicker?.SelectedIndex ?? 0,
                categoryPicker?.SelectedItem as BudgetTracker.Infrastructure.Models.Category,
                typePicker?.SelectedIndex ?? 0,
                sortPicker?.SelectedIndex ?? 0
            );
        }
    }

    private void OnAllFilterClicked(object? sender, EventArgs e)
    {
        if (BindingContext is TransactionsViewModel viewModel)
        {
            // Set type filter to "All" (index 0) and apply filters
            var typePicker = this.FindByName<Picker>("TypePicker");
            if (typePicker != null)
            {
                typePicker.SelectedIndex = 0;
            }
            
            var dateRangePicker = this.FindByName<Picker>("DateRangePicker");
            var categoryPicker = this.FindByName<Picker>("CategoryPicker");
            var sortPicker = this.FindByName<Picker>("SortPicker");

            viewModel.ApplyFilters(
                dateRangePicker?.SelectedIndex ?? 0,
                categoryPicker?.SelectedItem as BudgetTracker.Infrastructure.Models.Category,
                0, // All types
                sortPicker?.SelectedIndex ?? 0
            );
        }
    }

    private void OnIncomeFilterClicked(object? sender, EventArgs e)
    {
        if (BindingContext is TransactionsViewModel viewModel)
        {
            // Set type filter to "Income" (index 1) and apply filters
            var typePicker = this.FindByName<Picker>("TypePicker");
            if (typePicker != null)
            {
                typePicker.SelectedIndex = 1;
            }
            
            var dateRangePicker = this.FindByName<Picker>("DateRangePicker");
            var categoryPicker = this.FindByName<Picker>("CategoryPicker");
            var sortPicker = this.FindByName<Picker>("SortPicker");

            viewModel.ApplyFilters(
                dateRangePicker?.SelectedIndex ?? 0,
                categoryPicker?.SelectedItem as BudgetTracker.Infrastructure.Models.Category,
                1, // Income only
                sortPicker?.SelectedIndex ?? 0
            );
        }
    }

    private void OnExpenseFilterClicked(object? sender, EventArgs e)
    {
        if (BindingContext is TransactionsViewModel viewModel)
        {
            // Set type filter to "Expense" (index 2) and apply filters
            var typePicker = this.FindByName<Picker>("TypePicker");
            if (typePicker != null)
            {
                typePicker.SelectedIndex = 2;
            }
            
            var dateRangePicker = this.FindByName<Picker>("DateRangePicker");
            var categoryPicker = this.FindByName<Picker>("CategoryPicker");
            var sortPicker = this.FindByName<Picker>("SortPicker");

            viewModel.ApplyFilters(
                dateRangePicker?.SelectedIndex ?? 0,
                categoryPicker?.SelectedItem as BudgetTracker.Infrastructure.Models.Category,
                2, // Expense only
                sortPicker?.SelectedIndex ?? 0
            );
        }
    }
}
