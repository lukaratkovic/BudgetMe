using BudgetMe.API.Features.Categories.DTOs;

namespace BudgetMe.API.Features.Transactions.DTOs;

public record BankTransactionDto(
    Guid Id,
    decimal Amount,
    string Type,
    Guid TransactionTypeId,
    List<CategoryDto> Categories,
    DateTime TransactionTime,
    string? Description);