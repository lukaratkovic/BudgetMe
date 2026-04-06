namespace BudgetMe.API.Features.Transactions.DTOs;

public record UpdateBankTransactionDto(
    Guid TransactionTypeId,
    decimal Amount,
    DateTime TransactionTime,
    List<Guid> CategoryIds,
    string? Description);