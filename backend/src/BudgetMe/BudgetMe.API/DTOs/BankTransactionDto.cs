namespace BudgetMe.API.DTOs;

public class BankTransactionDto
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public string Type { get; set; } = null!;
}