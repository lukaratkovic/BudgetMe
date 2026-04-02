using System.ComponentModel.DataAnnotations;

namespace BudgetMe.API.Models;

public class BankTransaction
{
    public Guid Id { get; set; }
    [Required] public Guid TransactionTypeId { get; set; }
    [Required] public TransactionType TransactionType { get; set; }
    public DateTime TransactionTime { get; set; }
    public decimal Amount { get; set; }
}