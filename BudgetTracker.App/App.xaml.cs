using BudgetTracker.App.Views;

#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
using WinRT.Interop;
using Windows.Graphics;
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
        var window = new Window(new DashboardView());
        
#if WINDOWS
        // Configure native Windows styling
        if (window.Handler?.PlatformView is Microsoft.UI.Xaml.Window nativeWindow)
        {
            var windowHandle = WindowNative.GetWindowHandle(nativeWindow);
            var windowId = Win32Interop.GetWindowIdFromWindow(windowHandle);
            var appWindow = AppWindow.GetFromWindowId(windowId);
            
            if (appWindow is not null)
            {
                // Set window size
                appWindow.Resize(new SizeInt32(1200, 800));
                
                // Configure title bar
                var titleBar = appWindow.TitleBar;
                titleBar.ExtendsContentIntoTitleBar = true;
                titleBar.ButtonBackgroundColor = Microsoft.UI.Colors.Transparent;
                titleBar.ButtonForegroundColor = Microsoft.UI.Colors.White;
                titleBar.ButtonHoverBackgroundColor = Microsoft.UI.ColorHelper.FromArgb(20, 255, 255, 255);
                titleBar.ButtonPressedBackgroundColor = Microsoft.UI.ColorHelper.FromArgb(30, 255, 255, 255);
            }
        }
#endif
        
        return window;
    }
}