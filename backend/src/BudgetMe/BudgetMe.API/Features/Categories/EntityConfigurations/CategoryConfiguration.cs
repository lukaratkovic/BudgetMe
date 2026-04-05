using BudgetMe.API.Features.Categories.Models;
using BudgetMe.API.Features.Transactions.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BudgetMe.API.Features.Categories.EntityConfigurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(x => x.Id);
        builder
            .HasIndex(x => new { x.Name, x.TransactionTypeId })
            .IsUnique();
        builder.HasData(
            new Category(CategoryIds.Household, "Household", null, TransactionTypeIds.Expense, true),
            new Category(CategoryIds.Food, "Food", null, TransactionTypeIds.Expense, true),
            new Category(CategoryIds.Entertainment, "Entertainment", null, TransactionTypeIds.Expense, true),
            new Category(CategoryIds.Transportation, "Transportation", null, TransactionTypeIds.Expense, true),
            new Category(CategoryIds.GiftExpense, "Gift", null, TransactionTypeIds.Expense, true),
            new Category(CategoryIds.OtherExpense, "Other", null, TransactionTypeIds.Expense, true),
            
            new Category(CategoryIds.Earnings, "Earnings", null, TransactionTypeIds.Income, true),
            new Category(CategoryIds.GiftIncome, "Gift", null, TransactionTypeIds.Income, true),
            new Category(CategoryIds.OtherIncome, "Other", null, TransactionTypeIds.Income, true)
        );
    }
}