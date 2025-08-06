using BudgetTracker.App.ViewModels;

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
        
        // Wire up events safely
        try
        {
            var addButton = this.FindByName<Button>("AddTransactionButton");
            if (addButton != null)
                addButton.Clicked += OnAddTransactionClicked;
                
            var dateRangePicker = this.FindByName<Picker>("DateRangePicker");
            if (dateRangePicker != null)
            {
                dateRangePicker.SelectedIndexChanged += OnFilterChanged;
                dateRangePicker.SelectedIndex = 0; // This Month
            }
            
            var categoryPicker = this.FindByName<Picker>("CategoryPicker");
            if (categoryPicker != null)
                categoryPicker.SelectedIndexChanged += OnFilterChanged;
                
            var typePicker = this.FindByName<Picker>("TypePicker");
            if (typePicker != null)
            {
                typePicker.SelectedIndexChanged += OnFilterChanged;
                typePicker.SelectedIndex = 0; // All
            }
            
            var sortPicker = this.FindByName<Picker>("SortPicker");
            if (sortPicker != null)
            {
                sortPicker.SelectedIndexChanged += OnFilterChanged;
                sortPicker.SelectedIndex = 0; // Date (Newest)
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error wiring TransactionsView events: {ex.Message}");
        }
    }
    
    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        var addButton = this.FindByName<Button>("AddTransactionButton");
        if (addButton != null)
            addButton.Clicked -= OnAddTransactionClicked;
        
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
            viewModel.ApplyFilters(
                DateRangePicker.SelectedIndex,
                CategoryPicker.SelectedItem as BudgetTracker.Infrastructure.Models.Category,
                TypePicker.SelectedIndex,
                SortPicker.SelectedIndex
            );
        }
    }
}
