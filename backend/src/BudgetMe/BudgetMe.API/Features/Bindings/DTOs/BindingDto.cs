namespace BudgetMe.API.Features.Bindings.DTOs;

public record BindingDto(
    Guid Id,
    string Keyword,
    Guid CategoryId,
    string Category,
    Guid TransactionTypeId,
    string TransactionType);