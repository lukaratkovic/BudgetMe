using System.ComponentModel.DataAnnotations;
using BudgetMe.API.Features.Categories.Models;

namespace BudgetMe.API.Features.Transactions.Models;

public class BankTransaction(
    Guid id,
    Guid transactionTypeId,
    DateTime transactionTime,
    decimal amount,
    string? description,
    string? referenceNumber = null)
{
    public Guid Id { get; set; } = id;
    [Required] public Guid TransactionTypeId { get; set; } = transactionTypeId;
    [Required] public TransactionType TransactionType { get; set; } = null!;
    public ICollection<Category> Categories { get; set; } = new List<Category>();
    public DateTime TransactionTime { get; set; } = transactionTime;
    public decimal Amount { get; set; } = amount;
    [StringLength(250)] public string? Description { get; set; } = description;
    [StringLength(100)] public string? ReferenceNumber { get; set; } = referenceNumber;
}