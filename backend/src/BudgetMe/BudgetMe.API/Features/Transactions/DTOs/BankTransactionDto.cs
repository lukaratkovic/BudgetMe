namespace BudgetMe.API.Features.Transactions.DTOs;

public record BankTransactionDto(
    Guid Id,
    decimal Amount,
    string Type,
    DateTime TransactionTime,
    string? Description);