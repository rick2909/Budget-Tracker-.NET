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
        ConfigureTitleBar();
    }
    
    protected override void OnAppearing()
    {
        base.OnAppearing();
        
        // Wire up events after the page appears and controls are available
        if (AddTransactionButton != null)
            AddTransactionButton.Clicked += OnAddTransactionClicked;
        if (ViewAllTransactionsButton != null)
            ViewAllTransactionsButton.Clicked += OnViewAllTransactionsClicked;
        if (PreviousMonthButton != null)
            PreviousMonthButton.Clicked += OnPreviousMonthClicked;
        if (NextMonthButton != null)
            NextMonthButton.Clicked += OnNextMonthClicked;
        if (SettingsButton != null)
            SettingsButton.Clicked += OnSettingsClicked;
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
        ConfigureTitleBar();
    }

    private void ConfigureTitleBar()
    {
#if WINDOWS
        if (Handler?.PlatformView is Microsoft.UI.Xaml.Window window)
        {
            var windowHandle = WindowNative.GetWindowHandle(window);
            var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(windowHandle);
            var appWindow = AppWindow.GetFromWindowId(windowId);
            
            if (appWindow?.TitleBar is not null)
            {
                var titleBar = appWindow.TitleBar;
                titleBar.ExtendsContentIntoTitleBar = true;
                titleBar.ButtonBackgroundColor = Microsoft.UI.Colors.Transparent;
                titleBar.ButtonForegroundColor = Microsoft.UI.Colors.White;
                titleBar.ButtonHoverBackgroundColor = Microsoft.UI.ColorHelper.FromArgb(30, 255, 255, 255);
                titleBar.ButtonPressedBackgroundColor = Microsoft.UI.ColorHelper.FromArgb(50, 255, 255, 255);
                
                // Set the title bar as non-client area
                window.ExtendsContentIntoTitleBar = true;
            }
        }
#endif
    }

    private async void OnAddTransactionClicked(object? sender, EventArgs e)
    {
        var addTransactionView = new AddTransactionView();
        await Navigation.PushModalAsync(addTransactionView);
    }

    private async void OnViewAllTransactionsClicked(object? sender, EventArgs e)
    {
        var transactionsView = new TransactionsView();
        await Navigation.PushAsync(transactionsView);
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
}
