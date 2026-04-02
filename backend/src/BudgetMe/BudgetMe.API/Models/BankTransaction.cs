using System.ComponentModel.DataAnnotations;

namespace BudgetMe.API.Models;

public class BankTransaction(
    Guid id,
    Guid transactionTypeId,
    DateTime transactionTime,
    decimal amount,
    string? description)
{
    public Guid Id { get; set; } = id;
    [Required] public Guid TransactionTypeId { get; set; } = transactionTypeId;
    [Required] public TransactionType TransactionType { get; set; }
    public DateTime TransactionTime { get; set; } = transactionTime;
    public decimal Amount { get; set; } = amount;
    [StringLength(250)] public string? Description { get; set; } = description;
}