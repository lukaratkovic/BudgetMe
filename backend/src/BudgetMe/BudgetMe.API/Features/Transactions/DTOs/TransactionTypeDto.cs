namespace BudgetMe.API.Features.Transactions.DTOs;

public class TransactionTypeDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}