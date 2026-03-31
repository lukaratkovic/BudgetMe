using BudgetMe.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BudgetMe.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<TransactionType>().HasData(
            new TransactionType
            {
                Id = TransactionTypeIds.Income,
                Name = "Income",
            },
            new TransactionType
            {
                Id = TransactionTypeIds.Expense,
                Name = "Expense",
            }
        );
    }

    public DbSet<BankTransaction> BankTransaction => Set<BankTransaction>();
    public DbSet<TransactionType> TransactionType => Set<TransactionType>();
}