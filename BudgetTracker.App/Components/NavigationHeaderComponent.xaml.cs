using Microsoft.Maui.Controls;

namespace BudgetTracker.App.Components;

public partial class NavigationHeaderComponent : ContentView
{
    // Bindable properties for active page styling
    public static readonly BindableProperty ActivePageProperty = 
        BindableProperty.Create(
            nameof(ActivePage), 
            typeof(string), 
            typeof(NavigationHeaderComponent), 
            "Dashboard",
            propertyChanged: OnActivePageChanged);

    public string ActivePage
    {
        get => (string)GetValue(ActivePageProperty);
        set => SetValue(ActivePageProperty, value);
    }

    // Color properties for Dashboard
    public static readonly BindableProperty DashboardBackgroundColorProperty = 
        BindableProperty.Create(nameof(DashboardBackgroundColor), typeof(Color), typeof(NavigationHeaderComponent), Colors.Transparent);
    
    public Color DashboardBackgroundColor
    {
        get => (Color)GetValue(DashboardBackgroundColorProperty);
        set => SetValue(DashboardBackgroundColorProperty, value);
    }

    public static readonly BindableProperty DashboardTextColorProperty = 
        BindableProperty.Create(nameof(DashboardTextColor), typeof(Color), typeof(NavigationHeaderComponent), Colors.Gray);
    
    public Color DashboardTextColor
    {
        get => (Color)GetValue(DashboardTextColorProperty);
        set => SetValue(DashboardTextColorProperty, value);
    }

    public static readonly BindableProperty DashboardFontAttributesProperty = 
        BindableProperty.Create(nameof(DashboardFontAttributes), typeof(FontAttributes), typeof(NavigationHeaderComponent), FontAttributes.None);
    
    public FontAttributes DashboardFontAttributes
    {
        get => (FontAttributes)GetValue(DashboardFontAttributesProperty);
        set => SetValue(DashboardFontAttributesProperty, value);
    }

    // Color properties for Transactions
    public static readonly BindableProperty TransactionsBackgroundColorProperty = 
        BindableProperty.Create(nameof(TransactionsBackgroundColor), typeof(Color), typeof(NavigationHeaderComponent), Colors.Transparent);
    
    public Color TransactionsBackgroundColor
    {
        get => (Color)GetValue(TransactionsBackgroundColorProperty);
        set => SetValue(TransactionsBackgroundColorProperty, value);
    }

    public static readonly BindableProperty TransactionsTextColorProperty = 
        BindableProperty.Create(nameof(TransactionsTextColor), typeof(Color), typeof(NavigationHeaderComponent), Colors.Gray);
    
    public Color TransactionsTextColor
    {
        get => (Color)GetValue(TransactionsTextColorProperty);
        set => SetValue(TransactionsTextColorProperty, value);
    }

    public static readonly BindableProperty TransactionsFontAttributesProperty = 
        BindableProperty.Create(nameof(TransactionsFontAttributes), typeof(FontAttributes), typeof(NavigationHeaderComponent), FontAttributes.None);
    
    public FontAttributes TransactionsFontAttributes
    {
        get => (FontAttributes)GetValue(TransactionsFontAttributesProperty);
        set => SetValue(TransactionsFontAttributesProperty, value);
    }

    // Color properties for Settings
    public static readonly BindableProperty SettingsBackgroundColorProperty = 
        BindableProperty.Create(nameof(SettingsBackgroundColor), typeof(Color), typeof(NavigationHeaderComponent), Colors.Transparent);
    
    public Color SettingsBackgroundColor
    {
        get => (Color)GetValue(SettingsBackgroundColorProperty);
        set => SetValue(SettingsBackgroundColorProperty, value);
    }

    public static readonly BindableProperty SettingsTextColorProperty = 
        BindableProperty.Create(nameof(SettingsTextColor), typeof(Color), typeof(NavigationHeaderComponent), Colors.Gray);
    
