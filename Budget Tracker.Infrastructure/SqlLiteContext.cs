using Budget_Tracker.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Budget_Tracker.Infrastructure;

public class SqlLiteContext : DbContext
{
    // TODO: Replace with your actual entity classes
    public DbSet<Transaction> Transactions { get; set; }
    // public DbSet<Category> Categories { get; set; }

    public SqlLiteContext(DbContextOptions<SqlLiteContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // Default SQLite connection string (update as needed)
            optionsBuilder.UseSqlite("Data Source=budget_tracker.db");
        }
    }
}

// NOTE: Install these NuGet packages in your project:
// - Microsoft.EntityFrameworkCore
// - Microsoft.EntityFrameworkCore.Sqlite
//
// And define your entity classes (e.g., Transaction, Category) in the appropriate folder.