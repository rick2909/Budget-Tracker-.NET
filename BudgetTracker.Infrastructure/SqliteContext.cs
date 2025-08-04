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
        });
    }
}