using BudgetMe.API.Data;
using BudgetMe.API.Features.Categories.DTOs;
using BudgetMe.API.Features.Categories.Models;
using BudgetMe.API.Helpers;
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

        app.MapPost("/api/category", async (CreateCategoryDto dto, AppDbContext context) =>
        {
            var category = new Category(Guid.NewGuid(), dto.Name, dto.Description, dto.TransactionTypeId);
            try
            {
                context.Add(category);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException e) when (DbHelper.IsUniqueConstraintViolation(e))
            {
                return Results.Conflict(new
                {
                    message = "Category already exists.",
                });
            }
            
            return Results.Created($"/api/category/{category.Id}", category); // TODO: Implement this get
        });
    }
}