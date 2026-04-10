using BudgetMe.API.Data;
using BudgetMe.API.Features.Reports.DTOs;
using BudgetMe.API.Features.Transactions.Mappings;
using BudgetMe.API.Features.Transactions.Models;
using Microsoft.EntityFrameworkCore;

namespace BudgetMe.API.Features.Reports.Endpoints;

public static class ReportsEndpoints
{
    public static void MapReportsEndpoints(this WebApplication app)
    {
        app.MapGet("/api/reports/per-day", async (AppDbContext context) =>
        {
            return await context.BankTransaction
                .GroupBy(x => x.TransactionTime.Date)
                .Select(g => new PerDayDto(
                    g.Key,
                    g.AsQueryable().Select(BankTransactionMappings.ToDto).ToList()))
                .ToListAsync();
        });
    }
}