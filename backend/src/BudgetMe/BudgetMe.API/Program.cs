using System.ComponentModel;
using BudgetMe.API.Data;
using BudgetMe.API.Features.Categories.Endpoints;
using BudgetMe.API.Features.Transactions.Endpoints;
using BudgetMe.API.Features.Transactions.Services;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
    builder.Services.AddOpenApi();

    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("Default")));
    
    builder.Services.AddScoped<IExcelImportService, ExcelImportService>();
    
    ExcelPackage.License.SetNonCommercialPersonal("lukaratkovic");

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