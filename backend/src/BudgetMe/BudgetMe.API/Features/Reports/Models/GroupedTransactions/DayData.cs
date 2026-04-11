using BudgetMe.API.Features.Transactions.DTOs;
using BudgetMe.API.Features.Transactions.Models;

namespace BudgetMe.API.Features.Reports.Models.GroupedTransactions;

public record DayData(DateTime Date, List<BankTransactionDto> Transactions)
{
    public decimal Income =>
        Transactions
            .Where(x => x.TransactionTypeId == TransactionTypeIds.Income)
            .Sum(x => x.Amount);
    public decimal Expense =>
        Transactions
            .Where(x => x.TransactionTypeId == TransactionTypeIds.Expense)
            .Sum(x => x.Amount);

    public decimal Balance => Income - Expense;
}