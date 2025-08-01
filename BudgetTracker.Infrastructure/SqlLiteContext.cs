using BudgetTracker.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace BudgetTracker.Infrastructure;

public class SqlLiteContext : DbContext
{
    // TODO: Replace with your actual entity classes
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Category> Categories { get; set; }

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
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transaction>(transaction =>
        {
            transaction.HasKey(t => t.Id);
            transaction.Property(t => t.Title).IsRequired().HasMaxLength(100);
            transaction.Property(t => t.Amount).IsRequired().HasColumnType("decimal(18,2)");
            transaction.Property(t => t.Date).IsRequired();
            transaction.Property(t => t.Type).IsRequired().HasMaxLength(20);
            
            // Define foreign key relationship with Category
            transaction.HasOne(t => t.Category)
                .WithMany()
                .HasForeignKey(t => t.CategoryId);
        });

        modelBuilder.Entity<Category>(category =>
        {
            category.HasKey(c => c.Id);
            category.Property(c => c.Name).IsRequired().HasMaxLength(50);
            category.Property(c => c.Description).HasMaxLength(200);
        });
    }
}