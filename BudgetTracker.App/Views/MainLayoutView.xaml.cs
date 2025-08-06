using BudgetTracker.App.Views;
using BudgetTracker.App.Components;
using BudgetTracker.App.ViewModels;

namespace BudgetTracker.App.Views;

public partial class MainLayoutView : ContentPage
{
    private NavigationHeaderComponent? _navigationHeader;
    private ContentView? _contentArea;
    private ContentPage? _currentPage; // Store reference to current page

    public MainLayoutView()
    {
        InitializeComponent();
        WireUpNavigation();
        _ = ShowDashboardAsync();
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
        WireUpNavigation();
    }

    private void WireUpNavigation()
    {
        try
        {
            _navigationHeader = this.FindByName<NavigationHeaderComponent>("StaticNavigationHeader");
            _contentArea = this.FindByName<ContentView>("ContentArea");

            if (_navigationHeader != null)
            {
                _navigationHeader.DashboardTapped += async (s, e) => await ShowDashboardAsync();
                _navigationHeader.TransactionsTapped += async (s, e) => await ShowTransactionsAsync();
                _navigationHeader.SettingsTapped += async (s, e) => await ShowSettingsAsync();
                _navigationHeader.AddExpenseTapped += async (s, e) => await ShowAddExpenseAsync();
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error wiring navigation: {ex.Message}");
        }
    }

    private async Task AnimateContentChange(View newContent)
    {
        if (_contentArea == null) return;

        if (_contentArea.Content != null)
            await _contentArea.FadeTo(0, 150);

        _contentArea.Content = newContent;
        await _contentArea.FadeTo(1, 150);
    }

    public async Task ShowDashboardAsync()
    {
        if (_navigationHeader != null)
            _navigationHeader.ActivePage = "Dashboard";
        
        // Dispose previous page
        if (_currentPage != null)
        {
            _currentPage.Disappearing -= OnCurrentPageDisappearing;
        }
        
        var dashboardView = new DashboardView();
        _currentPage = dashboardView;
        
        // Wire up page lifecycle
        _currentPage.Appearing += OnCurrentPageAppearing;
        _currentPage.Disappearing += OnCurrentPageDisappearing;
        
        // Extract the content and preserve the binding context
        var content = dashboardView.Content;
        content.BindingContext = dashboardView.BindingContext;
        await AnimateContentChange(content);
        
        // Manually wire up month navigation buttons after content is set
        WireUpMonthNavigation(content, dashboardView);
    }

    private void WireUpMonthNavigation(View content, DashboardView dashboardView)
    {
        try
        {
            System.Diagnostics.Debug.WriteLine("Starting to wire up month navigation buttons...");
            
            // Find month navigation buttons in the extracted content
            var prevButton = content.FindByName<Button>("PreviousMonthButton");
            if (prevButton != null)
            {
                System.Diagnostics.Debug.WriteLine("Found PreviousMonthButton, wiring up event...");
                prevButton.Clicked += (s, e) => OnPreviousMonthClicked(dashboardView);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("PreviousMonthButton not found!");
            }
            
            var nextButton = content.FindByName<Button>("NextMonthButton");
            if (nextButton != null)
            {
                System.Diagnostics.Debug.WriteLine("Found NextMonthButton, wiring up event...");
                nextButton.Clicked += (s, e) => OnNextMonthClicked(dashboardView);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("NextMonthButton not found!");
            }
            
            // Wire up mobile buttons
            var prevButtonMobile = content.FindByName<Button>("PreviousMonthButtonMobile");
            if (prevButtonMobile != null)
            {
                System.Diagnostics.Debug.WriteLine("Found PreviousMonthButtonMobile, wiring up event...");
                prevButtonMobile.Clicked += (s, e) => OnPreviousMonthClicked(dashboardView);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("PreviousMonthButtonMobile not found!");
            }
            
            var nextButtonMobile = content.FindByName<Button>("NextMonthButtonMobile");
            if (nextButtonMobile != null)
            {
                System.Diagnostics.Debug.WriteLine("Found NextMonthButtonMobile, wiring up event...");
                nextButtonMobile.Clicked += (s, e) => OnNextMonthClicked(dashboardView);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("NextMonthButtonMobile not found!");
            }
            
            System.Diagnostics.Debug.WriteLine("Finished wiring up month navigation buttons.");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error wiring month navigation: {ex.Message}");
        }
    }

    private void OnPreviousMonthClicked(DashboardView dashboardView)
    {
        System.Diagnostics.Debug.WriteLine("Previous month button clicked!");
        if (dashboardView.BindingContext is DashboardViewModel viewModel)
        {
            System.Diagnostics.Debug.WriteLine("Calling NavigateToPreviousMonth...");
            viewModel.NavigateToPreviousMonth();
        }
        else
        {
            System.Diagnostics.Debug.WriteLine("BindingContext is not DashboardViewModel!");
        }
    }

    private void OnNextMonthClicked(DashboardView dashboardView)
    {
        System.Diagnostics.Debug.WriteLine("Next month button clicked!");
        if (dashboardView.BindingContext is DashboardViewModel viewModel)
        {
            System.Diagnostics.Debug.WriteLine("Calling NavigateToNextMonth...");
            viewModel.NavigateToNextMonth();
        }
        else
        {
            System.Diagnostics.Debug.WriteLine("BindingContext is not DashboardViewModel!");
        }
    }

    public async Task ShowTransactionsAsync()
    {
        if (_navigationHeader != null)
            _navigationHeader.ActivePage = "Transactions";
        
        // Dispose previous page
        if (_currentPage != null)
        {
            _currentPage.Disappearing -= OnCurrentPageDisappearing;
        }
        
        var transactionsView = new TransactionsView();
        _currentPage = transactionsView;
        
        // Wire up page lifecycle
        _currentPage.Appearing += OnCurrentPageAppearing;
        _currentPage.Disappearing += OnCurrentPageDisappearing;
        
        // Extract the content and preserve the binding context
        var content = transactionsView.Content;
        content.BindingContext = transactionsView.BindingContext;
        await AnimateContentChange(content);
        
        // Manually trigger OnAppearing to wire up events
        transactionsView.SendAppearing();
    }

    public async Task ShowSettingsAsync()
    {
        if (_navigationHeader != null)
            _navigationHeader.ActivePage = "Settings";
        
        // Dispose previous page
        if (_currentPage != null)
        {
            _currentPage.Disappearing -= OnCurrentPageDisappearing;
        }
        
        var settingsView = new SettingsView();
        _currentPage = settingsView;
        
        // Wire up page lifecycle
        _currentPage.Appearing += OnCurrentPageAppearing;
        _currentPage.Disappearing += OnCurrentPageDisappearing;
        
        // Extract the content and preserve the binding context
        var content = settingsView.Content;
        content.BindingContext = settingsView.BindingContext;
        await AnimateContentChange(content);
        
        // Manually trigger OnAppearing to wire up events
        settingsView.SendAppearing();
    }

    public async Task ShowAddExpenseAsync()
    {
        await Navigation.PushModalAsync(new AddTransactionView());
    }
    
    private void OnCurrentPageAppearing(object? sender, EventArgs e)
    {
        // Handle page appearing if needed
    }
    
    private void OnCurrentPageDisappearing(object? sender, EventArgs e)
    {
        // Handle page disappearing if needed
    }
}
