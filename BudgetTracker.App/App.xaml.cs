using BudgetTracker.App.Views;

#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
using WinRT.Interop;
#endif

namespace BudgetTracker.App;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var window = new Window(new AppShell());
        
#if WINDOWS
        window.Created += (s, e) =>
        {
            if (window.Handler?.PlatformView is Microsoft.UI.Xaml.Window nativeWindow)
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
                    titleBar.BackgroundColor = ColorHelper.FromArgb(255, 27, 26, 25);
                    titleBar.ForegroundColor = Microsoft.UI.Colors.White;

                    // Set custom draggable region
                    // var titleBarGrid = dashboardView.FindByName<Grid>("TitleBarGrid");
                    // if (titleBarGrid?.Handler?.PlatformView is Microsoft.UI.Xaml.FrameworkElement fe)
                    // {
                    //     nativeWindow.SetTitleBar(fe);
                    // }
                }
            }
        };
#endif
        
        AppDomain.CurrentDomain.UnhandledException += (s, e) =>
        {
            System.Diagnostics.Debug.WriteLine($"Unhandled: {e.ExceptionObject}");
        };
        
        return window;
    }
}