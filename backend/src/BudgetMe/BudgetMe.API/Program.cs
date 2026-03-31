    using System.Transactions;
    using BudgetMe.API.Data;
    using BudgetMe.API.DTOs;
    using BudgetMe.API.Models;
    using Microsoft.EntityFrameworkCore;

    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
    builder.Services.AddOpenApi();

    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("Default")));

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
    }

    app.UseHttpsRedirection();

    app.MapGet("/api/transactions", async (AppDbContext db) =>
    {
        return await db.BankTransaction
            .Select(x => new BankTransactionDto
            {
                Id = x.Id,
                Amount = x.Amount,
                Type = x.TransactionType.Name
            })
            .ToListAsync();
    });

    app.MapGet("/api/transactionTypes", async (AppDbContext db) =>
    {
        return await db.TransactionType
            .Select(x => new TransactionTypeDto()
            {
                Id = x.Id,
                Name = x.Name
            })
            .ToListAsync();
    });

    app.MapPost("/api/transaction", async (SaveTransactionDto dto, AppDbContext db) =>
    {
        if (dto.Amount <= 0)
            return Results.BadRequest("Amount must be greater than 0");

        var transactionTypeExists = await db.TransactionType
            .AnyAsync(x => x.Id == dto.TransactionTypeId);
        if (!transactionTypeExists)
            return Results.BadRequest("Provided transaction type does not exist");

        var transaction = new BankTransaction
        {
            Id = Guid.NewGuid(),
            TransactionTypeId = dto.TransactionTypeId,
            Amount = dto.Amount,
        };

        db.BankTransaction.Add(transaction);
        await db.SaveChangesAsync();
        
        return Results.Created($"/api/transaction/{transaction.Id}", transaction);
    });

    app.Run();