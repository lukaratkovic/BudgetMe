namespace BudgetMe.API.Features.Transactions.DTOs;

public class SaveTransactionDto
{
    public Guid TransactionTypeId { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransactionTime { get; set; }
    public string? Description { get; set; }
}