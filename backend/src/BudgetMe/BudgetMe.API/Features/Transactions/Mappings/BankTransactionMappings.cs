using System.Linq.Expressions;
using BudgetMe.API.Features.Categories.DTOs;
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
            x.Categories.Select(c => new CategoryDto(
                c.Id,
                c.Name,
                c.Name,
                c.Description,
                c.TransactionType.Name,
                c.TransactionTypeId,
                c.IsSystem)).ToList(),
            x.TransactionTime,
            x.Description);
}