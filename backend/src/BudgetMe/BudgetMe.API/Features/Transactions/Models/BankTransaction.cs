using System.ComponentModel.DataAnnotations;
using BudgetMe.API.Features.Categories.Models;

namespace BudgetMe.API.Features.Transactions.Models;

public class BankTransaction(
    Guid id,
    Guid transactionTypeId,
    DateTime transactionTime,
    decimal amount,
    string? description,
    Guid categoryId)
{
    public Guid Id { get; set; } = id;
    [Required] public Guid TransactionTypeId { get; set; } = transactionTypeId;
    [Required] public TransactionType TransactionType { get; set; } = null!;
    [Required] public Guid CategoryId { get; set; } = categoryId;
    [Required] public Category Category { get; set; } = null!;
    public DateTime TransactionTime { get; set; } = transactionTime;
    public decimal Amount { get; set; } = amount;
    [StringLength(250)] public string? Description { get; set; } = description;
}