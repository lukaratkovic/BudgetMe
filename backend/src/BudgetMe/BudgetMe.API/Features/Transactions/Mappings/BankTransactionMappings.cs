using System.Linq.Expressions;
using BudgetMe.API.Features.Transactions.DTOs;
using BudgetMe.API.Features.Transactions.Models;

namespace BudgetMe.API.Features.Transactions.Mappings;

public class BankTransactionMappings
{
    public static Expression<Func<BankTransaction, BankTransactionDto>> ToDto =
        x => new BankTransactionDto(
            x.Id,
            x.Amount,
            x.TransactionType.Name,
            x.TransactionTypeId,
            x.Categories.Select(c => c.Name).ToList(),
            x.Categories.Select(c => c.Id).ToList(),
            x.TransactionTime,
            x.Description);
}