    using BudgetMe.API.Data;
    using BudgetMe.API.DTOs;
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

    app.Run();