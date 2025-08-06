using BudgetTracker.App.ViewModels;
using Microsoft.Maui.Controls;

#if WINDOWS
using Microsoft.UI.Xaml;
using Microsoft.UI.Windowing;
using WinRT.Interop;
using Microsoft.UI;
#endif

namespace BudgetTracker.App.Views;

public partial class DashboardView : ContentPage
{
    public DashboardView()
    {
        InitializeComponent();
        BindingContext = new DashboardViewModel();
    }
    
    protected override void OnAppearing()
    {
        base.OnAppearing();
        
        // Wire up events safely with null checks
        try
        {
            var addButton = this.FindByName<Button>("AddTransactionButton");
            if (addButton != null)
                addButton.Clicked += OnAddTransactionClicked;
                
            var viewAllButton = this.FindByName<Button>("ViewAllTransactionsButton");
            if (viewAllButton != null)
                viewAllButton.Clicked += OnViewAllTransactionsClicked;
                
            var prevButton = this.FindByName<Button>("PreviousMonthButton");
            if (prevButton != null)
                prevButton.Clicked += OnPreviousMonthClicked;
                
            var nextButton = this.FindByName<Button>("NextMonthButton");
            if (nextButton != null)
                nextButton.Clicked += OnNextMonthClicked;
                
            var settingsButton = this.FindByName<Button>("SettingsButton");
            if (settingsButton != null)
                settingsButton.Clicked += OnSettingsClicked;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error wiring events: {ex.Message}");
        }
    }
    
    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        var addButton = this.FindByName<Button>("AddTransactionButton");
        if (addButton != null)
            addButton.Clicked -= OnAddTransactionClicked;

        var viewAllButton = this.FindByName<Button>("ViewAllTransactionsButton");
        if (viewAllButton != null)
            viewAllButton.Clicked -= OnViewAllTransactionsClicked;

        var prevButton = this.FindByName<Button>("PreviousMonthButton");
        if (prevButton != null)
            prevButton.Clicked -= OnPreviousMonthClicked;

        var nextButton = this.FindByName<Button>("NextMonthButton");
        if (nextButton != null)
            nextButton.Clicked -= OnNextMonthClicked;

        var settingsButton = this.FindByName<Button>("SettingsButton");
        if (settingsButton != null)
            settingsButton.Clicked -= OnSettingsClicked;
    }

    // Title bar configuration is now handled at the AppShell level

    private async void OnAddTransactionClicked(object? sender, EventArgs e)
    {
        var addTransactionView = new AddTransactionView();
        await Navigation.PushModalAsync(addTransactionView);
    }

    private async void OnViewAllTransactionsClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(TransactionsView));
    }

    private void OnPreviousMonthClicked(object? sender, EventArgs e)
    {
        if (BindingContext is DashboardViewModel viewModel)
        {
            viewModel.NavigateToPreviousMonth();
        }
    }

    private void OnNextMonthClicked(object? sender, EventArgs e)
    {
        if (BindingContext is DashboardViewModel viewModel)
        {
            viewModel.NavigateToNextMonth();
        }
    }

    private async void OnSettingsClicked(object? sender, EventArgs e)
    {
        // var settingsView = new SettingsView();
        await Shell.Current.GoToAsync(nameof(SettingsView));
    }
}
