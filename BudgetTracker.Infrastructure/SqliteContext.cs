using BudgetTracker.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace BudgetTracker.Infrastructure;

public class SqliteContext(DbContextOptions<SqliteContext> options) : DbContext(options)
{
    // TODO: Replace with your actual entity classes
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<RecurringTransaction> RecurringTransactions { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // Default SQLite connection string (update as needed)
            optionsBuilder.UseSqlite("Data Source=budget_tracker.db");
        }
    }
    
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is Transaction || e.Entity is RecurringTransaction)
            .ToList();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property("CreatedAt").CurrentValue = DateTime.UtcNow;
                entry.Property("UpdatedAt").CurrentValue = DateTime.UtcNow;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Property("UpdatedAt").CurrentValue = DateTime.UtcNow;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transaction>(transaction =>
        {
            transaction.HasKey(t => t.Id);
            
            transaction.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(100);
            
            transaction.Property(t => t.Amount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
            
            transaction.Property(t => t.Date).IsRequired();
            
            transaction.Property(t => t.Type)
                .IsRequired()
                .HasMaxLength(20);
            
            transaction.Property(t => t.CreatedAt).IsRequired();

            transaction.Property(t => t.UpdatedAt).IsRequired();
            
            // Define foreign key relationship with Category
            transaction.HasOne(t => t.Category)
                .WithMany()
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            transaction.HasOne(t => t.RecurringTransaction)
                .WithMany(r => r.GeneratedTransactions)
                .HasForeignKey(t => t.RecurringTransactionId)
                .OnDelete(DeleteBehavior.SetNull);
        });
        
        modelBuilder.Entity<RecurringTransaction>(recurring =>
        {
            recurring.HasKey(r => r.Id);

            recurring.Property(r => r.Title)
                .IsRequired()
                .HasMaxLength(100);

            recurring.Property(r => r.Amount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            recurring.Property(r => r.StartDate).IsRequired();

            recurring.Property(r => r.Pattern)
                .IsRequired()
                .HasMaxLength(20);

            recurring.Property(r => r.Type)
                .IsRequired()
                .HasMaxLength(20);

            recurring.Property(r => r.CreatedAt).IsRequired();

            recurring.Property(r => r.UpdatedAt).IsRequired();

            recurring.HasOne(r => r.Category)
                .WithMany()
                .HasForeignKey(r => r.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Category>(category =>
        {
            category.HasKey(c => c.Id);
            category.Property(c => c.Name).IsRequired().HasMaxLength(50);
            category.Property(c => c.Description).HasMaxLength(200);
            category.Property(c => c.Icon).IsRequired().HasMaxLength(50);

            // Seed Categories with Material Icons
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Groceries", Description = "Food, drinks, and supermarket purchases", Icon = "shopping_cart" },
                new Category { Id = 2, Name = "Dining Out", Description = "Restaurants, cafes, takeaways, bars", Icon = "restaurant" },
                new Category { Id = 3, Name = "Utilities", Description = "Electricity, water, gas, internet, phone", Icon = "flash_on" },
                new Category { Id = 4, Name = "Housing", Description = "Rent, mortgage, property maintenance", Icon = "home" },
                new Category { Id = 5, Name = "Transport", Description = "Public transport, fuel, tolls", Icon = "commute" },
                new Category { Id = 6, Name = "Vehicle", Description = "Car payments, repairs, insurance", Icon = "directions_car" },
                new Category { Id = 7, Name = "Entertainment", Description = "Movies, hobbies, games, streaming services", Icon = "movie" },
                new Category { Id = 8, Name = "Travel", Description = "Trips, vacations, hotels, flights", Icon = "flight" },
                new Category { Id = 9, Name = "Health", Description = "Medical, dental, pharmacy, health insurance", Icon = "local_hospital" },
                new Category { Id = 10, Name = "Personal Care", Description = "Haircuts, beauty, grooming", Icon = "spa" },
                new Category { Id = 11, Name = "Education", Description = "Courses, books, learning materials", Icon = "school" },
                new Category { Id = 12, Name = "Services", Description = "Professional, repair, or cleaning services", Icon = "build" },
                new Category { Id = 13, Name = "Insurance", Description = "Premiums for health, life, property", Icon = "security" },
                new Category { Id = 14, Name = "Investments", Description = "Crypto, shares, bonds, assets", Icon = "trending_up" },
                new Category { Id = 15, Name = "Loan", Description = "Loan payments and repayments", Icon = "account_balance" },
                new Category { Id = 16, Name = "Donations", Description = "Charity and contributions", Icon = "volunteer_activism" },
                new Category { Id = 17, Name = "Salary", Description = "Primary income from employment", Icon = "attach_money" },
                new Category { Id = 18, Name = "Business", Description = "Income from business or freelance work", Icon = "business_center" },
                new Category { Id = 19, Name = "Gifts", Description = "Received or given gifts", Icon = "card_giftcard" },
                new Category { Id = 20, Name = "Interest", Description = "Interest income or loan interest paid", Icon = "trending_up" },
                new Category { Id = 21, Name = "Taxes", Description = "Income tax, property tax, other taxes", Icon = "receipt_long" },
                new Category { Id = 22, Name = "Savings", Description = "Transfers to savings or emergency funds", Icon = "savings" },
                new Category { Id = 23, Name = "Other", Description = "Miscellaneous expenses", Icon = "category" }
            );
        });
    }
}