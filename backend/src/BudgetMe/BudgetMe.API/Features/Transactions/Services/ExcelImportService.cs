using BudgetMe.API.Data;
using BudgetMe.API.Exceptions;
using BudgetMe.API.Features.Categories.Models;
using BudgetMe.API.Features.Transactions.Models;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

namespace BudgetMe.API.Features.Transactions.Services;

public class ExcelImportService : IExcelImportService
{
    private readonly AppDbContext _context;

    public ExcelImportService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ImportResult<List<BankTransaction>>> ImportTransactionsAsync(IFormFile file)
    {
        using var stream = file.OpenReadStream();
        using var package = new ExcelPackage(stream);
        var worksheet = package.Workbook.Worksheets[0];

        var transactions = new List<BankTransaction>();
        var errors = new List<string>();

        for (int row = 6; row <= worksheet.Dimension.Rows; row++)
        {
            var date = worksheet.Cells[row, 1].GetValue<DateTime>();
            var description = worksheet.Cells[row, 3].Text;
            var income = worksheet.Cells[row, 4].GetValue<decimal>();
            var expense = worksheet.Cells[row, 5].GetValue<decimal>();
            decimal amount;

            Guid transactionTypeId;
            if (income != 0m && expense == 0m)
            {
                transactionTypeId = TransactionTypeIds.Income;
                amount = income;
            }
            else if (income == 0m && expense != 0m)
            {
                transactionTypeId = TransactionTypeIds.Expense;
                amount = expense;
            }
            else
            {
                errors.Add(
                    $"Entry on row {row} has invalid parameters — either income or expense should be set, not both.");
                continue;
            }

            var categories = await _context.Binding
                .Where(x => x.Category.TransactionTypeId == transactionTypeId)
                .Where(x => description.Contains(x.Keyword))
                .Select(x => x.Category)
                .Distinct()
                .ToListAsync();

            var transaction = new BankTransaction(
                Guid.NewGuid(),
                transactionTypeId,
                date,
                amount,
                description
            )
            {
                Categories = categories
            };

            transactions.Add(transaction);
        }

        return errors.Any()
            ? ImportResult<List<BankTransaction>>.Fail(errors)
            : ImportResult<List<BankTransaction>>.Success(transactions);
    }
}