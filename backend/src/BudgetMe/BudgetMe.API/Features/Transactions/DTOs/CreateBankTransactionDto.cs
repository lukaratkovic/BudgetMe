namespace BudgetMe.API.Features.Transactions.DTOs;

public record CreateBankTransactionDto(
    Guid TransactionTypeId,
    decimal Amount,
    DateTime TransactionTime,
    List<Guid> CategoryIds,
    string? Description);