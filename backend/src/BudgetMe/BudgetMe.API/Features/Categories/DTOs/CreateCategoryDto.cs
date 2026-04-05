namespace BudgetMe.API.Features.Categories.DTOs;

public record CreateCategoryDto(
    string Name,
    string? Description,
    Guid TransactionTypeId);