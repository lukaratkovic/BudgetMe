namespace BudgetMe.API.Models;

public class TransactionType
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}

public static class TransactionTypeIds
{
    public static readonly Guid Income = Guid.Parse("520A21EA-E710-4EB6-ABEF-5C4A93D55720");
    public static readonly Guid Expense = Guid.Parse("A96BCD4C-C469-430F-951E-E3492F1DE2DB");
}