namespace BudgetMe.API.DTOs;

public class BankTransactionDto(
    Guid id,
    decimal amount,
    string type,
    DateTime transactionTime,
    string? description)
{
    public Guid Id { get; set; } = id;
    public decimal Amount { get; set; } = amount;
    public string Type { get; set; } = type;
    public DateTime TransactionTime { get; set; } = transactionTime;
    public string? Description { get; set; } = description;
}