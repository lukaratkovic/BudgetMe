namespace BudgetMe.API.Features.Categories.DTOs;

public class CategoryDto(Guid id, string name, string? description, string transactionType)
{
    public Guid Id { get; set; } = id;
    public string Name { get; set; } = name;
    public string? Description { get; set; } = description;
    public string TransactionType { get; set; } = transactionType;
}