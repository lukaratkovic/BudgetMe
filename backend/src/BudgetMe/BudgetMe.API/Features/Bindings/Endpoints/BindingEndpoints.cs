using BudgetMe.API.Data;
using BudgetMe.API.Features.Bindings.DTOs;
using BudgetMe.API.Features.Bindings.Mappings;
using BudgetMe.API.Features.Bindings.Models;
using Microsoft.EntityFrameworkCore;

namespace BudgetMe.API.Features.Bindings.Endpoints;

public static class BindingEndpoints
{
    public static void MapBindingEndpoints(this WebApplication app)
    {
        app.MapGet("/api/binding", async (AppDbContext context) =>
        {
            return await context.Binding
                .Select(BindingMappings.ToDto)
                .ToListAsync();
        });

        app.MapGet("/api/binding/{id}", async (Guid id, AppDbContext context) =>
        {
            var binding = await context.Binding
                .Where(x => x.Id == id)
                .Select(BindingMappings.ToDto)
                .FirstOrDefaultAsync();
            return binding is not null
                ? Results.Ok(binding)
                : Results.NotFound();
        });

        app.MapPost("/api/binding", async (CreateBindingDto dto, AppDbContext context) =>
        {
            var exists = await context.Binding
                .AnyAsync(x => x.Keyword == dto.Keyword && x.CategoryId == dto.CategoryId);
            if (exists)
                return Results.Conflict();
            
            var binding = new Binding(Guid.NewGuid(), dto.Keyword, dto.CategoryId);
            
            context.Add(binding);
            await context.SaveChangesAsync();
            
            return Results.Created($"api/binding/{binding.Id}", binding);
        });

        app.MapPut("/api/binding/{id}", async (UpdateBindingDto dto, Guid id, AppDbContext context) =>
        {
            if (await context.Binding.FindAsync(id) is { } binding)
            {
                binding.Keyword = dto.Keyword;
                binding.CategoryId = dto.CategoryId;
                
                context.Update(binding);
                await context.SaveChangesAsync();
                
                return Results.NoContent();
            }

            return Results.NotFound();
        });

        app.MapDelete("/api/binding/{id}", async (Guid id, AppDbContext context) =>
        {
            var binding = await context.Binding
                .FindAsync(id);
            
            if (binding is null)
                return Results.NotFound();

            context.Remove(binding);
            await context.SaveChangesAsync();
            
            return Results.NoContent();
        });
    }
}