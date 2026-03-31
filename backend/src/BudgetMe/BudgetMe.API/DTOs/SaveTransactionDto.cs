namespace BudgetMe.API.DTOs;

public class SaveTransactionDto
{
    public Guid? Id { get; set; }
    public Guid TransactionTypeId { get; set; }
    public decimal Amount { get; set; }
}