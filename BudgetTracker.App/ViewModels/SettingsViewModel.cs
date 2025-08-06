using System.Windows.Input;

namespace BudgetTracker.App.ViewModels;

public class SettingsViewModel : BaseViewModel
{
    private bool _isDarkTheme = true;
    private bool _enableNotifications = true;
    private string _currency = "USD";
    private string _appVersion = "1.0.0";

    public SettingsViewModel()
    {
        SaveCommand = new Command(OnSave);
        ResetCommand = new Command(OnReset);
    }

    public bool IsDarkTheme
    {
        get => _isDarkTheme;
        set => SetProperty(ref _isDarkTheme, value);
    }

    public bool EnableNotifications
    {
        get => _enableNotifications;
        set => SetProperty(ref _enableNotifications, value);
    }

    public string Currency
    {
        get => _currency;
        set => SetProperty(ref _currency, value);
    }

    public string AppVersion
    {
        get => _appVersion;
        set => SetProperty(ref _appVersion, value);
    }

    public ICommand SaveCommand { get; }
    public ICommand ResetCommand { get; }

    private async void OnSave()
    {
        // TODO: Save settings to preferences
        if (Shell.Current != null)
        {
            await Shell.Current.DisplayAlert(
                "Settings", 
                "Settings saved successfully!", 
                "OK");
        }
    }

    private async void OnReset()
    {
        if (Shell.Current != null)
        {
            var result = await Shell.Current.DisplayAlert(
                "Reset Settings", 
                "Are you sure you want to reset all settings to default?", 
                "Reset", 
                "Cancel");

            if (result)
            {
                IsDarkTheme = true;
                EnableNotifications = true;
                Currency = "USD";
            }
        }
    }
}
