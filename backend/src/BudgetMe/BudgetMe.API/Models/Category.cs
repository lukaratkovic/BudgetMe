using System.ComponentModel.DataAnnotations;

namespace BudgetMe.API.Models;

public class Category(Guid id, string name, string? description, Guid transactionTypeId)
{
    public Guid Id { get; set; } = id;
    [Required] [StringLength(50)] public string Name { get; set; } = name;
    [StringLength(250)] public string? Description { get; set; } = description;
    [Required] public Guid TransactionTypeId { get; set; } = transactionTypeId;
    [Required] public TransactionType TransactionType { get; set; }
}

public class CategoryIds
{
    // Expenses
    public static readonly Guid Household = new Guid("32B90ECF-51E6-48A8-A9DB-CF8CA9A79AB5");
    public static readonly Guid Food = new Guid("8E9F5D5D-FBED-4748-8D5C-53B1D4ABB84F");
    public static readonly Guid Entertainment = new Guid("E7B9DD86-7C8E-48F1-ACD6-ACE5E8909C23");
    public static readonly Guid Transportation = new Guid("1D65AFC4-0EDF-464E-8CAB-24F7A175F043");
    public static readonly Guid GiftExpense = new Guid("3A75B398-BE8F-410C-A3D0-3F9CA395EF98");
    public static readonly Guid OtherExpense = new Guid("0ECB7853-21B1-4E54-9BC2-8E2099E0F2F9");
    
    // Income
    public static readonly Guid Earnings = new Guid("8E19C4F0-6051-4EC3-B306-B43616379436");
    public static readonly Guid GiftIncome = new Guid("13237788-358D-48AE-A686-7100AA4E1333");
    public static readonly Guid OtherIncome = new Guid("13921DB7-47F2-4E1A-B87E-C75530164533");
}