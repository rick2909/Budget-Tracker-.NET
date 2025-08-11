namespace BudgetTracker.Logic.Validators;

public static class MaterialIconValidator
{
    // Common Material Icons used in budget/finance apps
    private static readonly HashSet<string> ValidMaterialIcons = new()
    {
        // Financial
        "attach_money", "money_off", "account_balance", "account_balance_wallet",
        "credit_card", "payment", "savings", "trending_up", "trending_down",
        "pie_chart", "bar_chart", "show_chart", "analytics",
        
        // Categories
        "shopping_cart", "restaurant", "local_dining", "fastfood",
        "directions_car", "local_gas_station", "commute", "directions_bus",
        "home", "apartment", "business", "store",
        "flash_on", "power", "electrical_services",
        "movie", "theaters", "sports_esports", "music_note",
        "local_hospital", "fitness_center", "spa", "medical_services",
        "school", "library_books", "menu_book", "auto_stories",
        "flight", "hotel", "luggage", "beach_access",
        "shopping_bag", "local_mall", "storefront",
        "pets", "child_care", "elderly",
        "build", "handyman", "plumbing", "electrical_services",
        "security", "gavel", "description",
        "card_giftcard", "volunteer_activism", "favorite",
        "receipt_long", "assignment", "folder",
        
        // General
        "category", "label", "bookmark", "star", "flag",
        "work", "business_center", "corporate_fare",
        "more_horiz", "more_vert", "settings", "help",
        "info", "error", "warning", "check_circle",
        "add", "remove", "edit", "delete", "search",
        "visibility", "visibility_off", "lock", "lock_open"
    };

    public static bool IsValidMaterialIcon(string? iconName)
    {
        if (string.IsNullOrWhiteSpace(iconName))
            return false;
            
        return ValidMaterialIcons.Contains(iconName.ToLower());
    }

    public static IEnumerable<string> GetAllValidIcons()
    {
        return ValidMaterialIcons.OrderBy(x => x);
    }

    public static IEnumerable<string> GetFinancialIcons()
    {
        var financialIcons = new[]
        {
            "attach_money", "account_balance", "account_balance_wallet",
            "credit_card", "payment", "savings", "trending_up", "trending_down"
        };
        return financialIcons;
    }

    public static IEnumerable<string> GetCategoryIcons()
    {
        var categoryIcons = new[]
        {
            "shopping_cart", "restaurant", "directions_car", "home",
            "flash_on", "movie", "local_hospital", "school",
            "flight", "shopping_bag", "pets", "build",
            "security", "card_giftcard", "receipt_long"
        };
        return categoryIcons;
    }

    public static string GetSuggestedIcon(string categoryName)
    {
        return categoryName.ToLower() switch
        {
            var name when name.Contains("food") || name.Contains("groceries") => "shopping_cart",
            var name when name.Contains("dining") || name.Contains("restaurant") => "restaurant",
            var name when name.Contains("transport") || name.Contains("car") || name.Contains("gas") => "directions_car",
            var name when name.Contains("home") || name.Contains("rent") || name.Contains("mortgage") => "home",
            var name when name.Contains("utility") || name.Contains("bill") || name.Contains("electric") => "flash_on",
            var name when name.Contains("entertainment") || name.Contains("movie") => "movie",
            var name when name.Contains("health") || name.Contains("medical") || name.Contains("fitness") => "local_hospital",
            var name when name.Contains("education") || name.Contains("book") || name.Contains("school") => "school",
            var name when name.Contains("travel") || name.Contains("vacation") || name.Contains("flight") => "flight",
            var name when name.Contains("shopping") || name.Contains("clothing") || name.Contains("retail") => "shopping_bag",
            var name when name.Contains("salary") || name.Contains("income") || name.Contains("wage") => "attach_money",
            var name when name.Contains("investment") || name.Contains("stock") => "trending_up",
            var name when name.Contains("insurance") => "security",
            var name when name.Contains("loan") || name.Contains("debt") => "account_balance",
            var name when name.Contains("gift") || name.Contains("donation") => "card_giftcard",
            var name when name.Contains("tax") => "receipt_long",
            var name when name.Contains("saving") => "savings",
            _ => "help"
        };
    }
}
