using BudgetTracker.App.Views;

namespace BudgetTracker.App.Components;

public partial class RecentTransactionsComponent : ContentView
{
    public RecentTransactionsComponent()
    {
        InitializeComponent();
    }
    
    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
        
        // Wire up the View All button event
        var viewAllButton = this.FindByName<Button>("ViewAllTransactionsButton");
        if (viewAllButton != null)
        {
            viewAllButton.Clicked += OnViewAllTransactionsClicked;
        }
    }
    
    private async void OnViewAllTransactionsClicked(object? sender, EventArgs e)
    {
        // Navigate to transactions view
        var transactionsView = new TransactionsView();
        
        // Use the Window property instead of obsolete MainPage
        var window = this.Window;
        if (window?.Page != null)
        {
            await window.Page.Navigation.PushAsync(transactionsView);
        }
    }
}
