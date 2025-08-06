using BudgetTracker.App.Views;

#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Graphics;
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
        Routing.RegisterRoute("DashboardView", typeof(Views.DashboardView));
        Routing.RegisterRoute("AddTransactionView", typeof(Views.AddTransactionView));
    }
    
    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();

#if WINDOWS
        var mauiWindow = this.Handler?.MauiContext?.Services.GetService<Microsoft.Maui.Controls.Window>();
        if (mauiWindow != null)
        {
            mauiWindow.Title = "Budget Tracker";
            
            if (mauiWindow.Handler?.PlatformView is Microsoft.UI.Xaml.Window nativeWindow)
            {
                try
                {
                    nativeWindow.SystemBackdrop = new Microsoft.UI.Xaml.Media.MicaBackdrop();
                }
                catch
                {
                    // Mica not available, continue without it
                }

                var appWindow = GetAppWindowForCurrentWindow(nativeWindow);
                if (appWindow is not null)
                {
                    appWindow.SetIcon("Platforms/Windows/trayicon.ico");
                    var displayArea = DisplayArea.GetFromWindowId(appWindow.Id, DisplayAreaFallback.Nearest);
                    if (displayArea is not null)
                    {
                        var centeredPosition = appWindow.Position;
                        centeredPosition.X = ((displayArea.WorkArea.Width - appWindow.Size.Width) / 2);
                        centeredPosition.Y = ((displayArea.WorkArea.Height - appWindow.Size.Height) / 2);
                        appWindow.Move(centeredPosition);
                    }
                }
            }
        }
#endif
    }

#if WINDOWS
    private static AppWindow GetAppWindowForCurrentWindow(object window)
    {
        var windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(window);
        var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(windowHandle);
        return AppWindow.GetFromWindowId(windowId);
    }
#endif
    
    private void ConfigureTitleBar()
    {
#if WINDOWS
        try
        {
            if (Handler?.MauiContext?.Services?.GetService<IWindow>() is Window window &&
                window.Handler?.PlatformView is Microsoft.UI.Xaml.Window nativeWindow)
            {
                var windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(nativeWindow);
                var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(windowHandle);
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