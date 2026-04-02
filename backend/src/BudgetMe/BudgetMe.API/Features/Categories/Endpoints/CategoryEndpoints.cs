using BudgetMe.API.Data;
using BudgetMe.API.Features.Categories.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BudgetMe.API.Features.Categories.Endpoints;

public static class CategoryEndpoints
{
    public static void MapCategoryEndpoints(this WebApplication app)
    {
        app.MapGet("/api/category", async (
            Guid? transactionTypeId,
            AppDbContext context
        ) =>
        {
            var query = context.Category.AsQueryable();
        
            if (transactionTypeId.HasValue)
                query = query.Where(x => x.TransactionTypeId == transactionTypeId);
        
            return await query
                .Select(x => new CategoryDto(x.Id, x.Name, x.Description, x.TransactionType.Name))
                .ToListAsync();
        });
    }
}