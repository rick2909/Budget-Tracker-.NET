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

            transaction.Property(t => t.UpdatedAt) .IsRequired();
            
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

            recurring.Property(r => r.StartDate) .IsRequired();

            recurring.Property(r => r.Pattern)
                .IsRequired()
                .HasMaxLength(20);

            recurring.Property(r => r.Type)
                .IsRequired()
                .HasMaxLength(20);

            recurring.Property(r => r.CreatedAt).IsRequired();

            recurring.Property(r => r.UpdatedAt) .IsRequired();

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
            category.HasData(
                new Category { Id = 1, Name = "Groceries", Description = "Food and supermarket purchases" },
                new Category { Id = 2, Name = "Utilities", Description = "Electricity, water, gas, etc." },
                new Category { Id = 3, Name = "Transport", Description = "Public transport, fuel, etc." },
                new Category { Id = 4, Name = "Leisure", Description = "Entertainment and hobbies" },
                new Category { Id = 5, Name = "Other", Description = "Miscellaneous expenses" },
                new Category { Id = 6, Name = "Travel", Description = "Trips, vacations, hotels, flights" },
                new Category { Id = 7, Name = "Health", Description = "Medical, pharmacy, insurance" },
                new Category { Id = 8, Name = "Services", Description = "Professional and personal services" },
                new Category { Id = 9, Name = "Insurance", Description = "Insurance premiums and payments" },
                new Category { Id = 10, Name = "Assets", Description = "Crypto, shares, investments" },
                new Category { Id = 11, Name = "Loan", Description = "Loan payments and repayments" },
                new Category { Id = 12, Name = "Donation", Description = "Charity and donations" },
                new Category { Id = 13, Name = "Salary", Description = "Income from employment" },
                new Category { Id = 14, Name = "Gifts", Description = "Received or given gifts" },
                new Category { Id = 15, Name = "Interest", Description = "Interest income or expenses" }
            );
        });
    }
}