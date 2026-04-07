using BudgetMe.API.Data;
using BudgetMe.API.Features.Bindings.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BudgetMe.API.Features.Bindings.Endpoints;

public static class BindingEndpoints
{
    public static void MapBindingEndpoints(this WebApplication app)
    {
        app.MapGet("/api/binding", async (AppDbContext context) =>
        {
            return await context.Binding
                .Select(x => new BindingDto(
                    x.Id,
                    x.Keyword,
                    x.CategoryId,
                    x.Category.Name,
                    x.Category.TransactionTypeId,
                    x.Category.TransactionType.Name))
                .ToListAsync();
        });
    }
}