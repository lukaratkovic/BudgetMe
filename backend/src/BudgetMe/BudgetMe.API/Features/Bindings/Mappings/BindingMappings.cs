using System.Linq.Expressions;
using BudgetMe.API.Features.Bindings.DTOs;
using BudgetMe.API.Features.Bindings.Models;

namespace BudgetMe.API.Features.Bindings.Mappings;

public class BindingMappings
{
    public static Expression<Func<Binding, BindingDto>> ToDto =
        x => new BindingDto(
            x.Id,
            x.Keyword,
            x.CategoryId,
            x.Category.Name,
            x.Category.TransactionTypeId,
            x.Category.TransactionType.Name);
}