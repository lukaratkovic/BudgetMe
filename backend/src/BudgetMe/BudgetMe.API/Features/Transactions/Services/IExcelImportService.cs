using BudgetMe.API.Features.Transactions.Models;

namespace BudgetMe.API.Features.Transactions.Services;

public interface IExcelImportService
{
    Task<ImportResult<List<BankTransaction>>> ImportTransactionsAsync(IFormFile file);
}