namespace BudgetMe.API.Features.Transactions.DTOs;

public class CreateBankTransactionDto
{
    public Guid TransactionTypeId { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransactionTime { get; set; }
    public Guid CategoryId { get; set; }
    public string? Description { get; set; }
}