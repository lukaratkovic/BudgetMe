namespace BudgetMe.API.Features.Categories.DTOs;

public record CategoryDto(Guid Id, string Name, string? Description, string TransactionType, Guid TransactionTypeId, bool IsSystem);