namespace BudgetMe.API.Features.Categories.DTOs;

public record UpdateCategoryDto(string Name, string? Description, Guid TransactionTypeId);