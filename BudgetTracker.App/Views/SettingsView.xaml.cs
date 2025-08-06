using BudgetTracker.App.ViewModels;

namespace BudgetTracker.App.Views;

public partial class SettingsView : ContentPage
{
    public SettingsView()
    {
        InitializeComponent();
        BindingContext = new SettingsViewModel();
    }
}
