namespace BudgetMe.API.Models;

public class BankTransaction
{
    public Guid Id { get; set; }
    public Guid TransactionTypeId { get; set; }
    public TransactionType TransactionType { get; set; } = null!;
    public DateTime TransactionTime { get; set; }
    public decimal Amount { get; set; }
}