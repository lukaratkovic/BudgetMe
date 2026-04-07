using System.Linq.Expressions;
using BudgetMe.API.Features.Categories.DTOs;
using BudgetMe.API.Features.Categories.Models;

namespace BudgetMe.API.Features.Categories.Mappings;

public class CategoryMappings
{
    public static Expression<Func<Category, CategoryDto>> ToDto(List<string> duplicateNames) =>
        x => new CategoryDto(
            x.Id,
            x.Name,
            duplicateNames.Contains(x.Name)
                ? $"{x.Name} ({x.TransactionType.Name})"
                : x.Name,
            x.Description,
            x.TransactionType.Name,
            x.TransactionTypeId,
            x.IsSystem);
}