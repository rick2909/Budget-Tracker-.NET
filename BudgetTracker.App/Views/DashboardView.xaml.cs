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
    
    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
        
        // Switch layout based on screen width
        // Desktop: > 1024px, Tablet/Mobile: <= 1024px
        var desktopLayout = this.FindByName<Grid>("DesktopLayout");
        var mobileLayout = this.FindByName<Grid>("MobileLayout");
        
        if (desktopLayout != null && mobileLayout != null)
        {
            if (width > 1024)
            {
                desktopLayout.IsVisible = true;
                mobileLayout.IsVisible = false;
            }
            else
            {
                desktopLayout.IsVisible = false;
                mobileLayout.IsVisible = true;
            }
        }
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
                
            var prevButton = this.FindByName<Button>("PreviousMonthButton");
            if (prevButton != null)
                prevButton.Clicked += OnPreviousMonthClicked;
                
            var nextButton = this.FindByName<Button>("NextMonthButton");
            if (nextButton != null)
                nextButton.Clicked += OnNextMonthClicked;
                
            var settingsButton = this.FindByName<Button>("SettingsButton");
            if (settingsButton != null)
                settingsButton.Clicked += OnSettingsClicked;
                
            var accountButton = this.FindByName<Button>("AccountButton");
            if (accountButton != null)
                accountButton.Clicked += OnAccountClicked;
                
            // Wire up mobile buttons
            var prevButtonMobile = this.FindByName<Button>("PreviousMonthButtonMobile");
            if (prevButtonMobile != null)
                prevButtonMobile.Clicked += OnPreviousMonthClicked;
                
            var nextButtonMobile = this.FindByName<Button>("NextMonthButtonMobile");
            if (nextButtonMobile != null)
                nextButtonMobile.Clicked += OnNextMonthClicked;
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

        var prevButton = this.FindByName<Button>("PreviousMonthButton");
        if (prevButton != null)
            prevButton.Clicked -= OnPreviousMonthClicked;

        var nextButton = this.FindByName<Button>("NextMonthButton");
        if (nextButton != null)
            nextButton.Clicked -= OnNextMonthClicked;

        var settingsButton = this.FindByName<Button>("SettingsButton");
        if (settingsButton != null)
            settingsButton.Clicked -= OnSettingsClicked;
            
        var accountButton = this.FindByName<Button>("AccountButton");
        if (accountButton != null)
            accountButton.Clicked -= OnAccountClicked;
            
        // Unwire mobile buttons
        var prevButtonMobile = this.FindByName<Button>("PreviousMonthButtonMobile");
        if (prevButtonMobile != null)
            prevButtonMobile.Clicked -= OnPreviousMonthClicked;
            
        var nextButtonMobile = this.FindByName<Button>("NextMonthButtonMobile");
        if (nextButtonMobile != null)
            nextButtonMobile.Clicked -= OnNextMonthClicked;
    }

    // Title bar configuration is now handled at the AppShell level

    private async void OnAddTransactionClicked(object? sender, EventArgs e)
    {
        var addTransactionView = new AddTransactionView();
        await Navigation.PushModalAsync(addTransactionView);
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
        var settingsView = new SettingsView();
        await Navigation.PushAsync(settingsView);
    }
    
    private async void OnAccountClicked(object? sender, EventArgs e)
    {
        // Placeholder for account functionality
        await DisplayAlert("Account", "Account functionality coming soon!", "OK");
    }
}
