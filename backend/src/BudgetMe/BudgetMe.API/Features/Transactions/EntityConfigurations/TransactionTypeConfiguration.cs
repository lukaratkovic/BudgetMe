using BudgetMe.API.Features.Transactions.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BudgetMe.API.Features.Transactions.EntityConfigurations;

public class TransactionTypeConfiguration : IEntityTypeConfiguration<TransactionType>
{
    public void Configure(EntityTypeBuilder<TransactionType> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasData(
            new TransactionType(TransactionTypeIds.Income, "Income"),
            new TransactionType(TransactionTypeIds.Expense, "Expense")
        );
    }
}