    using System.Transactions;
    using BudgetMe.API.Data;
    using BudgetMe.API.Features.Categories.DTOs;
    using BudgetMe.API.Features.Categories.Endpoints;
    using BudgetMe.API.Features.Transactions;
    using BudgetMe.API.Features.Transactions.DTOs;
    using BudgetMe.API.Features.Transactions.Endpoints;
    using BudgetMe.API.Features.Transactions.Models;
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
    
    // Map endpoints
    app.MapCategoryEndpoints();
    app.MapTransactionEndpoints();

    app.Run();