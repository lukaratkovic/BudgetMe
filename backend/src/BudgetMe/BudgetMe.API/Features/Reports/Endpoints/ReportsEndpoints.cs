using BudgetMe.API.Data;
using BudgetMe.API.Features.Reports.Models.GroupedTransactions;
using BudgetMe.API.Features.Transactions.Mappings;
using BudgetMe.API.Features.Transactions.Models;
using Microsoft.EntityFrameworkCore;

namespace BudgetMe.API.Features.Reports.Endpoints;

public static class ReportsEndpoints
{
    public static void MapReportsEndpoints(this WebApplication app)
    {
        app.MapGet("/api/reports/grouped-transactions", async (AppDbContext context) =>
        {
            var dayData = context.BankTransaction
                .Include(x => x.Categories)
                .Include(x => x.TransactionType)
                .AsEnumerable()
                .GroupBy(x => x.TransactionTime.Date)
                .Select(x => new DayData(
                    x.Key, 
                    x.AsQueryable().Select(BankTransactionMappings.ToDto).ToList()
                    )
                )
                .OrderByDescending(x => x.Date)
                .ToList();

            var monthData = dayData
                .GroupBy(x => new { x.Date.Month, x.Date.Year })
                .Select(x => new MonthData(
                        x.Key.Month,
                        x.Key.Year,
                        x.ToList()
                    )
                )
                .OrderByDescending(x => x.Month)
                .ToList();

            var yearData = monthData
                .GroupBy(x => x.Year)
                .Select(x => new YearData(
                        x.Key,
                        x.ToList()
                    )
                )
                .OrderBy(x => x.Year)
                .ToList();

            return yearData;
        });
    }
}