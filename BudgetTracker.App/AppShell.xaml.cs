using BudgetTracker.App.Views;

#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
using WinRT.Interop;
#endif

namespace BudgetTracker.App;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(SettingsView), typeof(SettingsView));
        Routing.RegisterRoute(nameof(TransactionsView), typeof(TransactionsView));
    }
    
    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
        ConfigureTitleBar();
    }
    
    private void ConfigureTitleBar()
    {
#if WINDOWS
        try
        {
            if (Handler?.MauiContext?.Services?.GetService<IWindow>() is Window window &&
                window.Handler?.PlatformView is Microsoft.UI.Xaml.Window nativeWindow)
            {
                var windowHandle = WindowNative.GetWindowHandle(nativeWindow);
                var windowId = Win32Interop.GetWindowIdFromWindow(windowHandle);
                var appWindow = AppWindow.GetFromWindowId(windowId);

                if (appWindow?.TitleBar is not null)
                {
                    var titleBar = appWindow.TitleBar;
                    titleBar.ExtendsContentIntoTitleBar = true;
                    titleBar.ButtonBackgroundColor = Microsoft.UI.Colors.Transparent;
                    titleBar.ButtonForegroundColor = Microsoft.UI.Colors.White;
                    titleBar.ButtonHoverBackgroundColor = ColorHelper.FromArgb(20, 255, 255, 255);
                    titleBar.ButtonPressedBackgroundColor = ColorHelper.FromArgb(30, 255, 255, 255);
                    titleBar.BackgroundColor = ColorHelper.FromArgb(255, 27, 26, 25); // #1B1A19
                    titleBar.ForegroundColor = Microsoft.UI.Colors.White;

                    // Set custom draggable region
                    var dashboardView = window.Page as DashboardView;
                    var titleBarGrid = dashboardView?.FindByName<Grid>("TitleBarGrid");
                    if (titleBarGrid?.Handler?.PlatformView is Microsoft.UI.Xaml.FrameworkElement fe)
                    {
                        nativeWindow.SetTitleBar(fe);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error configuring title bar: {ex.Message}");
        }
#endif
    }
}