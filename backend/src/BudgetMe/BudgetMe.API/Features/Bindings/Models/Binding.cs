using System.ComponentModel.DataAnnotations;
using BudgetMe.API.Features.Categories.Models;
using BudgetMe.API.Features.Transactions.Models;

namespace BudgetMe.API.Features.Bindings.Models;

public class Binding(Guid id, string keyword, Guid categoryId)
{
    public Guid Id { get; set; } = id;
    [StringLength(50)] public string Keyword { get; set; } = keyword;
    public Guid CategoryId { get; set; } = categoryId;
    public Category Category { get; set; } = null!;
}