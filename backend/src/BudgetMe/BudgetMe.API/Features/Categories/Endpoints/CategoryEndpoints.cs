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
                .Select(x => new CategoryDto(x.Id, x.Name, x.Description, x.TransactionType.Name, x.TransactionTypeId, x.IsSystem))
                .ToListAsync();
        });

        app.MapGet("/api/category/{id}", async (Guid id, AppDbContext context) =>
        {
            var category = await context.Category
                .Where(x => x.Id == id)
                .Select(x => new CategoryDto(x.Id, x.Name, x.Description, x.TransactionType.Name, x.TransactionTypeId, x.IsSystem))
                .FirstOrDefaultAsync();
            return category is not null
                ? Results.Ok(category)
                : Results.NotFound();
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

        app.MapPut("/api/category/{id}", async (Guid id, UpdateCategoryDto dto, AppDbContext context) =>
        {
            var category = await context.Category
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            if (category is null)
                return Results.NotFound();
            if (category.IsSystem)
                return Results.Json(
                    new { message = "System categories cannot be updated." },
                    statusCode: StatusCodes.Status403Forbidden
                );
            
            category.Name = dto.Name;
            category.Description = dto.Description;
            category.TransactionTypeId = dto.TransactionTypeId;
            
            await context.SaveChangesAsync();
            return Results.NoContent();
        });

        app.MapDelete("/api/category/{id}", async (Guid id, AppDbContext context) =>
        {
            if (await context.Category.FindAsync(id) is { } category)
            {
                if (category.IsSystem)
                    return Results.Json(
                        new { message = "System categories cannot be deleted." },
                        statusCode: StatusCodes.Status403Forbidden
                    );
                
                var hasRelatedTransactions = await context.BankTransaction
                    .AnyAsync(x => x.CategoryId == category.Id);
                if (hasRelatedTransactions)
                    return Results.Json(
                        new { message = "Transactions with this category exist. Please delete related transactions or change their category before retrying." },
                        statusCode: StatusCodes.Status403Forbidden
                    );
                
                context.Category.Remove(category);
                await context.SaveChangesAsync();
                return Results.NoContent();
            }

            return Results.NotFound();
        });
    }
}