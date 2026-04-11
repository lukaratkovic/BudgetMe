namespace BudgetMe.API.Features.Reports.Models.GroupedTransactions;

public record YearData(int Year, List<MonthData> MonthData)
{
    public decimal Income => MonthData.Sum(x => x.Income);
    public decimal Expense => MonthData.Sum(x => x.Expense);
    public decimal Balance => MonthData.Sum(x => x.Balance);
}