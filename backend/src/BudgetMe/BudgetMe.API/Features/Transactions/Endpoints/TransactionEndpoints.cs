using BudgetMe.API.Data;
using BudgetMe.API.Features.Transactions.DTOs;
using BudgetMe.API.Features.Transactions.Models;
using Microsoft.EntityFrameworkCore;

namespace BudgetMe.API.Features.Transactions.Endpoints;

public static class TransactionEndpoints
{
    public static void MapTransactionEndpoints(this WebApplication app)
    {
        app.MapGet("/api/transaction", async (AppDbContext context) =>
        {
            return await context.BankTransaction
                .Select(x => new BankTransactionDto(
                    x.Id,
                    x.Amount,
                    x.TransactionType.Name,
                    x.TransactionTypeId,
                    x.Category.Name,
                    x.CategoryId,
                    x.TransactionTime,
                    x.Description))
                .ToListAsync();
        });

        app.MapGet("/api/transaction/{id:guid}", async (Guid id, AppDbContext context) =>
        {
            var transaction = await context.BankTransaction
                .Where(x => x.Id == id)
                .Select(x => new BankTransactionDto(
                    x.Id,
                    x.Amount,
                    x.TransactionType.Name,
                    x.TransactionTypeId,
                    x.Category.Name,
                    x.CategoryId,
                    x.TransactionTime,
                    x.Description))
                .FirstOrDefaultAsync();
            return transaction is not null
                ? Results.Ok(transaction)
                : Results.NotFound();
        });
        
        app.MapPost("/api/transaction", async (CreateBankTransactionDto dto, AppDbContext context) =>
        {
            if (dto.Amount <= 0)
                return Results.BadRequest("Amount must be greater than 0");

            var transactionTypeExists = await context.TransactionType
                .AnyAsync(x => x.Id == dto.TransactionTypeId);
            if (!transactionTypeExists)
                return Results.BadRequest("Provided transaction type does not exist");
        
            var categoryExists = await context.Category
                .AnyAsync(x => x.Id == dto.CategoryId);
            if (!categoryExists)
                return Results.BadRequest("Provided category does not exist");

            var transaction = new BankTransaction(Guid.NewGuid(), dto.TransactionTypeId, dto.TransactionTime, dto.Amount, dto.Description, dto.CategoryId);

            context.BankTransaction.Add(transaction);
            await context.SaveChangesAsync();
        
            return Results.Created($"/api/transaction/{transaction.Id}", transaction);
        });

        app.MapPut("/api/transaction/{id}", async (UpdateBankTransactionDto dto, Guid id, AppDbContext context) =>
        {
            var affected = await context.BankTransaction
                .Where(x => x.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(x => x.TransactionTypeId, dto.TransactionTypeId)
                    .SetProperty(x => x.Amount, dto.Amount)
                    .SetProperty(x => x.TransactionTime, dto.TransactionTime)
                    .SetProperty(x => x.CategoryId, dto.CategoryId)
                    .SetProperty(x => x.Description, dto.Description)
                );
            
            if (affected == 0)
                return Results.NotFound();

            return Results.NoContent();
        });

        app.MapDelete("/api/transaction/{id}", async (Guid id, AppDbContext context) =>
        {
            if (await context.BankTransaction.FindAsync(id) is { } transaction)
            {
                context.BankTransaction.Remove(transaction);
                await context.SaveChangesAsync();
                return Results.NoContent();
            }

            return Results.NotFound();
        });
        
        app.MapGet("/api/transactionType", async (AppDbContext context) =>
        {
            return await context.TransactionType
                .Select(x => new TransactionTypeDto(x.Id, x.Name))
                .ToListAsync();
        });
    }
}