    public Color SettingsTextColor
    {
        get => (Color)GetValue(SettingsTextColorProperty);
        set => SetValue(SettingsTextColorProperty, value);
    }

    public static readonly BindableProperty SettingsFontAttributesProperty = 
        BindableProperty.Create(nameof(SettingsFontAttributes), typeof(FontAttributes), typeof(NavigationHeaderComponent), FontAttributes.None);
    
    public FontAttributes SettingsFontAttributes
    {
        get => (FontAttributes)GetValue(SettingsFontAttributesProperty);
        set => SetValue(SettingsFontAttributesProperty, value);
    }

    // Events
    public event EventHandler? DashboardTapped;
    public event EventHandler? TransactionsTapped;
    public event EventHandler? SettingsTapped;
    public event EventHandler? AddExpenseTapped;

    public NavigationHeaderComponent()
    {
        InitializeComponent();
        UpdateActivePageStyling();
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
        WireUpEvents();
    }

    private void WireUpEvents()
    {
        try
        {
            var dashboardNavTap = this.FindByName<TapGestureRecognizer>("DashboardNavTap");
            if (dashboardNavTap != null)
                dashboardNavTap.Tapped += OnDashboardTapped;

            var transactionsNavTap = this.FindByName<TapGestureRecognizer>("TransactionsNavTap");
            if (transactionsNavTap != null)
                transactionsNavTap.Tapped += OnTransactionsTapped;

            var settingsNavTap = this.FindByName<TapGestureRecognizer>("SettingsNavTap");
            if (settingsNavTap != null)
                settingsNavTap.Tapped += OnSettingsTapped;

            var addExpenseButton = this.FindByName<Button>("AddExpenseButton");
            if (addExpenseButton != null)
                addExpenseButton.Clicked += OnAddExpenseTapped;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error wiring navigation events: {ex.Message}");
        }
    }

    private static void OnActivePageChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is NavigationHeaderComponent component)
        {
            component.UpdateActivePageStyling();
        }
    }

    private void UpdateActivePageStyling()
    {
        var activeColor = Color.FromArgb("#0F6CBD");
        var inactiveColor = Color.FromArgb("#A19F9D");
        var transparentColor = Colors.Transparent;

        // Reset all to inactive
        DashboardBackgroundColor = transparentColor;
        DashboardTextColor = inactiveColor;
        DashboardFontAttributes = FontAttributes.None;

        TransactionsBackgroundColor = transparentColor;
        TransactionsTextColor = inactiveColor;
        TransactionsFontAttributes = FontAttributes.None;

        SettingsBackgroundColor = transparentColor;
        SettingsTextColor = inactiveColor;
        SettingsFontAttributes = FontAttributes.None;

        // Set active page styling
        switch (ActivePage?.ToLower())
        {
            case "dashboard":
                DashboardBackgroundColor = activeColor;
                DashboardTextColor = Colors.White;
                DashboardFontAttributes = FontAttributes.Bold;
                break;
            case "transactions":
                TransactionsBackgroundColor = activeColor;
                TransactionsTextColor = Colors.White;
                TransactionsFontAttributes = FontAttributes.Bold;
                break;
            case "settings":
                SettingsBackgroundColor = activeColor;
                SettingsTextColor = Colors.White;
                SettingsFontAttributes = FontAttributes.Bold;
                break;
        }
    }

    private void OnDashboardTapped(object? sender, EventArgs e)
    {
        DashboardTapped?.Invoke(this, EventArgs.Empty);
    }

    private void OnTransactionsTapped(object? sender, EventArgs e)
    {
        TransactionsTapped?.Invoke(this, EventArgs.Empty);
    }

    private void OnSettingsTapped(object? sender, EventArgs e)
    {
        SettingsTapped?.Invoke(this, EventArgs.Empty);
    }

    private void OnAddExpenseTapped(object? sender, EventArgs e)
    {
        AddExpenseTapped?.Invoke(this, EventArgs.Empty);
    }
}
