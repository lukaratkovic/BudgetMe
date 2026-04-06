namespace BudgetMe.API.Features.Transactions.DTOs;

public record BankTransactionDto(
    Guid Id,
    decimal Amount,
    string Type,
    Guid TransactionTypeId,
    List<string> CategoryNames,
    List<Guid> CategoryIds,
    DateTime TransactionTime,
    string? Description);