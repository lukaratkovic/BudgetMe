using BudgetMe.API.Data;
using BudgetMe.API.Features.Transactions.DTOs;
using BudgetMe.API.Features.Transactions.Mappings;
using BudgetMe.API.Features.Transactions.Models;
using BudgetMe.API.Features.Transactions.Services;
using Microsoft.EntityFrameworkCore;

namespace BudgetMe.API.Features.Transactions.Endpoints;

public static class TransactionEndpoints
{
    public static void MapTransactionEndpoints(this WebApplication app)
    {
        app.MapGet("/api/transaction", async (AppDbContext context) =>
        {
            return await context.BankTransaction
                .Select(BankTransactionMappings.ToDto)
                .ToListAsync();
        });

        app.MapGet("/api/transaction/{id:guid}", async (Guid id, AppDbContext context) =>
        {
            var transaction = await context.BankTransaction
                .Where(x => x.Id == id)
                .Select(BankTransactionMappings.ToDto)
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

            var categories = await context.Category
                .Where(c => dto.CategoryIds.Contains(c.Id))
                .ToListAsync();
            
            if (categories.Count != dto.CategoryIds.Count)
                return Results.BadRequest("One or more of the provided categories does not exist");
            var transaction = new BankTransaction(Guid.NewGuid(), dto.TransactionTypeId, dto.TransactionTime,
                dto.Amount, dto.Description)
            {
                Categories = categories
            };

            context.BankTransaction.Add(transaction);
            await context.SaveChangesAsync();
            
            var newTransactionDto = await context.BankTransaction
                .Where(x => x.Id == transaction.Id)
                .Select(BankTransactionMappings.ToDto)
                .FirstOrDefaultAsync();
        
            return Results.Created($"/api/transaction/{transaction.Id}", newTransactionDto);
        });

        app.MapPut("/api/transaction/{id}", async (UpdateBankTransactionDto dto, Guid id, AppDbContext context) =>
        {
            var transaction = await context.BankTransaction
                .Include(x => x.Categories)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            if (transaction is null)
                return Results.NotFound();
            
            transaction.TransactionTypeId = dto.TransactionTypeId;
            transaction.Amount = dto.Amount;
            transaction.TransactionTime = dto.TransactionTime;
            transaction.Categories = await context.Category
                .Where(x => dto.CategoryIds.Contains(x.Id))
                .ToListAsync();
            transaction.Description = dto.Description;

            if (transaction.Categories.Count != dto.CategoryIds.Count)
                return Results.BadRequest("One or more provided categories do not exist");
            
            var hasInvalidTransactionTypes = transaction.Categories
                .Any(c => c.TransactionTypeId != transaction.TransactionTypeId);
            if (hasInvalidTransactionTypes)
                return Results.BadRequest($"One or more provided categories have a transaction type that is different to the current transaction's type.");

            await context.SaveChangesAsync();

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

        app.MapPost("/api/transaction-import", async (IFormFile file, IExcelImportService service, AppDbContext context) =>
        {
            if (file is null || file.Length == 0)
                return Results.BadRequest("The file was not uploaded or contained no data.");

            var result = await service.ImportTransactionsAsync(file);
            
            if (!result.IsSuccess)
                return Results.BadRequest(result.Errors);

            if (result.Data is null || !result.Data.Any())
                return Results.BadRequest("0 transactions were successfully parsed.");
            
            await context.BankTransaction.AddRangeAsync(result.Data);
            await context.SaveChangesAsync();

            return Results.Ok();
        })
        .DisableAntiforgery();
    }
}