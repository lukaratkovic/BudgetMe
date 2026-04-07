using BudgetMe.API.Features.Bindings.Models;
using BudgetMe.API.Features.Categories.Models;
using BudgetMe.API.Features.Transactions;
using BudgetMe.API.Features.Transactions.Models;
using Microsoft.EntityFrameworkCore;

namespace BudgetMe.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    public DbSet<BankTransaction> BankTransaction => Set<BankTransaction>();
    public DbSet<TransactionType> TransactionType => Set<TransactionType>();
    public DbSet<Category> Category => Set<Category>();
    public DbSet<Binding> Binding => Set<Binding>();
}