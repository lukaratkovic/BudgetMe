using System.ComponentModel.DataAnnotations;

namespace BudgetMe.API.Features.Transactions.Models;

public class TransactionType(Guid id, string name)
{
    public Guid Id { get; set; } = id;
    [Required] [StringLength(50)] public string Name { get; set; } = name;
}

public static class TransactionTypeIds
{
    public static readonly Guid Income = Guid.Parse("520A21EA-E710-4EB6-ABEF-5C4A93D55720");
    public static readonly Guid Expense = Guid.Parse("A96BCD4C-C469-430F-951E-E3492F1DE2DB");
}