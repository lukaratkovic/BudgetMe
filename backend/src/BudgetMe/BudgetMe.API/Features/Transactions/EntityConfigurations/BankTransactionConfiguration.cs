using BudgetMe.API.Features.Categories.Models;
using BudgetMe.API.Features.Transactions.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BudgetMe.API.Features.Transactions.EntityConfigurations;

public class BankTransactionConfiguration : IEntityTypeConfiguration<BankTransaction>
{
    public void Configure(EntityTypeBuilder<BankTransaction> builder)
    {
        builder
            .HasMany(t => t.Categories)
            .WithMany(c => c.Transactions)
            .UsingEntity<Dictionary<string, object>>(
                "BankTransactionCategory",
                j => j
                    .HasOne<Category>()
                    .WithMany()
                    .HasForeignKey("CategoriesId")
                    .OnDelete(DeleteBehavior.Restrict),
                j => j
                    .HasOne<BankTransaction>()
                    .WithMany()
                    .HasForeignKey("TransactionsId")
            );
    }
    }